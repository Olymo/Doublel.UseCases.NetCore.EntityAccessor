using Doublel.EntityAccessor;
using Doublel.UseCases;

namespace Dobulel.UseCases.EntityAccessor
{
    public abstract class EfCommandUseCaseHandler<TUseCase, TData, TEntity> : IUseCaseHandler<TUseCase, TData, Empty>
        where TUseCase : UseCase<TData, Empty>
        where TEntity : Entity
    {
        protected Doublel.EntityAccessor.EntityAccessor Accessor { get; }

        protected EfCommandUseCaseHandler(Doublel.EntityAccessor.EntityAccessor accessor) => Accessor = accessor;

        public abstract Empty Handle(TUseCase useCase);
    }
}
