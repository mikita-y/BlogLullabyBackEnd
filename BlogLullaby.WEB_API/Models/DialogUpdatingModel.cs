using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Models
{
    public class DialogUpdatingModel
    {
        public string Id { get; set; }
        public string NewTitle { get; set; }
        public string AddingMember { get; set; }
        public string RemovingMember { get; set; }
    }
}
