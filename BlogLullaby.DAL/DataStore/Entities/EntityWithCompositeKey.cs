namespace BlogLullaby.DAL.DataStore.Entities
{
    public class EntityWithCompositeKey<Key1, Key2>
    {
        public Key1 FirstKey { get; set; }
        public Key2 SecondKey { get; set; }
    }
}
