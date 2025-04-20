namespace CMouss.ERM.APIModels
{
    public class EntityRecord
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }

        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string OwnerUserId { get; set; }
    }
}