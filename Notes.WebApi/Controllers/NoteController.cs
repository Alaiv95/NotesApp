using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

[ApiVersionNeutral]
[Produces("application/json")] 
[Route("api/[controller]")]
public class NoteController : BaseController
{
    private IMapper _mapper;

    public NoteController(IMapper mapper, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager) : base(userManager, signInManager) => _mapper = mapper;

    /// <summary>
    /// Get the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// Get /note
    /// </remarks>
    /// <returns>Returns NoteListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If user unauthorized</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        Guid userId = await GetUserId();

        var query = new GetNoteListQuery()
        {
            UserId = userId
        };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Get the details of note
    /// </summary>
    /// <param name="id">Id of the note</param>
    /// <remarks>
    /// Sample request:
    /// Get /note/{id}
    /// </remarks>
    /// <returns>Returns NoteDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If user unauthorized</response>
    /// <response code="404">If note wasnt found</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        Guid userId = await GetUserId();

        var query = new GetNoteDetailsQuery()
        {
            Id = id,
            UserId = userId
        };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Create the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// Post /note
    /// </remarks>
    /// <returns>Returns CreationResponseVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If user unauthorized</response>
    /// <response code="400">If passed invalid data</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreationResponseVm>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        Guid userId = await GetUserId();

        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = userId;
        var noteData = await Mediator.Send(command);
        return Ok(noteData);
    }

    /// <summary>
    /// Update the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// Put /note
    /// </remarks>
    /// <response code="204">No content</response>
    /// <response code="401">If user unauthorized</response>
    /// <response code="400">If passed invalid data</response>
    /// <response code="404">If note for update wasnt found</response>
    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        Guid userId = await GetUserId();

        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = userId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Delete the note
    /// </summary>
    /// <param name="id">Id of the note to delete</param>
    /// <remarks>
    /// Sample request:
    /// Delete /note/{id}
    /// </remarks>
    /// <response code="204">No content</response>
    /// <response code="401">If user unauthorized</response>
    /// <response code="404">If note to delete wasnt found</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        Guid userId = await GetUserId();

        var command = new DeleteNoteCommand
        {
            Id = id,
            UserId = userId
        };

        await Mediator.Send(command);
        return NoContent();
    }

}
