using AutoMapper;
using Doublel.EntityAccessor;
using Doublel.EntityAccessor.Exceptions;
using Doublel.UseCases;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfGenericFindHandler<TUseCase, TResult, TEntity> : IUseCaseHandler<TUseCase, int, TResult>
        where TUseCase : UseCase<int, TResult>
        where TEntity : Entity
    {
        private readonly IMapper _mapper;
        private readonly Doublel.EntityAccessor.EntityAccessor _accessor;

        public EfGenericFindHandler(IMapper mapper, Doublel.EntityAccessor.EntityAccessor context)
        {
            _mapper = mapper;
            _accessor = context;
        }

        public TResult Handle(TUseCase useCase)
        {
            var foundEntity = _accessor.Find<TEntity>(useCase.Data, false);

            return foundEntity == null
                ? throw new EntityNotFoundException(typeof(TEntity), useCase.Data)
                : _mapper.Map<TEntity, TResult>(foundEntity);
        }
    }
}
