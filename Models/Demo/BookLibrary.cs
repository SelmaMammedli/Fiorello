namespace Fiorello.Models.Demo
{
    public class BookLibrary
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
