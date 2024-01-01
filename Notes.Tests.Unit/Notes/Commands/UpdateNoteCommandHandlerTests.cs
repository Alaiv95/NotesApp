using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Unit.Common;

namespace Notes.Tests.Unit.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Test]
        public async Task UpdateNoteCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            string newTitle = "newTitle";
            string newDetails= "newDetails";

            // Act
            var updatedNote = await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = newTitle,
                Details = newDetails
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note => 
                note.Id == NotesContextFactory.NoteIdForUpdate &&
                note.Title == newTitle && note.Details == newDetails));
        }

        [Test]
        public async Task UpdateNoteCommandHandler_FailWrongId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            string newTitle = "newTitle";

            // Act
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateNoteCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserBId,
                    Title = newTitle
                }, CancellationToken.None));
        }

        [Test]
        public async Task UpdateNoteCommandHandler_FailWrongUserId()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(Context);
            string newTitle = "newTitle";

            // Act
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateNoteCommand
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserAId,
                    Title = newTitle
                }, CancellationToken.None));
        }
    }
}
