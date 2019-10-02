
namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public class DialogPreview
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int UnReadMessages { get; set; }
        public MessageDTO LastMessage { get; set; }
    }
}
