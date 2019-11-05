using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.PostService.DTO;
using BlogLullaby.WEB_API.Infrastructure;
using BlogLullaby.WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.ModelMappers
{
    public static class PostModelMapper
    {
        public static PostDTO MapToDTO(this CreatingPostModel model, string username, FileSavingHelper _fileSavingHelper)
        {
            var postDto = new PostDTO()
            {
                BodyBlocks = model.BodyBlocks?./*Where(x => x.Image != null).*/Select((x, i) =>
                new PostBodyBlockDTO()
                {
                    BlockType = x.Type,
                    Position = i,
                    Content = x.Image != null ? _fileSavingHelper.SaveFormFileAsync(x.Image, "postsPhotos").Result : x.Text
                }),
                Author = new UserViewDTO()
                {
                    Username = username
                },
                Date = DateTime.Now,
                MainImageUrl = _fileSavingHelper.SaveFormFileAsync(model.MainImage, "postsPhotos").Result,
                Title = model.Title,
                Visits = 0
            };
            return postDto;
           

        }
    }
}
