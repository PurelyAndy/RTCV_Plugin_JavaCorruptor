using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using NLog;
using ObjectWeb.Asm;
using ObjectWeb.Asm.Tree;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI.Modular;

namespace Java_Corruptor.UI;

public partial class PluginForm : ComponentForm
{
    public Java_Corruptor plugin;
    public volatile bool HideOnClose = true;
    Logger logger = LogManager.GetCurrentClassLogger();

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