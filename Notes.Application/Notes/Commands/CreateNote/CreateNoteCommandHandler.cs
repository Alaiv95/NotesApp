using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.WebApi.Models;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, CreationResponseVm>
{
    private readonly INotesDbContext _dbContext;

    public CreateNoteCommandHandler(INotesDbContext dbContext) => _dbContext = dbContext;

    public async Task<CreationResponseVm> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            UserId = command.UserId,
            Title = command.Title,
            Details = command.Details,
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            EditDate = null
        };

        await _dbContext.Notes.AddAsync(note, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreationResponseVm { Id = note.Id };
    }
}
