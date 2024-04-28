using System.Text.RegularExpressions;

namespace AIStoryteller_Repository.Utils
{
    public static class TextUtils
    {
        public static string CleanAnomaliesFromText(string text)
        {
            
            string noSpaceOrTabText= Regex.Replace(text, @"\t|\n|\r", " ");
            string noQuotesText = Regex.Replace(noSpaceOrTabText, "[\"“”]", " ");
            return noQuotesText;
        }
    }
}
