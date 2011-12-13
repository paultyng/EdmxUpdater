using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EdmxUpdater.UI.Model
{
    sealed class MainControlModel : INotifyPropertyChanged
    {
        public MainControlModel()
        {
            PropertyPascalCase = true;
            PropertyNoUnderscores = true;
            MruEdmxFiles = new ObservableCollection<string>();

            MruEdmxFiles.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(MruEdmxFiles_CollectionChanged);
        }

        void MruEdmxFiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("MruEdmxFiles");
        }

        void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        bool _propertyPascalCase;
        public bool PropertyPascalCase 
        { 
            get
            {
                return _propertyPascalCase;
            }
            set
            {
                _propertyPascalCase = value;
                RaisePropertyChanged("PropertyPascalCase");
            }
        }

        bool _propertyNoUnderscores;
        public bool PropertyNoUnderscores
        {
            get
            {
                return _propertyNoUnderscores;
            }
            set
            {
                _propertyNoUnderscores = value;
                RaisePropertyChanged("PropertyNoUnderscores");
            }
        }

        string _edmxFile;
        public string EdmxFile
        {
            get
            {
                return _edmxFile;
            }
            set
            {
                _edmxFile = value;
                RaisePropertyChanged("EdmxFile");
            }
        }

        public ObservableCollection<string> MruEdmxFiles { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
