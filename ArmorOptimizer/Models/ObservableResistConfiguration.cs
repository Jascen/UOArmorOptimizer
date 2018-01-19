using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArmorOptimizer.Properties;

namespace ArmorOptimizer.Models
{
    public class ObservableResistConfiguration : INotifyPropertyChanged
    {
        private long _cold;
        private long _energy;
        private long _fire;
        private long _physical;
        private long _poison;

        public long Cold
        {
            get { return _cold; }
            set
            {
                if (value == _cold) return;

                _cold = value;
                OnPropertyChanged();
            }
        }

        public long Energy
        {
            get { return _energy; }
            set
            {
                if (value == _energy) return;

                _energy = value;
                OnPropertyChanged();
            }
        }

        public long Fire
        {
            get { return _fire; }

            set
            {
                if (value == _fire) return;
                _fire = value;
                OnPropertyChanged();
            }
        }

        public long Physical
        {
            get { return _physical; }
            set
            {
                if (value == _physical) return;

                _physical = value;
                OnPropertyChanged();
            }
        }

        public long Poison
        {
            get { return _poison; }
            set
            {
                if (value == _poison) return;

                _poison = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}