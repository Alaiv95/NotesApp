using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Tests.Unit.Common;

namespace Notes.Tests.Unit.Notes.Queries;

public class GetNoteDetailQueryHandler : QueryTestFixture
{
    [Test]
    public async Task GetNoteDetail_Success()
    {
        // Arrange
        var handler = new GetNoteDetailsQueryHandler(Context, Mapper);
        Guid noteId = Guid.Parse("b195f295-aeb8-4fee-8b41-5d68f73bf1d8");

        // Act
        var result = await handler.Handle(new GetNoteDetailsQuery
        {
            Id = noteId,
            UserId = NotesContextFactory.UserBId
        }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.That(result.CreationDate, Is.EqualTo(DateTime.Today));
        Assert.That(result.Details, Is.EqualTo("Details2"));
        Assert.That(result.Title, Is.EqualTo("Title2"));
        Assert.That(result.Id, Is.EqualTo(noteId));
    }
}
