using AutoMapper;
using Doublel.EntityAccessor;
using Doublel.UseCases;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfGenericInsertHandler<TUseCase, TData, TEntity> : EfCommandUseCaseHandler<TUseCase, TData, TEntity>
        where TUseCase : UseCase<TData, Empty>
        where TEntity : Entity
        where TData : class
    {
        private readonly IMapper _mapper;
        public EfGenericInsertHandler(Doublel.EntityAccessor.EntityAccessor accessor, IMapper mapper)
            : base(accessor) => _mapper = mapper;

        public override Empty Handle(TUseCase useCase)
        {
            var entityToInsert = _mapper.Map<TEntity>(useCase.Data);

            Accessor.Add(entityToInsert);

            Accessor.Commit();

            return Empty.Value;
        }
    }
}
