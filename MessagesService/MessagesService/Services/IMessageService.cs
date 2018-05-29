using MessagesService.Models.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesService.Services
{
    public interface IMessageService
    {
        Task<MessageOutputModel> CreateMessageAsync(MessageCreateInputModel inputModel);

        Task DeleteMovieAsync(long id);

        Task<MessageOutputModel> GetMessageAsync(long id);

        IQueryable<MessageOutputModel> GetMessages();

        Task<bool> MessageExistsAsync(long id);

        Task UpdateMessageAsync(MessageInputModel inputModel);
    }
}