namespace TechNewsOficialBackend.Api.Models
{
    using Supabase.Postgrest.Attributes;
    using Supabase.Postgrest.Models;

    [Table("newsletter")]
    public class Newsletter : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
