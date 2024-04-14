namespace AIStoryteller_Repository.DTO
{
    public class NewBookDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public Stream TextData { get; set; }
    }
}
