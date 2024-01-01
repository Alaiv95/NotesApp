using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Tests.Unit.Common;

namespace Notes.Tests.Unit.Notes.Commands;

public class DeleteNoteCommandHandlerTests : TestCommandBase
{
    [Test]
    public async Task DeleteNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);

        // Act
        await handler.Handle(new DeleteNoteCommand
        {
            Id = NotesContextFactory.NoteIdForDelete,
            UserId = NotesContextFactory.UserAId
        },
        CancellationToken.None);

        // Assert
        Assert.Null(Context.Notes.SingleOrDefault(note =>
            note.Id == NotesContextFactory.NoteIdForDelete));
    }

    [Test]
    public async Task DeleteNoteCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteNoteCommandHandler(Context);

        // Act
        // Assert
        Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None));
    }

    [Test]
    public async Task DeleteNoteCommandHandler_FailOnWronUserId()
    {
        // Arrange
        var deleteHandler = new DeleteNoteCommandHandler(Context);
        var createHandler = new CreateNoteCommandHandler(Context);
        var createdNote = await createHandler.Handle(
                new CreateNoteCommand
                {
                    Title = "Random",
                    Details = "Random",
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None
            );

        // Act
        // Assert
       Assert.ThrowsAsync<NotFoundException>(async () =>
            await deleteHandler.Handle(
                new DeleteNoteCommand
                {
                    Id = createdNote.Id,
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None));
    }
}
