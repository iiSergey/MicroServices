using AutoMapper;
using MessagesService.EntityFrameworkCore;
using MessagesService.Models;
using MessagesService.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesService.Services
{
    public class MessageService : IMessageService
    {
        public MessageService(MessagesContext messagesContext, IMapper mapper)
        {
            AutoMapper = mapper;
            MessagesContext = messagesContext;
        }

        private IMapper AutoMapper { get; }
        private MessagesContext MessagesContext { get; }

        public async Task<MessageOutputModel> CreateMessageAsync(MessageCreateInputModel inputModel)
        {
            var messageModel = AutoMapper.Map<Message>(inputModel);
            await MessagesContext.Messages.AddAsync(messageModel)
                  .ConfigureAwait(false);
            await MessagesContext.SaveChangesAsync()
                .ConfigureAwait(false);
            return AutoMapper.Map<MessageOutputModel>(messageModel);
        }

        public async Task DeleteMovieAsync(long id)
        {
            var message = await MessagesContext.Messages
                .SingleOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
            MessagesContext.Messages.Remove(message);
            await MessagesContext.SaveChangesAsync()
                  .ConfigureAwait(false);
        }

        public async Task<MessageOutputModel> GetMessageAsync(long id)
        {
            var message = await MessagesContext.Messages
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
            return AutoMapper.Map<MessageOutputModel>(message);
        }

        public IQueryable<MessageOutputModel> GetMessages()
        {
            return MessagesContext.Messages.AsNoTracking().AsEnumerable()
                .Select(AutoMapper.Map<MessageOutputModel>).AsQueryable();
        }

        public Task<bool> MessageExistsAsync(long id)
        {
            return MessagesContext.Messages.AnyAsync(m => m.Id == id);
        }

        public Task UpdateMessageAsync(MessageInputModel inputModel)
        {
            var messageModel = AutoMapper.Map<Message>(inputModel);
            MessagesContext.Messages.Update(messageModel);
            return MessagesContext.SaveChangesAsync();
        }
    }
}