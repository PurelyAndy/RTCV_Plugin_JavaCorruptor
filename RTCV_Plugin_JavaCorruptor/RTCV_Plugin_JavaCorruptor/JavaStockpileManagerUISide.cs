using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Java_Corruptor.UI.Components;
using Java_Corruptor.BlastClasses;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;

namespace Java_Corruptor;

public static class JavaStockpileManagerUISide
{
    //Object references
    private static JavaStockpile CurrentStockpile { get; set; }
    internal static JavaStashKey LastStashkey { get; private set; }

    private static JavaStashKey _currentStashKey;
    internal static JavaStashKey CurrentStashkey
    {
        get => _currentStashKey;
        set
        {
            LastStashkey = CurrentStashkey;
            _currentStashKey = value;
        }
    }
    internal static JavaStashKey CurrentSavestateStashKey { get; set; }
    public static bool StashAfterOperation { get; set; } = true;
    internal static readonly List<JavaStashKey> StashHistory = [];

    internal static bool ApplyStashkey(JavaStashKey sk, bool clearUnitsBeforeApply = true)
    {
        bool isCorruptionApplied = sk?.BlastLayer?.MappedLayers?.Count > 0;

        bool mergeWithCurrent = !clearUnitsBeforeApply;

        JavaCorruptionEngineForm ceForm = S.GET<JavaCorruptionEngineForm>();
        string oldJarName = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];

        AllSpec.VanguardSpec.Update(VSPEC.OPENROMFILENAME, sk.JarFilename);

        SerializedInsnBlastLayerCollection bl = sk.BlastLayer;

        if (mergeWithCurrent)
        {
            foreach (KeyValuePair<string, SerializedInsnBlastLayer> kv in bl)
            {
                JavaCorruptionEngineForm.BlastLayerCollection.Add(kv);
            }
        }
        else
            JavaCorruptionEngineForm.BlastLayerCollection = bl;
        
        ceForm.Corrupt();
        AllSpec.VanguardSpec.Update(VSPEC.OPENROMFILENAME, oldJarName);

        return isCorruptionApplied;
    }

    internal static void Import(SerializedInsnBlastLayerCollection importedBlastLayer,string name = null)
    {
        JavaStashKey psk = CurrentSavestateStashKey;

        if (psk is null)
        {
            string s = RtcCore.GetRandomKey();
            psk = new(s, s, null);
        }

        //We make it without the blastlayer so we can send it across and use the cached version without needing a prototype
        CurrentStashkey = new(RtcCore.GetRandomKey(), psk.ParentKey, null)
        {
            JarFilename = psk.JarFilename,
            GameName = psk.GameName,
        };

        SerializedInsnBlastLayerCollection bl = importedBlastLayer;

        CurrentStashkey.BlastLayer = bl;
        StashHistory.Add(CurrentStashkey);
    }

    internal static bool Corrupt(bool loadBeforeOperation = true)
    {
        string saveStateWord = "Savestate";

        object renameSaveStateWord = AllSpec.VanguardSpec[VSPEC.RENAME_SAVESTATE];
        if (renameSaveStateWord != null && renameSaveStateWord is string s)
        {
            saveStateWord = s;
        }

        JavaStashKey psk = CurrentSavestateStashKey;

        bool UseSavestates = (bool)AllSpec.VanguardSpec[VSPEC.SUPPORTS_SAVESTATES];
        if (!UseSavestates)
        {
            psk = SaveState();
        }

        if (psk == null && UseSavestates)
        {
            MessageBox.Show($"The Glitch Harvester could not perform the CORRUPT action\n\nEither no {saveStateWord} Box was selected in the {saveStateWord} Manager\nor the {saveStateWord} Box itself is empty.");
            return false;
        }

        /*string currentGame = (string)AllSpec.VanguardSpec[VSPEC.GAMENAME]; TODO: i think this is unnecessary
        string currentCore = (string)AllSpec.VanguardSpec[VSPEC.SYSTEMCORE];
        if (UseSavestates && (currentGame == null || psk.GameName != currentGame))
        {
            //LocalNetCoreRouter.Route(NetCore.Endpoints.Vanguard, NetCore.Commands.Remote.LoadROM, psk.JarFilename, true);
            //TODO: Load the jar into JavaCorruptorForm
        }*/

        //We make it without the blastlayer so we can send it across and use the cached version without needing a prototype
        CurrentStashkey = new(RtcCore.GetRandomKey(), psk.ParentKey, null)
        {
            JarFilename = psk.JarFilename,
            GameName = psk.GameName,
        };

        /*JavaBlastLayer bl = LocalNetCoreRouter.QueryRoute<JavaBlastLayer>(NetCore.Endpoints.CorruptCore,
            NetCore.Commands.Basic.GenerateBlastLayer,
            new object[]
            {
                CurrentStashkey,
                loadBeforeOperation,
                true,
                true
            }, true);*/
        JavaCorruptionEngineForm ceForm = S.GET<JavaCorruptionEngineForm>();
        ceForm.Corrupt(true);
        SerializedInsnBlastLayerCollection bl = JavaCorruptionEngineForm.BlastLayerCollection;
        bool isCorruptionApplied = bl?.MappedLayers?.Count > 0;
        
        CurrentStashkey.BlastLayer = bl;

        if (StashAfterOperation && bl != null)
        {
            StashHistory.Add(CurrentStashkey);
        }

        return isCorruptionApplied;
    }

    internal static void RemoveFirstStashItem()
    {
        StashHistory.RemoveAt(0);
    }

    internal static bool MergeStashkeys(List<JavaStashKey> sks, bool loadBeforeOperation = true)
    {
        if (sks?.Count > 1)
        {
            JavaStashKey master = sks[0];

            if (!RtcCore.AllowCrossCoreCorruption)
            {
                if (sks.Any(item => item.GameName != master.GameName))
                {
                    MessageBox.Show("Merge attempt failed: game mismatch\n\n" + string.Join("\n", sks.Select(it => $"{it.GameName}")));
                    return false;
                }
            }

            SerializedInsnBlastLayerCollection bl = new();

            foreach (JavaStashKey item in sks)
            {
                foreach (KeyValuePair<string, SerializedInsnBlastLayer> kv in item.BlastLayer.MappedLayers)
                {
                    bl.Add(kv);
                }
            }

            bl.Layer = new(bl.Layer.Layer.Distinct().ToList());

            CurrentStashkey = new(RtcCore.GetRandomKey(), master.ParentKey, bl)
            {
                JarFilename = master.JarFilename,
                GameName = master.GameName,
            };

            bool isCorruptionApplied = CurrentStashkey?.BlastLayer?.MappedLayers?.Count > 0;

            /*if (loadBeforeOperation)
            {
                if (!LoadState(CurrentStashkey))
                {
                    return isCorruptionApplied;
                }
            }
            else
            {
                LocalNetCoreRouter.Route(NetCore.Endpoints.CorruptCore, NetCore.Commands.Basic.ApplyBlastLayer, new object[] { CurrentStashkey.BlastLayer, true }, true);
            }*/
            //JavaCorruptionEngineForm.BlastLayer = bl;
            JavaCorruptionEngineForm ceForm = S.GET<JavaCorruptionEngineForm>();
            
            string oldEngineName = ceForm.GetEngineName();
            string oldJarName = (string)AllSpec.VanguardSpec[VSPEC.OPENROMFILENAME];
            AllSpec.VanguardSpec.Update(VSPEC.OPENROMFILENAME, CurrentStashkey!.JarFilename);

            JavaCorruptionEngineForm.BlastLayerCollection = bl;

            ceForm.Corrupt();
            AllSpec.VanguardSpec.Update(VSPEC.OPENROMFILENAME, oldJarName);


            if (StashAfterOperation)
            {
                StashHistory.Add(CurrentStashkey);
            }


            return true;
        }
        MessageBox.Show("You need 2 or more items for Merging");
        return false;
    }

    internal static JavaStashKey SaveState(JavaStashKey sk = null)
    {
        if (sk != null)
            return sk;
        if (CurrentSavestateStashKey != null)
            return CurrentSavestateStashKey;
        string s = RtcCore.GetRandomKey();
        sk = new(s, s, null);

        return sk;
        /*bool UseSavestates = (bool)AllSpec.VanguardSpec[VSPEC.SUPPORTS_SAVESTATES]; TODO: something about this

        if (UseSavestates)
        {
            return LocalNetCoreRouter.QueryRoute<JavaStashKey>(NetCore.Endpoints.CorruptCore, NetCore.Commands.Remote.SaveState, sk, true);
        }
        else
        {
            return LocalNetCoreRouter.QueryRoute<JavaStashKey>(NetCore.Endpoints.CorruptCore, NetCore.Commands.Remote.SaveStateless, sk, true);
        }*/
    }


    internal static void StockpileChanged()
    {
        //S.GET<RTC_StockpileBlastBoard_Form>().RefreshButtons();
    }


    internal static bool AddCurrentStashkeyToStash(bool stashAfterOperation = true)
    {
        bool isCorruptionApplied = CurrentStashkey?.BlastLayer?.MappedLayers?.Count > 0;

        if (isCorruptionApplied && StashAfterOperation && stashAfterOperation)
        {
            StashHistory.Add(CurrentStashkey);
        }

        return isCorruptionApplied;
    }
        
    /// <summary>
    /// Takes a stashkey and a list of keys, fixing the path and if a list of keys is provided, it'll look for all shared references and update them
    /// </summary>
    /// <param name="psk"></param>
    /// <param name="force"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    internal static bool CheckAndFixMissingReference(JavaStashKey psk, bool force = false, IEnumerable<JavaStashKey> keys = null, string customTitle = null, string customMessage = null)
    {
        if (psk == null)
            throw new ArgumentNullException(nameof(psk));

        string message = customMessage ?? $"Can't find file {psk.JarFilename}\nGame name: {psk.GameName}\n\nTo continue loading, provide a new file for replacement.";
        string title = customTitle ?? "Error: File not found";

        if ((!force && File.Exists(psk.JarFilename)) || psk.JarFilename.EndsWith("IGNORE"))
            return true;
            
        if (DialogResult.OK == MessageBox.Show(message, title, MessageBoxButtons.OKCancel))
        {
            OpenFileDialog ofd = new()
            {
                DefaultExt = "*",
                Title = "Select Replacement File",
                Filter = "Any file|*.*",
                RestoreDirectory = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                string oldFilename = psk.JarFilename;
                if (Path.GetFileName(psk.JarFilename) != Path.GetFileName(filename))
                {
                    if (DialogResult.Cancel == MessageBox.Show($"Selected file {Path.GetFileName(filename)} has a different name than the old file {Path.GetFileName(psk.JarFilename)}.\nIf you know this file is correct, you can ignore this warning.\nContinue?", title,
                            MessageBoxButtons.OKCancel))
                    {
                        return false;
                    }
                }

                foreach (JavaStashKey sk in keys.Where(x => x.JarFilename == oldFilename))
                {
                    sk.JarFilename = filename;
                    sk.JarShortFilename = Path.GetFileName(sk.JarFilename);
                    sk.GameName = Path.GetFileNameWithoutExtension(sk.JarFilename);
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }
        
    internal static void ClearCurrentStockpile()
    {
        CurrentStockpile = new();
        StockpileChanged();
    }

    internal static string GetCurrentStockpilePath()
    {
        return CurrentStockpile?.Filename ?? "";
    }

    internal static void SetCurrentStockpile(JavaStockpile sks)
    {
        CurrentStockpile = sks;
        StockpileChanged();
    }
}