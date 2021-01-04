using AutoMapper;
using Doublel.EntityAccessor;
using Doublel.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfGenericBatchInsertHandler<TUseCase, TData, TEntity> : EfCommandUseCaseHandler<TUseCase, IEnumerable<TData>, Entity>
            where TUseCase : UseCase<IEnumerable<TData>, Empty>
            where TEntity : Entity
    {
        private readonly IMapper _mapper;
        private readonly Doublel.EntityAccessor.EntityAccessor _accessor;
        public EfGenericBatchInsertHandler(Doublel.EntityAccessor.EntityAccessor accessor, IMapper mapper) : base(accessor)
        {
            _mapper = mapper;
            _accessor = accessor;
        }

        public override Empty Handle(TUseCase useCase)
        {
            var entitiesToInsert = _mapper.Map<IEnumerable<TEntity>>(useCase.Data);

            _accessor.AddRange(entitiesToInsert);

            _accessor.Commit();

            return Empty.Value;
        }
    }
}
