using Journal.Application.Commons.Commands.CreateJournal;
using Journal.Application.Commons.Commands.EditJournal;
using Journal.Application.Commons.Queries.GetAllJournal;
using Journal.Application.Dtos;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JournalApi.Controllers;
[ApiController]
[Route("Journal")]
public class JournalController :ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILog _log;

    public JournalController(IMediator mediator, ILog log)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _log = log;
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody]CreateJournalCommand command)
    {
        var result=await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllJournalQuery());
        return Ok(result);
    }
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> EditJournal([FromRoute]string id,[FromBody] EditAdctionFormDto dto)
    {
        
            var result = await _mediator.Send(new EditJournalCommand
            {
                Id = id,
                EditProperty = dto
            });

            return Ok(result);
    }
}