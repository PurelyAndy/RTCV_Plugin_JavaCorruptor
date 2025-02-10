using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Java_Corruptor.UI.Components;
using RTCV.Common;
using RTCV.NetCore;

namespace Java_Corruptor
{
    public class LaunchScript
    {
        public List<ScriptStage> Stages { get; set; } = [];
        public LaunchScript() { }

        public class ScriptStage
        {
            public string Program { get; set; }
            public string Arguments { get; set; }
            public bool ShowOutput { get; set; }
        }

        public void Execute()
        {
            foreach (ScriptStage stage in Stages)
            {
                Process p = new()
                {
                    StartInfo = new()
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        WorkingDirectory = Path.GetDirectoryName(stage.Program)!,
                    },
                };
                if (stage.Program.EndsWith(".exe"))
                {
                    p.StartInfo.FileName = stage.Program;
                    p.StartInfo.Arguments = stage.Arguments;
                }
                else
                {
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.Arguments = $"/c {stage.Program} {stage.Arguments}";
                }

                if (stage.ShowOutput)
                {
                    p.OutputDataReceived += (_, args) =>
                    {
                        SyncObjectSingleton.FormExecute(form =>
                        {
                            form.tbOutput.SelectionStart = form.tbOutput.TextLength;
                            form.tbOutput.SelectionLength = 0;

                            form.tbOutput.SelectionColor = Color.White;
                            form.tbOutput.AppendText(args.Data + Environment.NewLine);
                            form.tbOutput.SelectionColor = form.tbOutput.ForeColor;
                            form.tbOutput.ScrollToCaret();
                        }, S.GET<JavaGeneralParametersForm>());
                    };
                    p.ErrorDataReceived += (_, args) =>
                    {
                        SyncObjectSingleton.FormExecute(form =>
                        {
                            form.tbOutput.SelectionStart = form.tbOutput.TextLength;
                            form.tbOutput.SelectionLength = 0;

                            form.tbOutput.SelectionColor = Color.FromArgb(0xff, 0x40, 0x40);
                            form.tbOutput.AppendText(args.Data + Environment.NewLine);
                            form.tbOutput.SelectionColor = form.tbOutput.ForeColor;
                            form.tbOutput.ScrollToCaret();
                        }, S.GET<JavaGeneralParametersForm>());
                    };
                }

                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.WaitForExit();
            }
        }
    }
}
