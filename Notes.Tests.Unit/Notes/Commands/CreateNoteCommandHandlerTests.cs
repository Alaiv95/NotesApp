using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Tests.Unit.Common;

namespace Notes.Tests.Unit.Notes.Commands;

public class CreateNoteCommandHandlerTests : TestCommandBase
{
    [Test]
    public async Task CreateNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new CreateNoteCommandHandler(Context);
        var noteName = "name";
        var noteDetails = "details";

        // act
        var createdNote = await handler.Handle(
            new CreateNoteCommand
            {
                Title = noteName,
                Details = noteDetails,
                UserId = NotesContextFactory.UserAId
            },
            CancellationToken.None);

        // Assert
        Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
            note.Id == createdNote.Id && note.Title == noteName && note.Details == noteDetails));
    }
}
