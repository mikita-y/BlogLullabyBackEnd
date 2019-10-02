using BlogLullaby.DAL.DataStore.Interfaces;
using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    public class EFDataStore<T> : IDataStore where T : DbContext
    {
        private T _context;
        private IRepository<UserProfile, int> userProfileRepository;
        private IRepository<Post, int> postRepository;
        private IRepository<Message, string> messageRepository;
        private IRepository<Dialog, string> dialogRepository;
        private IRelationRepository<NotReadMessage, string, int> notReadMessageRepository;

        public EFDataStore(T context)
        {
            _context = context;
        }

        public IRepository<Message, string> Messages
        {
            get
            {
                if (messageRepository == null)
                    messageRepository = new MessageRepository(_context);
                return messageRepository;
            }
        }

        public IRepository<Dialog, string> Dialogs
        {
            get
            {
                if (dialogRepository == null)
                    dialogRepository = new DialogRepository(_context);
                return dialogRepository;
            }
        }

        public IRepository<Post, int> Posts
        {
            get
            {
                if (postRepository == null)
                    postRepository = new PostRepository(_context);
                return postRepository;
            }
        }

        public IRepository<UserProfile, int> UserProfiles
        {
            get
            {
                if (userProfileRepository == null)
                    userProfileRepository = new UserProfileRepository(_context);
                return userProfileRepository;
            }
        }

        public IRelationRepository<NotReadMessage, string, int> NotReadMessages
        {
            get
            {
                if (notReadMessageRepository == null)
                    notReadMessageRepository = new EFRelationRepository<NotReadMessage, string, int>(_context);
                return notReadMessageRepository;
            }
        }

    }
}
