namespace BlogLullaby.DAL.DataStore.Entities
{
    public class NotReadMessage : EntityWithCompositeKey<string, int>
    {
        public Message Message { get; set; }
        public UserProfile Recipient { get; set; }
    }
}
