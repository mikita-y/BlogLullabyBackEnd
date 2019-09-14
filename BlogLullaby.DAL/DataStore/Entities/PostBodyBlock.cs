using System;
using System.Collections.Generic;
using System.Text;

namespace BlogLullaby.DAL.DataStore.Entities
{
    public enum PostBodyBlockType { Text, Image, Movie}

    public class PostBodyBlock : Entity<string>
    {
        public int Position { get; set; }
        public PostBodyBlockType BlockType { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
    }
}
