using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>
{
    private INotesDbContext _notesDbContext;

    public UpdateNoteCommandHandler(INotesDbContext notesDbContext) => _notesDbContext = notesDbContext;

    public async Task<Unit> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _notesDbContext.Notes.FirstOrDefaultAsync(n => n.Id == command.Id, cancellationToken);

        if (entity == null || entity.UserId != command.UserId) 
        {
            throw new NotFoundException(nameof(Note), command.Id);
        }

        entity.Details = command.Details;
        entity.Title = command.Title;
        entity.EditDate = DateTime.Now;

        await _notesDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
