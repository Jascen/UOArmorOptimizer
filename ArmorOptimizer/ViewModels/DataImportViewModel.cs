using ArmorOptimizer.Annotations;
using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmorOptimizer.ViewModels
{
    public class DataImportViewModel : INotifyPropertyChanged
    {
        private string _selectedFilepath;

        public DataImportViewModel()
        {
            Service = new ImportingService(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ArmorType> ArmorTypes { get; set; }
        public IEnumerable<Resource> Resources { get; set; }

        public string SelectedFilepath
        {
            get { return _selectedFilepath; }
            set
            {
                if (value == _selectedFilepath) return;

                _selectedFilepath = value;
                OnPropertyChanged();
            }
        }

        public ImportingService Service { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}