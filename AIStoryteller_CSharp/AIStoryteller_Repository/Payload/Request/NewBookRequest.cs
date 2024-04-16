namespace AIStoryteller_Repository.Payload.Request
{
    public class NewBookRequest
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public Stream TextData { get; set; }
    }
}
