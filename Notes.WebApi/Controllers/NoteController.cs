using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Controllers;

[Route("api/[controller]")]
public class NoteController : BaseController
{
    private IMapper _mapper;

    public NoteController(IMapper mapper) => _mapper = mapper;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery()
        {
            UserId = UserId
        };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery()
        {
            Id = id,
            UserId = UserId
        };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreationResponseVm>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var noteData = await Mediator.Send(command);
        return Ok(noteData);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand
        {
            Id = id,
            UserId = UserId
        };

        await Mediator.Send(command);
        return NoContent();
    }

}
