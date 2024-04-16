using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Constants
{
    public static class Paths
    {
        private static string _workingDirectory = Environment.CurrentDirectory;
        private static string _projectDirectory = Directory.GetParent(_workingDirectory).Parent.FullName;
        public static string TtsPythonScriptPath = $"{_projectDirectory}\\AIStoryteller_Python\\main.py";
        public static string TempAudioPath = $"{_projectDirectory}\\TempAudioFiles\\VoiceModelNo";
    }
}
