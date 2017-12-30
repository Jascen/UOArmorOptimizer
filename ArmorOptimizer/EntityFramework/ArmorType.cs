using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorType
    {
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string ItemType { get; set; }
        [Column(TypeName = "bigint")]
        public long Slot { get; set; }
    }
}
