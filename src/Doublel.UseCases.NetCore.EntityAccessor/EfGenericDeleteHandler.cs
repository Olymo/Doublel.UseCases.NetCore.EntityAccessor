using Dobulel.UseCases.EntityAccessor;
using Doublel.EntityAccessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doublel.UseCases.NetCore.EntityAccessor
{
    public class EfGenericDeleteHandler<TUseCase, TEntity> : EfCommandUseCaseHandler<TUseCase, int, TEntity>
        where TUseCase : UseCase<int, Empty>
        where TEntity : Entity
    {
        public EfGenericDeleteHandler(Doublel.EntityAccessor.EntityAccessor accessor) : base(accessor)
        {
        }

        public override Empty Handle(TUseCase useCase)
        {
            Accessor.Remove<TEntity>(useCase.Data);
            Accessor.Commit();
            return Empty.Value;
        }
    }
}
