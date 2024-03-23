namespace Fiorello.Models.Demo
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookLibrary> BookLibraries { get; set; }
    }
}
