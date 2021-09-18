namespace WbParser.Domain.Entities
{
    public class SyncRatingQueryEntity : Entity
    {
        public SyncRatingQueryEntity()
        { }

        public SyncRatingQueryEntity(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
        public bool Inactive { get; private set; }

        public void SetInactive()
        {
            Inactive = false;
            SetUpdated();
        }
    }
}