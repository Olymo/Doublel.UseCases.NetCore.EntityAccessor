using Doublel.EntityAccessor;
using Doublel.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfGenericDeactivateHandler<TUseCase, TEntity> : EfCommandUseCaseHandler<TUseCase, int, TEntity>
        where TUseCase : UseCase<int, Empty>
        where TEntity : Entity
    {
        public EfGenericDeactivateHandler(Doublel.EntityAccessor.EntityAccessor accessor) : base(accessor)
        {
        }

        public override Empty Handle(TUseCase useCase)
        {
            Accessor.Deactivate<TEntity>(useCase.Data);
            Accessor.Commit();
            return Empty.Value;
        }
    }
}
