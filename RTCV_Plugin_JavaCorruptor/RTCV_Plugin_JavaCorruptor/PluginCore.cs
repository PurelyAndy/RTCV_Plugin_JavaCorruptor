using Java_Corruptor.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.IO;
using Java_Corruptor.Javanguard;
using RTCV.CorruptCore;
using System.Reflection;
using System.Linq;
using RTCV.UI.Modular;

namespace Java_Corruptor;

[Export(typeof(IPlugin))]
public class Java_Corruptor : IPlugin
{
    //-------[ Plugin metadata ]-------

    // >>> Make sure you rename BOTH the namespace and class (Very important)
    public string Description => "Corrupt Java programs with ease!";
    public string Author => "PurelyAndy";
    public Version Version => new(1, 1, 0);

    //-----[ Plugin loading workflow ]-----

    //Server is StandaloneRTC process (RTCV UI)
    //Client is Emulator process

    //Tells on which sides the plugin has to load
    public RTCSide SupportedSide => RTCSide.Server;   

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

        if (!Params.IsParamSet("JAVA_METHOD_COMPUTE"))
            Params.SetParam("JAVA_METHOD_COMPUTE", ObjectWeb.Asm.ClassWriter.Compute_Maxs.ToString());
        if (!Params.IsParamSet("JAVA_COMPRESS_JAR"))
            Params.SetParam("JAVA_COMPRESS_JAR", "False");
        if (!Params.IsParamSet("JAVA_CORRUPTION_THREADS"))
            Params.SetParam("JAVA_CORRUPTION_THREADS", "4");

        CorruptionOptions.MethodCompute = int.Parse(Params.ReadParam("JAVA_METHOD_COMPUTE"));
        CorruptionOptions.CompressJar = bool.Parse(Params.ReadParam("JAVA_COMPRESS_JAR"));
        CorruptionOptions.Threads = int.Parse(Params.ReadParam("JAVA_CORRUPTION_THREADS"));

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
        
        
        AllSpec.VanguardSpec.SpecUpdated += HandleSpecChanges;
        HandleSpecChanges(null, new(AllSpec.VanguardSpec.GetPartialSpec(), true));
        
        Logging.GlobalLogger.Info($"{Name} v{Version} initialized.");
        CurrentSide = side;
        return true;
    }

    private static void HandleSpecChanges(object _, SpecUpdateEventArgs eas)
    {
        MemoryDomainProxy[] memoryDomains = (MemoryDomainProxy[])eas.partialSpec[VSPEC.MEMORYDOMAINS_INTERFACES];
        string vanguardName = (string)eas.partialSpec[VSPEC.NAME];
        
        if (!string.IsNullOrEmpty(vanguardName))
        {
            switch (vanguardName)
            {
                case "FileStub":
                    CorruptModeInfo.Live = false;
                    ChangePluginVisibility(true);
                    Logging.GlobalLogger.Info("Java Corruptor connected to FileStub.");
                    break;
                case "JavaStubProxy":
                    CorruptModeInfo.Live = true;
                    JavaConnector.StartListening();
                    Logging.GlobalLogger.Info("Java Corruptor connected to JavaStub.");
                    ChangePluginVisibility(true);
                    break;
                default:
                    CorruptModeInfo.Live = false;
                    Logging.GlobalLogger.Info(
                        $"Not connected to FileStub or JavaStub ({vanguardName}), Java Corruptor plugin will be hidden.");
                    ChangePluginVisibility(false);
                    break;
            }
        }
        
        if (memoryDomains is null) return;
        if (!CorruptModeInfo.Live && (memoryDomains.Length != 1 || !memoryDomains[0].Name.EndsWith(".jar")))
        {
            if (memoryDomains.Length < 1)
                Logging.GlobalLogger.Info("No files were loaded, Java Corruptor plugin will be hidden.");
            else
                Logging.GlobalLogger.Info("Multiple files or a non-jar file was loaded, Java Corruptor plugin will be hidden.");
            ChangePluginVisibility(false);
        }
        else
            ChangePluginVisibility(true);
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
        return new(a);
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

    private static void ChangePluginVisibility(bool show)
    {
        string toPath = Path.Combine(RtcCore.RtcDir, "LAYOUTS", $"{(show ? "" : "_")}Java Corruptor.txt");
        string fromPath = Path.Combine(RtcCore.RtcDir, "LAYOUTS", $"{(show ? "_" : "")}Java Corruptor.txt");
            
        if (File.Exists(fromPath))
        {
            Logging.GlobalLogger.Info($"Java Corruptor layout is {(show ? "" : "not ")}hidden, {(show ? "un" : "")}hiding it.");
            if (File.Exists(toPath))
                File.Delete(toPath);
            File.Move(fromPath, toPath);
        }
        if (show && !UICore.mtForm.cbSelectBox.Items.Contains(PluginForm))
            UICore.mtForm.cbSelectBox.Items.Add(PluginForm);
        else if (!show && UICore.mtForm.cbSelectBox.Items.Contains(PluginForm))
            UICore.mtForm.cbSelectBox.Items.Remove(PluginForm);
        /*
        Type t = typeof(CanvasGrid);
        MethodInfo enabledLayoutsMeth = t.GetMethod("GetEnabledCustomLayouts", BindingFlags.NonPublic | BindingFlags.Static);
        FileInfo[] layouts = (FileInfo[])enabledLayoutsMeth.Invoke(null, null);
        FileInfo layout = layouts.FirstOrDefault(l => l.Name == "Java Corruptor.txt");
        if (layout is not null)
        {
            MethodInfo loadLayout = t.GetMethod("LoadCustomLayout", BindingFlags.NonPublic | BindingFlags.Static);
            loadLayout.Invoke(null, [layout.FullName]);
        }
        else
        {
            DefaultGrids.engineConfig.LoadToMainP();
        }
        */ //runs too early
    }
}