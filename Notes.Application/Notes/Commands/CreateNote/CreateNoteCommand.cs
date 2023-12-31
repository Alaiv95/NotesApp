using MediatR;
using Notes.WebApi.Models;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommand : IRequest<CreationResponseVm>
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
}
