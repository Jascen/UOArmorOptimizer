using ArmorOptimizer.EntityFramework;

namespace ArmorOptimizer.Extensions
{
    public static class ResistConfigurationExtensions
    {
        public static ResistConfiguration Clone(this ResistConfiguration currentResists)
        {
            return new ResistConfiguration
            {
                Physical = currentResists.Physical,
                Fire = currentResists.Fire,
                Cold = currentResists.Cold,
                Poison = currentResists.Poison,
                Energy = currentResists.Energy,
            };
        }

        public static void MinusResists(this ResistConfiguration currentResists, ResistConfiguration newResists, bool canGoNegative)
        {
            currentResists.Physical -= newResists.Physical;
            currentResists.Fire -= newResists.Fire;
            currentResists.Cold -= newResists.Cold;
            currentResists.Poison -= newResists.Poison;
            currentResists.Energy -= newResists.Energy;

            if (canGoNegative) return;

            if (currentResists.Physical < 0)
            {
                currentResists.Physical = 0;
            }

            if (currentResists.Fire < 0)
            {
                currentResists.Fire = 0;
            }

            if (currentResists.Cold < 0)
            {
                currentResists.Cold = 0;
            }

            if (currentResists.Poison < 0)
            {
                currentResists.Poison = 0;
            }

            if (currentResists.Energy < 0)
            {
                currentResists.Energy = 0;
            }
        }

        public static void PlusResists(this ResistConfiguration currentResists, ResistConfiguration newResists)
        {
            currentResists.Physical += newResists.Physical;
            currentResists.Fire += newResists.Fire;
            currentResists.Cold += newResists.Cold;
            currentResists.Poison += newResists.Poison;
            currentResists.Energy += newResists.Energy;
        }
    }
}