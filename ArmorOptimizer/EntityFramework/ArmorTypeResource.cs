using System.ComponentModel.DataAnnotations.Schema;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorTypeResource
    {
        [Column(TypeName = "bigint")]
        public long ArmorTypeId { get; set; }
        [Column(TypeName = "bigint")]
        public long ResourceId { get; set; }
    }
}
