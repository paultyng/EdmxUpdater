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
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;
using EnvDTE;

namespace EdmxUpdater
{
    /// <summary>
    /// Interaction logic for EdmxUpdaterDialogWindow.xaml
    /// </summary>
    public partial class EdmxUpdaterDialogWindow : DialogWindow
    {
        public EdmxUpdaterDialogWindow(DTE dte)
        {
            InitializeComponent();

            mc.SetMruList(FindEdmxFiles(dte));
        }

        private static IEnumerable<string> FindEdmxFiles(DTE dte)
        {
            //TODO: set active edmx as selected option...

            var files = from p in dte.Solution.Projects.Cast<Project>()
                        from pi in p.ProjectItems.Cast<ProjectItem>()
                        where pi.Name.EndsWith(".edmx")
                        select pi.FileNames[0];

            return files.ToList();
        }
    }
}
