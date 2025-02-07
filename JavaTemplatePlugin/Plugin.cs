using System;
using System.ComponentModel.Composition;
using FileStub.Templates.PluginHost;

namespace JavaTemplatePlugin
{
    [Export(typeof(IFileStubPlugin))]
    public class Plugin : IFileStubPlugin
    {
        public string Name => "Java Template";
        public string Description => "Targets .JAR files in FileStub, with presets for Minecraft and Project Zomboid";
        public string Author => "PurelyAndy";
        public Version Version => new(1, 1, 0);
        public bool Start() => true;
    }
}