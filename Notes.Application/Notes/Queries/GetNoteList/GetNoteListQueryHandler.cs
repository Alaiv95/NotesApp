using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
{
    private readonly INotesDbContext _notesDbContext;
    private readonly IMapper _mapper;

    public GetNoteListQueryHandler(IMapper mapper, INotesDbContext notesDbContext)
    {
        _mapper = mapper; 
        _notesDbContext = notesDbContext;
    }

    public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        var notes = await _notesDbContext.Notes
            .Where(n => n.UserId == request.UserId)
            .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new NoteListVm { Notes = notes };
    }
}
