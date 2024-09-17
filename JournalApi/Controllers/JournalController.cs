using Journal.Application.Commons.Commands.CreateJournal;
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
    
}