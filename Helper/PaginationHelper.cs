namespace Fiorello.Helper
{
    public class PaginationHelper<T>
    {
        public PaginationHelper( int currentPage,int totalPage,IEnumerable<T> items)
        {
            
            CurrentPage= currentPage;
            TotalPage= totalPage;
            Items= items;
        }
        public PaginationHelper()
        {
            
        }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
