namespace WbParser.Domain.Entities
{
    public class RatingSupportedProductEntity : Entity
    {
        public RatingSupportedProductEntity()
        { }

        public RatingSupportedProductEntity(long articleId)
        {
            ArticleId = articleId;
        }
        public long ArticleId { get; private set; }
        public bool Inactive { get; private set; }

        public void SetInactive()
        {
            Inactive = false;
            SetUpdated();
        }
    }
}