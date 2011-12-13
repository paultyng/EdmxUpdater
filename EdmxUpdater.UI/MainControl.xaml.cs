using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using EdmxUpdater.UI.Model;
using System.Xml.Linq;
using System.Xml;
using EdmxUpdater.Library;

namespace EdmxUpdater.UI
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public MainControl()
        {
            InitializeComponent();
        }

        //TODO: dependency property or something
        public void SetMruList(IEnumerable<string> mru)
        {
            Model.MruEdmxFiles.Clear();

            foreach (var f in mru)
            {
                Model.MruEdmxFiles.Add(f);
            }
        }

        MainControlModel Model
        {
            get
            {
                return (MainControlModel)Resources["viewModel"];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var updater = new EntityNameUpdater();

            updater.PropertyNoUnderscores = Model.PropertyNoUnderscores;
            updater.PropertyPascalCase = Model.PropertyPascalCase;

            updater.ProcessFile(Model.EdmxFile);
        }

        
    }
}
