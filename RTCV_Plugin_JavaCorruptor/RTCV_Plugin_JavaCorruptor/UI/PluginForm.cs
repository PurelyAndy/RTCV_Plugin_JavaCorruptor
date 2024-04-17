using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Java_Corruptor.BlastClasses;
using Java_Corruptor.UI.Components.EngineControls;
using Java_Corruptor.UI.Components;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using NLog;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.Common;
using RTCV.Common.CustomExtensions;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;

namespace Java_Corruptor.UI;

public partial class PluginForm : ComponentForm
{
    public Java_Corruptor plugin;
    public volatile bool HideOnClose = true;
    Logger logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly CommonOpenFileDialog _cofdOutputFolder = new()
    {
        IsFolderPicker = true,
    };
        
    public PluginForm(Java_Corruptor _plugin)
    {
        plugin = _plugin;
        
        InitializeComponent();
        FormClosing += JavaCorruptorForm_FormClosing;

        Text = "Java Corruptor";
        lbVersion.Text = plugin.Version.ToString();
    }

    private void JavaCorruptorForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!HideOnClose)
            return;
        e.Cancel = true;
        Hide();
    }
}