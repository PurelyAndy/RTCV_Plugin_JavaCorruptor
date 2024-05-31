using Java_Corruptor.UI;
using NLog;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using RTCV.CorruptCore;

namespace Java_Corruptor
{
    [Export(typeof(IPlugin))]
    public class Java_Corruptor : IPlugin, IDisposable
    {
        //-------[ Plugin metadata ]-------

        // >>> Make sure you rename BOTH the namespace and class (Very important)
        public string Description => "Corrupt Java programs with ease!";
        public string Author => "NoSkillPureAndy";
        public Version Version => new(1, 0, 0);

        //-----[ Plugin loading workflow ]-----

        //Server is StandaloneRTC process (RTCV UI)
        //Client is Emulator process

        //Tells on which sides the plugin has to load
        public RTCSide SupportedSide => RTCSide.Server;

        //Tells where we want the form to load when OpenTools button is pressed
        public static RTCSide FormRequestSide = RTCSide.Server;     

        //-----[ Additional information ]------

        //Don't forget to also change the following values in the project Application properties:
        // - Assembly name
        // - Default namespace
        // - Assembly Information

        #region Plugin Implementation mechanics

        public static RTCSide CurrentSide = RTCSide.Server;
        public static PluginForm PluginForm;
        internal static PluginConnectorRTC connectorRTC;

        // the name of the plugin is auto-generated from the class name.
        public string Name => nameof(Java_Corruptor).Replace("_"," ");

        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            Logging.GlobalLogger.Info($"{Name} v{Version} initializing on {side} side.");

            //Plugin initialization

            connectorRTC = new(this);
            PluginForm = new(this);
            S.SET(PluginForm);


            // Doing sanity checks before registering the plugin in the OpenTools form
            if (S.ISNULL<OpenToolsForm>())
            {
                Logging.GlobalLogger.Error(
                    $"{Name} v{Version} failed to start: Singleton RTC_OpenTools_Form was null.");
                return false;
            }
            if (S.ISNULL<CoreForm>())
            {
                Logging.GlobalLogger.Error(
                    $"{Name} v{Version} failed to start: Singleton UI_CoreForm was null.");
                return false;
            }
            
            if ((string)AllSpec.VanguardSpec[VSPEC.NAME] != "FileStub")
            {
                Logging.GlobalLogger.Info("Vanguard is not FileStub, Java Corruptor plugin will be hidden.");
                ChangePluginVisibility(false);
            }
            else
            {
                ChangePluginVisibility(true);
                AllSpec.VanguardSpec.SpecUpdated += (_, eas) =>
                {
                    MemoryDomainProxy[] memoryDomains = (MemoryDomainProxy[])eas.partialSpec[VSPEC.MEMORYDOMAINS_INTERFACES];
                    if (memoryDomains is null)
                        return;
                    if (memoryDomains.Length != 1 || !memoryDomains[0].Name.EndsWith(".jar"))
                    {
                        if (memoryDomains.Length < 1)
                            Logging.GlobalLogger.Info("No files were loaded, Java Corruptor plugin will be hidden.");
                        else
                            Logging.GlobalLogger.Info("Multiple files or a non-jar file was loaded, Java Corruptor plugin will be hidden.");
                        ChangePluginVisibility(false);
                    }
                    else
                        ChangePluginVisibility(true);
                };
            }
            
            Logging.GlobalLogger.Info($"{Name} v{Version} initialized.");
            CurrentSide = side;
            return true;
        }

        public bool Stop()
        {
            if (CurrentSide == RTCSide.Client && !S.ISNULL<PluginForm>() && !S.GET<PluginForm>().IsDisposed)
            {
                S.GET<PluginForm>().HideOnClose = false;
                S.GET<PluginForm>().Close();
            }
            return true;
        }

        //this should be in a helper class 
        public static string CamelCase(string text)
        {
            char[] a = text.ToLower().ToCharArray();

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];

            }
            return new string(a);
        }

        public bool StopPlugin()
        {
            if (!S.ISNULL<PluginForm>() && !S.GET<PluginForm>().IsDisposed)
            {
                S.GET<PluginForm>().HideOnClose = false;
                S.GET<PluginForm>().Close();
            }
            return true;
        }

        #endregion

        private void ChangePluginVisibility(bool show)
        {
            string path1 = Path.Combine(RtcCore.RtcDir, "LAYOUTS", $"{(show ? "" : "_")}Java Corruptor.txt");
            string path2 = Path.Combine(RtcCore.RtcDir, "LAYOUTS", $"{(show ? "_" : "")}Java Corruptor.txt");
            
            if (File.Exists(path2))
            {
                Logging.GlobalLogger.Info($"Java Corruptor layout is {(show ? "" : "not ")}hidden, {(show ? "un" : "")}hiding it.");
                if (File.Exists(path1))
                    File.Delete(path1);
                File.Move(path2, path1);
            }
            if (show && !UICore.mtForm.cbSelectBox.Items.Contains(PluginForm))
                UICore.mtForm.cbSelectBox.Items.Add(PluginForm);
            else if (!show && UICore.mtForm.cbSelectBox.Items.Contains(PluginForm))
                UICore.mtForm.cbSelectBox.Items.Remove(PluginForm);
        }
    }
}