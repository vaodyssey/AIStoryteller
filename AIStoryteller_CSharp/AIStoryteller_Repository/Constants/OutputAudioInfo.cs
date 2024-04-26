using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Constants
{
    public class OutputAudioInfo
    {
        public static string OUTPUT_FILE_NAME(int fileNumber)
        {
            return $"output{fileNumber}.mp4";
        }
    }
}
