using System;
using System.Collections.Generic;

namespace BlogLullaby.DAL.DataStore.Entities
{
    public class Post : Entity<int>
    {
        public string Title { get; set; }
        public IEnumerable<PostBodyBlock> BodyBlocks { get; set; }
        public string MainImageUrl { get; set; }
        public int Visits { get; set; }
        public DateTime Date { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
