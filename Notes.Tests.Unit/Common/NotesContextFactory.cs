using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;

namespace Notes.Tests.Unit.Common;

public class NotesContextFactory
{
    public static Guid UserAId = Guid.NewGuid();
    public static Guid UserBId = Guid.NewGuid();

    public static Guid NoteIdForDelete = Guid.NewGuid();
    public static Guid NoteIdForUpdate = Guid.NewGuid();

    public static NotesDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NotesDbContext(options);
        context.Database.EnsureCreated();
        context.Notes.AddRange(
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Title = "Title1",
                    Id = Guid.Parse("71046a51-cf19-4265-9231-dd8f48d07969"),
                    UserId = UserAId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Title = "Title2",
                    Id = Guid.Parse("b195f295-aeb8-4fee-8b41-5d68f73bf1d8"),
                    UserId = UserBId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Title = "Title3",
                    Id = NoteIdForDelete,
                    UserId = UserAId
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Title = "Title4",
                    Id = NoteIdForUpdate,
                    UserId = UserBId
                }
            );
        context.SaveChanges();
        return context;
    }

    public static void Destroy(NotesDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
