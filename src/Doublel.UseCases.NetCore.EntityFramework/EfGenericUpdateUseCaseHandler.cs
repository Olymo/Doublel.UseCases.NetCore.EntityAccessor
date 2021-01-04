using AutoMapper;
using Doublel.EntityAccessor;
using Doublel.EntityAccessor.Exceptions;
using Doublel.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfGenericUpdateHandler<TUseCase, TData, TEntity> : EfCommandUseCaseHandler<TUseCase, TData, TEntity>
        where TUseCase : UseCase<TData, Empty>
        where TEntity : Entity
        where TData : IIdentifyable
    {
        private readonly IMapper _mapper;
        public EfGenericUpdateHandler(Doublel.EntityAccessor.EntityAccessor accessor, IMapper mapper)
            : base(accessor) => _mapper = mapper;

        public override Empty Handle(TUseCase useCase)
        {
            var itemToUpdate = Accessor.Find<TEntity>(useCase.Data.Identifier, false);

            if (itemToUpdate == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), useCase.Data.Identifier);

            }

            _mapper.Map(useCase.Data, itemToUpdate);

            Accessor.Update(itemToUpdate);

            Accessor.Commit();

            return Empty.Value;
        }
    }
}
