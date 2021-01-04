using Doublel.DynamicQueryBuilder.Search;
using Doublel.EntityAccessor;

namespace Dobulel.UseCases.EntityAccessor
{
    public interface ISearchObject : IPagedSearch
    {
        public ActivityLevel ActivityLevel { get; set; }
    }

    public class DefaultSearch : ISearchObject
    {
        public ActivityLevel ActivityLevel { get; set; } = ActivityLevel.Active;

        public int PerPage { get; set; } = 10;

        public int Page { get; set; } = 1;

        public bool Paginate { get; set; } = false;
    }

    public class SortablePagedSearch : Doublel.DynamicQueryBuilder.Search.SortablePagedSearch, ISearchObject
    {
        public ActivityLevel ActivityLevel { get; set; }
    }
}
