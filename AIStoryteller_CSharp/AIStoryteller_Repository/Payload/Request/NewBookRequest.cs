using System.ComponentModel.DataAnnotations;

namespace AIStoryteller_Repository.Payload.Request
{
    public class NewBookRequest
    {
        [Required(ErrorMessage ="You must input the book's name.")]        
        public string Name { get; set; }
        [Required(ErrorMessage = "You must input the author's name.")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "You must input the book's description.")]
        public string Description { get; set; }
        public long Size { get; set; }
        public Stream TextData { get; set; }
    }
}
