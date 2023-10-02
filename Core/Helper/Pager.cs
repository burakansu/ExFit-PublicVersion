namespace Core.Helper
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public int ShowingRangeStart { get; private set; }
        public int ShowingRangeEnd { get; private set; }
        public string SearchText { get; set; }
        public string OrderBy { get; set; }
        public string OrderWay { get; set; }

        public Pager()
        {

        }
        public Pager(int totalItems, int page, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 3;
            int endPage = currentPage + 2;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 5)
                    startPage = endPage - 4;
                
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            ShowingRangeStart = CurrentPage * PageSize - PageSize + 1;
            ShowingRangeEnd = ShowingRangeStart + PageSize - 1;

            if (ShowingRangeEnd > TotalItems)
                ShowingRangeEnd = TotalItems;
        }
    }
}
