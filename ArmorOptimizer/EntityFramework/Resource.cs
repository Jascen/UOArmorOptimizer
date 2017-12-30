using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmorOptimizer.EntityFramework
{
    public partial class Resource
    {
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Name { get; set; }
        [Column(TypeName = "bigint")]
        public long Color { get; set; }
        [Column(TypeName = "bigint")]
        public long ResistId { get; set; }
    }
}
