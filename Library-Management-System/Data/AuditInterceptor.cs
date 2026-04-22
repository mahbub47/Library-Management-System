using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Library_Management_System.Data;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        var entries = context.ChangeTracker.Entries().Where(
            e => e.State == EntityState.Added
            || e.State == EntityState.Modified
            || e.State == EntityState.Deleted);

        var auditLogs = new List<AuditLog>();

        foreach ( var entry in entries )
        {
            if (entry is AuditLog)
                continue;
            auditLogs.Add(new AuditLog
            {
                EntityName = entry.Entity.GetType().Name,
                Action = entry.State.ToString(),
                OccuredAt = DateTime.UtcNow
            });
        }

        context.Set<AuditLog>().AddRange(auditLogs);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
