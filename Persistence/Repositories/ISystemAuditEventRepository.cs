using Domain;

namespace Persistence.Repositories
{
    public class SystemAuditEventRepository : ISystemAuditEventRepository
    {
        private readonly HaSpManContext _context;

        public SystemAuditEventRepository(HaSpManContext context)
        {
            _context = context;
        }
        public void Add(SystemAuditEvent systemAuditEvent)
        {
            //_context.SystemAuditEvents.Add(systemAuditEvent);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }
    }
    public interface ISystemAuditEventRepository
    {
        void Add(SystemAuditEvent systemAuditEvent);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}