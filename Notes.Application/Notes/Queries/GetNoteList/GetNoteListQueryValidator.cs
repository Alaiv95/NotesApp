using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
{
    public GetNoteListQueryValidator() {
        // RuleFor(CreateNoteCommand => CreateNoteCommand.UserId).NotEqual(Guid.Empty);
    }
}
