using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class DeleteNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(CreateNoteCommand => CreateNoteCommand.Title).NotEmpty().MaximumLength(150);
        // RuleFor(CreateNoteCommand => CreateNoteCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(CreateNoteCommand => CreateNoteCommand.Id).NotEqual(Guid.Empty);
    }
}
