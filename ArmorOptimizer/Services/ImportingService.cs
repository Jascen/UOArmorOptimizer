using ArmorOptimizer.Annotations;
using System;

namespace ArmorOptimizer.Services
{
    public class ImportingService
    {
        protected readonly DatabaseService DatabaseService;

        public ImportingService(DatabaseService databaseService)
        {
            if (databaseService == null) throw new ArgumentNullException(nameof(databaseService));

            DatabaseService = databaseService;
        }

        public void Import([NotNull] string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(filename));
        }
    }
}