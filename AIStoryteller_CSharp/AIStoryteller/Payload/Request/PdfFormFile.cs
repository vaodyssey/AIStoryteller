

namespace AIStoryteller.Payload.Request
{
    public class PdfFormFile : IFormFile
    {
        public string ContentType { get; }
        public string ContentDisposition { get; }
        public IHeaderDictionary Headers { get; }
        public long Length { get; }
        public string Name { get; }
        public string FileName { get; }
        public Stream OpenReadStream() => Stream;

        void IFormFile.CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        Task IFormFile.CopyToAsync(Stream target, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Stream Stream { get; }

        public PdfFormFile(string name, string contentType, Stream stream, long length)
        {
            Name = name;
            ContentType = contentType;
            Stream = stream;
            Length = length;
            FileName = name;
        }
    }
}

