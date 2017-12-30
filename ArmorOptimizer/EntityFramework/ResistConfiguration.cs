using System.ComponentModel.DataAnnotations.Schema;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ResistConfiguration
    {
        public long Id { get; set; }
        [Column(TypeName = "bigint")]
        public long Physical { get; set; }
        [Column(TypeName = "bigint")]
        public long Fire { get; set; }
        [Column(TypeName = "bigint")]
        public long Cold { get; set; }
        [Column(TypeName = "bigint")]
        public long Poison { get; set; }
        [Column(TypeName = "bigint")]
        public long Energy { get; set; }
    }
}
