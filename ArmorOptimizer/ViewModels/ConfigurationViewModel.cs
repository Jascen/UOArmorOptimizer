using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Services;
using System.Collections.Generic;

namespace ArmorOptimizer.ViewModels
{
    public class ConfigurationViewModel
    {
        protected readonly ConfigurationService Service;

        public ConfigurationViewModel()
        {
            ConfigureArmorTypesViewModel = new ConfigureArmorTypesViewModel();
            DataImportViewModel = new DataImportViewModel();
            Service = new ConfigurationService(this);
        }

        public IEnumerable<ArmorType> ArmorTypes { get; set; }
        public ArmorType ArmorTypeToAdd { get; set; }
        public ConfigureArmorTypesViewModel ConfigureArmorTypesViewModel { get; set; }
        public DataImportViewModel DataImportViewModel { get; set; }
        public Item ItemToAdd { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
        public Resource ResourceToAdd { get; set; }
    }
}