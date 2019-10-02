using BlogLullaby.DAL.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.DAL.DataStore.Interfaces
{
    public interface IDataStore 
    {
        IRepository<Message, string> Messages { get; }
        IRepository<Post, int> Posts { get; }
        IRepository<Dialog, string> Dialogs { get; }
        IRepository<UserProfile, int> UserProfiles { get; }
        IRelationRepository<NotReadMessage, string, int> NotReadMessages { get; }
    }
}
