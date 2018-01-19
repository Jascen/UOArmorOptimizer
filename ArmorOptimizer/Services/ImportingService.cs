using ArmorOptimizer.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArmorOptimizer.Services
{
    public class ImportingService
    {
        protected readonly DatabaseService DatabaseService;

        public ImportingService()
        {
            DatabaseService = new DatabaseService();
        }

        public IEnumerable<string> Import([NotNull] string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(filename));
            if (File.Exists(filename)) throw new FileNotFoundException("Unable to locate file.", filename);

            return File.ReadAllLines(filename);
        }
    }
}