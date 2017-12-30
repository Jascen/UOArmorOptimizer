namespace ArmorOptimizer.Services
{
    public class ImportingService
    {
        protected readonly DatabaseService DatabaseService;
        public ImportingService(DatabaseService databaseService)
        {
            if (databaseService == null) throw new System.ArgumentNullException(nameof(databaseService));

            DatabaseService = databaseService;
        }

        public void Import(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new System.ArgumentException("message", nameof(filename));


        }
    }
}