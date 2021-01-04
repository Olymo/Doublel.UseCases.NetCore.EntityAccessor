using AutoMapper;
using Doublel.DynamicQueryBuilder;
using Doublel.EntityAccessor;
using Doublel.QueryableBuilder;
using Doublel.QueryableBuilder.Extensions;
using Doublel.UseCases;
using System.Collections.Generic;
using System.Linq;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfGenericSearchHandler<TUseCase, TResult, TEntity> : IUseCaseHandler<TUseCase, ISearchObject, object>
        where TUseCase : UseCase<ISearchObject, object>
        where TEntity : Entity
        where TResult : class
    {
        private readonly Doublel.EntityAccessor.EntityAccessor _accessor;
        private readonly IMapper _mapper;

        public EfGenericSearchHandler(Doublel.EntityAccessor.EntityAccessor accessor, IMapper mapper)
        {
            _accessor = accessor;
            _mapper = mapper;
        }

        public object Handle(TUseCase useCase)
        {
            var query = _accessor.GetQuery<TEntity>(useCase.Data.ActivityLevel);

            var foundItems = query.BuildQuery(useCase.Data);

            return useCase.Data.Paginate ? GeneratePagedResponse(useCase, foundItems) : _mapper.Map<IEnumerable<TResult>>(foundItems.ToList());
        }

        private object GeneratePagedResponse(TUseCase useCase, IQueryable<TEntity> query)
        {
            var queryPaged = query.AsPagedResponse(useCase.Data.PerPage, useCase.Data.Page);
            return new PagedResponse<TResult>
            {
                CurrentPage = queryPaged.CurrentPage,
                Items = _mapper.Map<IEnumerable<TResult>>(queryPaged.Items),
                ItemsPerPage = queryPaged.ItemsPerPage,
                TotalCount = queryPaged.TotalCount
            };
        }
    }
}
