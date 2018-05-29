using MessagesService.Models.Dto;
using MessagesService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MessagesService.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MessagesController : Controller
    {
        public MessagesController(IMessageService messageService, ILogger<MessagesController> logger)
        {
            MessageService = messageService;
            Logger = logger;
        }

        private ILogger<MessagesController> Logger { get; }
        private IMessageService MessageService { get; }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]MessageCreateInputModel inputModel)
        {
            //Logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", id);
            if (inputModel == null)
                return BadRequest();

            var outputModel = await MessageService.CreateMessageAsync(inputModel).ConfigureAwait(false);

            return CreatedAtRoute("GetMessage",
                new { id = outputModel.Id }, outputModel);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await MessageService.MessageExistsAsync(id).ConfigureAwait(false))
                return NotFound();

            await MessageService.DeleteMovieAsync(id).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var outputModel = MessageService.GetMessages();
            return Ok(outputModel);
        }

        [HttpGet("message/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var outputModel = await MessageService.GetMessageAsync(id).ConfigureAwait(false);
            if (outputModel == null)
                return NotFound();

            return Ok(outputModel);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]MessageInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!await MessageService.MessageExistsAsync(id).ConfigureAwait(false))
                return NotFound();

            await MessageService.UpdateMessageAsync(inputModel).ConfigureAwait(false);

            return NoContent();
        }
    }
}