using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Models
{
    public class CreatingPostModelCrutch
    {
        public string Title { get; set; }
        public IFormFile MainImage { get; set; }
        public IFormFile[] BodyBlockImages { get; set; }
        public string[] BodyBlockTexts { get; set; }
        public string[] BodyBlockTypes { get; set; }
        public SubjectArea SubjectArea { get; set; }

        public CreatingPostModel MapToCreatingPostModel()
        {
            var bodyBlocks = new List<PostBodyBlock>();
            if(BodyBlockTypes != null)
            {
                for (int i = 0, j = 0; i < BodyBlockTypes.Count(); i++)
                {
                    if (((string[])this.BodyBlockTypes)[i] == "text")
                        bodyBlocks.Add(new PostBodyBlock()
                        {
                            Text = ((string[])this.BodyBlockTexts)[i],
                            Type = ((string[])this.BodyBlockTypes)[i],
                        });
                    else
                        bodyBlocks.Add(new PostBodyBlock()
                        {
                            Image = ((IFormFile[])this.BodyBlockImages)[j++],
                            Type = ((string[])this.BodyBlockTypes)[i],
                        });
                }
            }
                
            return new CreatingPostModel()
            {
                Title = this.Title,
                MainImage = this.MainImage,
                BodyBlocks = bodyBlocks
            };
        }
    }
}
