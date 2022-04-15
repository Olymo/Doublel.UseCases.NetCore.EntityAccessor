using Doublel.UseCases;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Dobulel.UseCases.EntityAccessor
{
    public class EfUseCaseLogRepository : IUseCaseLogRepository
    {
        private readonly UseCasesDbContext _context;
        private readonly IApplicationActor _actor;

        public EfUseCaseLogRepository(UseCasesDbContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public PagedResult<UseCaseLog> GetLogs(IUseCaseLogSearch search)
        {
            var query = _context.UseCaseLogs.AsQueryable();

            if(search.ActorId.HasValue)
            {
                query = query.Where(x => (int)x.ActorId == search.ActorId.Value);
            }

            if(search.StartDate.HasValue)
            {
                query = query.Where(x => x.ExecutedTime >= search.StartDate);
            }

            if (search.EndDate.HasValue)
            {
                query = query.Where(x => x.ExecutedTime <= search.EndDate);
            }

            if(search.Status.HasValue)
            {
                query = query.Where(x => x.Status == (UseCaseExecutionStatus) search.Status.Value);
            }

            if(!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseId.Contains(search.UseCaseName));
            }

            var resultQueryPaged = query.OrderByDescending(x => x.ExecutedTime);

            var itemsToSkip = search.PerPage * (search.Page - 1);

            return new PagedResult<UseCaseLog>
            {
                TotalCount = resultQueryPaged.Count(),
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                Items = resultQueryPaged.Skip(itemsToSkip).Take(search.PerPage).ToList()
            };
        }

        public void Log(UseCaseLog log)
        {
            _context.UseCaseLogs.Add(new UseCaseLog
            {
                ActorId = (int)log.ActorId,
                Id = log.Id,
                ExecutedTime = log.ExecutedTime,
                ActorIdentity = log.ActorIdentity,
                Status = log.Status,
                UseCaseData = JsonConvert.SerializeObject(log.UseCaseData),
                UseCaseId = log.UseCaseId
            });

            _context.SaveChanges();
        }

        public void UpdateStatus(Guid id, UseCaseExecutionStatus status)
        {
            var log = _context.UseCaseLogs.Find(id);
            if (log == null)
            {
                return;
            }
            log.Status = status;

            _context.SaveChanges();
        }
    }
}
