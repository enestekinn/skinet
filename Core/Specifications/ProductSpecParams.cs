namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50; // we have only 18 
        public int PageIndex { get; set; } = 1; // by defaults return first page
        private int _pageSize = 6;
        public int PageSize {
            // value (gelen deger) MaxPageSize dan buyukse MaxPageSize i doner kucukse value yu doner.
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; 
        }

        public int? BrandId {get; set;}
        public int? TypeId  {get; set;}
        public string Sort { get; set; }

        // we have to always convert to lowercase
        private string _search;
        public string Search  {
            get => _search;
            set => _search = value.ToLower();
        }

        
    }
}