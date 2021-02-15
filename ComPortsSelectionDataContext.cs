using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UnitTestApp
{
    public class ComPortsSelectionDataContext : INotifyPropertyChanged
    {
        public ObservableCollection<string> ListOfAvaliablePorts { get; set; }

        public ComPortsSelectionDataContext()
        {

        }

        protected int selectedPortIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        public int SelectedPortIndex
        {
            get { return selectedPortIndex; }
            set
            {
                selectedPortIndex = value;
                NotifyPropertyChanged("SelectedPortIndex");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
