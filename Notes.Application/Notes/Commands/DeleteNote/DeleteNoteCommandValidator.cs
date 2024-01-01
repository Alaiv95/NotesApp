using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(CreateNoteCommand => CreateNoteCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(CreateNoteCommand => CreateNoteCommand.Id).NotEqual(Guid.Empty);
    }
}
