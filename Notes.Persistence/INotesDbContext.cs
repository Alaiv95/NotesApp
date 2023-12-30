using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Persistence;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
