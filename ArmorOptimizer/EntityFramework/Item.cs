using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmorOptimizer.EntityFramework
{
    public partial class Item
    {
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string UoId { get; set; }
        [Column(TypeName = "bigint")]
        public long ItemTypeId { get; set; }
        [Column(TypeName = "bigint")]
        public long Color { get; set; }
        [Column(TypeName = "bigint")]
        public long BaseResistId { get; set; }
        [Column(TypeName = "bigint")]
        public long PhysicalResist { get; set; }
        [Column(TypeName = "bigint")]
        public long FireResist { get; set; }
        [Column(TypeName = "bigint")]
        public long ColdResist { get; set; }
        [Column(TypeName = "bigint")]
        public long PoisonResist { get; set; }
        [Column(TypeName = "bigint")]
        public long EnergyResist { get; set; }
    }
}
