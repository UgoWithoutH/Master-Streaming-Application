using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Class
{
    public class MainManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyname)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));


        public ReadOnlyObservableCollection<Genre> ListGenres { get; private set; }
        public ObservableCollection<Genre> listGenres = new ObservableCollection<Genre>() { new Genre("Humour"), new Genre("Romance"), new Genre("Sci-Fi"), };

        public MainManager()
        {
        }
    }
}
