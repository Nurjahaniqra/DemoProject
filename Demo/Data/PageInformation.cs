using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Demo.Data
{
    public class PageInformation<T>
    {
        public List<T> Items { get; set; }
        public int TotalItem {  get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }

        public PageInformation(List<T> Item,int count,int Size, int pageNumber)
        {
            Items = Item;
			PageNumber = pageNumber;
			TotalPage = (int)Math.Ceiling((decimal)count / (decimal)Size);
            
        }
        public static PageInformation<T> Create(IQueryable<T> source,int pageNumber,int pageSize)
        {
			
            var count = source.Count();
            var item = source.Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToList();
            return new PageInformation<T>(item,count,pageSize, pageNumber);
        }

        public bool HasPreviousPage => (PageNumber> 1);
        public bool NextPreviousPage=> (PageNumber < TotalPage);

    }
}
