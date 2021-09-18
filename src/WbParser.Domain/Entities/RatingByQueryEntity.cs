namespace WbParser.Domain.Entities
{
    public class RatingByQueryEntity : Entity
    {
        public RatingByQueryEntity(long position)
        {
            Position = position;
        }

        public RatingByQueryEntity(long articleId, 
            long root,
            long kindId,
            long subjectId, 
            string name, 
            string brand,
            long brandId,
            long siteBrandId,
            long sale, 
            long priceU, 
            long salePriceU, 
            long pics,
            long rating, 
            long feedbacks,
            bool diffPrice, 
            bool? isNew,
            long? promopic, long position)
        {
            ArticleId = articleId;
            Root = root;
            KindId = kindId;
            SubjectId = subjectId;
            Name = name;
            Brand = brand;
            BrandId = brandId;
            SiteBrandId = siteBrandId;
            Sale = sale;
            PriceU = priceU;
            SalePriceU = salePriceU;
            Pics = pics;
            Rating = rating;
            Feedbacks = feedbacks;
            DiffPrice = diffPrice;
            IsNew = isNew;
            Promopic = promopic;
            Position = position;
        }
        public long Position { get; private set; }
        public long ArticleId { get; private set; }
        public long Root { get; private set; }

        public long KindId { get; private set; }

        public long SubjectId { get; private set; }

        public string Name { get; private set; }
        public string Brand { get; private set; }

        public long BrandId { get; private set; }

        public long SiteBrandId { get; private set; }

        public long Sale { get; private set; }

        public long PriceU { get; private set; }

        public long SalePriceU { get; private set; }

        public long Pics { get; private set; }

        public long Rating { get; private set; }

        public long Feedbacks { get; private set; }

        public bool DiffPrice { get; private set; }

        public bool? IsNew { get; private set; }

        public long? Promopic { get; private set; }
    }
}