using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteDetailQueryValidator() 
    {
        RuleFor(CreateNoteCommand => CreateNoteCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(CreateNoteCommand => CreateNoteCommand.Id).NotEqual(Guid.Empty);
    }
}
