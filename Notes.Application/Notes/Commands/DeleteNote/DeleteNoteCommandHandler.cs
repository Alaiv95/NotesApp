using MediatR;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
{
    private INotesDbContext _notesDbContext;

    public DeleteNoteCommandHandler(INotesDbContext notesDbContext) => _notesDbContext = notesDbContext;

    public async Task<Unit> Handle(DeleteNoteCommand command, CancellationToken token)
    {
        var entity = await _notesDbContext.Notes.FindAsync(new object[] {command.Id}, token);

        if (entity == null || entity.UserId != command.UserId)
        {
            throw new NotFoundException(nameof(Note), command.Id);
        }

        _notesDbContext.Notes.Remove(entity);
        await _notesDbContext.SaveChangesAsync(token);

        return Unit.Value;
    }
}
