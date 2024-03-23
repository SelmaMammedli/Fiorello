namespace Fiorello.Models.Demo
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookLibrary> BookLibraries { get; set; }
    }
}
