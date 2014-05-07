using Microsoft.Win32;
using PluralsightPublisher.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PluralsightPublisher.Views
{
    /// <summary>
    /// Interaction logic for ProjectProperties.xaml
    /// </summary>
    public partial class ProjectProperties : UserControl
    {
        public ProjectProperties()
        {
            InitializeComponent();
        }

        private void WorkingDirectory_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var directory = GetPathFromUser();
            if(!string.IsNullOrEmpty(directory))
                ((ProjectViewModel)DataContext).WorkingDirectory = directory;
        }

        private void PublicationDirectory_PreviewUp(object sender, MouseButtonEventArgs e)
        {
            var directory = GetPathFromUser();
            if(!string.IsNullOrEmpty(directory))
                ((ProjectViewModel)DataContext).PublicationDirectory = directory;
        }

        private static string GetPathFromUser()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                return dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK ?
                    dialog.SelectedPath : string.Empty;
            }
        }

        
    }
}
