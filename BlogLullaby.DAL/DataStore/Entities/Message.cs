using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogLullaby.DAL.DataStore.Entities
{
    public class Message : Entity<string>
    {
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public UserProfile UserProfile { get; set; }

        public Dialog Dialog { get; set; }
    }
}
