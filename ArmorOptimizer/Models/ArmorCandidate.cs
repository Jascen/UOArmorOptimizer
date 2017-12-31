using ArmorOptimizer.EntityFramework;

namespace ArmorOptimizer.Models
{
    public class ArmorCandidate
    {
        public CandidateInfo ColdCandidateInfo { get; set; }
        public CandidateInfo EnergyCandidateInfo { get; set; }
        public CandidateInfo FireCandidateInfo { get; set; }
        public Item Item { get; set; }
        public CandidateInfo PhysicalCandidateInfo { get; set; }
        public CandidateInfo PoisonCandidateInfo { get; set; }
    }
}