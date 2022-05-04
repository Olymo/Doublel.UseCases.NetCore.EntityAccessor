using Doublel.EntityAccessor;
using Doublel.UseCases;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Dobulel.UseCases.EntityAccessor;
using AutoMapper;

namespace Dobulel.UseCases.NetCore.EntityAccessor
{
    public class GenericUseCaseMediator
    {
        private readonly IServiceProvider _provider;

        public GenericUseCaseMediator(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Empty Deactivate<TEntity, TUseCase>(TUseCase useCase)
            where TUseCase : UseCase<int, Empty>
            where TEntity : Entity
        {
            var executor = _provider.GetService<UseCaseExecutor<TUseCase, int, Empty>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var handler = new EfGenericDeactivateHandler<TUseCase, TEntity>(accessor);

            return executor.ExecuteUseCase(useCase, handler);
        }

        public Empty Insert<TEntity, TData, TUseCase>(TUseCase useCase)
            where TUseCase : UseCase<TData, Empty>
            where TData : class
            where TEntity : Entity
        {
            var executor = _provider.GetService<UseCaseExecutor<TUseCase, TData, Empty>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var mapper = _provider.GetService<IMapper>();
            var handler = new EfGenericInsertHandler<TUseCase, TData, TEntity>(accessor, mapper);

            return executor.ExecuteUseCase(useCase, handler);
        }

        public Empty InsertBatch<TEntity, TData, TUseCase>(TUseCase useCase)
            where TUseCase : UseCase<IEnumerable<TData>, Empty>
            where TData : class
            where TEntity : Entity
        {
            var executor = _provider.GetService<UseCaseExecutor<TUseCase, IEnumerable<TData>, Empty>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var mapper = _provider.GetService<IMapper>();
            var handler = new EfGenericBatchInsertHandler<TUseCase, TData, TEntity>(accessor, mapper);

            return executor.ExecuteUseCase(useCase, handler);
        }

        public Empty Update<TEntity, TData, TUseCase>(TUseCase useCase)
            where TUseCase : UseCase<TData, Empty>
            where TData : class, IIdentifyable
            where TEntity : Entity
        {
            var executor = _provider.GetService<UseCaseExecutor<TUseCase, TData, Empty>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var mapper = _provider.GetService<IMapper>();
            var handler = new EfGenericUpdateHandler<TUseCase, TData, TEntity>(accessor, mapper);

            return executor.ExecuteUseCase(useCase, handler);
        }

        public TResult Find<TEntity, TResult, TUseCase>(TUseCase useCase)
            where TEntity : Entity
            where TUseCase : UseCase<int, TResult>
            where TResult : class
        {
            var executor = _provider.GetService<NoValidationUseCaseExecutor<TUseCase, int, TResult>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var mapper = _provider.GetService<IMapper>();
            var handler = new EfGenericFindHandler<TUseCase, TResult, TEntity>(mapper, accessor);

            return executor.ExecuteUseCase(useCase, handler);
        }

        public object Search<TEntity, TResult, TUseCase>(TUseCase useCase)
            where TEntity : Entity
            where TUseCase : UseCase<ISearchObject, object>
            where TResult : class
        {
            var executor = _provider.GetService<NoValidationUseCaseExecutor<TUseCase, ISearchObject, object>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var mapper = _provider.GetService<IMapper>();
            var handler = new EfGenericSearchHandler<TUseCase, TResult, TEntity>(accessor, mapper);

            return executor.ExecuteUseCase(useCase, handler);
        }

        public Empty Delete<TEntity, TUseCase>(TUseCase useCase)
            where TUseCase : UseCase<int, Empty>
            where TEntity : Entity
        {
            var executor = _provider.GetService<UseCaseExecutor<TUseCase, int, Empty>>();
            var accessor = _provider.GetService<Doublel.EntityAccessor.EntityAccessor>();
            var handler = new EfGenericDeleteHandler<TUseCase, TEntity>(accessor);

            return executor.ExecuteUseCase(useCase, handler);
        }
    }
}
