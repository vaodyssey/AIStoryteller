using AIStoryteller_Repository.Constants;
using Microsoft.AspNetCore.Components.Forms;

namespace AIStoryteller.Utils
{
    public static class FileConvertUtils
    {
        public static async Task<byte[]> ConvertIBrowserFileToByteArray(IBrowserFile browserFile)
        {

            var fileStream = browserFile.OpenReadStream(maxAllowedSize: BookFileInfo.MAX_ALLOWED_SIZE);
            using (var resultStream = new MemoryStream())
            {
                await fileStream.CopyToAsync(resultStream);
                return resultStream.ToArray();
            }
        }
        public static Task<string> ConvertByteArrayToBase64(string mediaType, byte[] data)
        {
            return Task.Run(() =>
            {
                var result = $"data:{mediaType};base64,{Convert.ToBase64String(data)}";
                return result;
            });
        }
    }
}
