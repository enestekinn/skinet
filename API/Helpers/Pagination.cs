using System.Collections.Generic;

namespace API.Helpers
{
public class Pagination<T> where T : class
{
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
    public int PageSize { get; set; }
    /*
     we want this value after filter have been applied.For example  if sb asks for a list of boots and
     they ask for a page size of two , we want to know how many boots are available in the
     entire collection.
     */
    public int Count { get; set; } 
    public IReadOnlyList<T> Data { get; set; }
}
}