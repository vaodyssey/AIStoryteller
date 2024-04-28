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
        public static string PythonProjectPath= $"{_projectDirectory}\\AIStoryteller_Python";
        public static string TtsPythonScriptPath = $"{PythonProjectPath}\\main.py";
        public static string ActivatePythonVenvScriptPath= $".venv\\Scripts\\activate.bat";
        public static string TtsAudioOutputPath = $"{_projectDirectory}\\TempAudioFiles\\tts";
        public static string RvcAudioOutputPath = $"{_projectDirectory}\\TempAudioFiles\\rvc";
    }
}
