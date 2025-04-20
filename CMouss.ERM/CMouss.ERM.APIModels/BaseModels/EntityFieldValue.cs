namespace CMouss.ERM.APIModels
{
    public class EntityFieldValue
    {
        public int Id { get; set; }
        public int EntityRecordId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}