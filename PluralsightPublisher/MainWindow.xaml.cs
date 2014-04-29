using Microsoft.Win32;
using PluralsightPublisher.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PluralsightPublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PluralsightProjectExtension = ".plpr";
        private string FileFilter { get { return string.Format("({0})|*{0}", PluralsightProjectExtension); } }

        private readonly MainWindowViewModel _viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ProjectPath = GenerateFileName(new OpenFileDialog());
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ProjectPath = GenerateFileName(new SaveFileDialog());
            File.Create(_viewModel.ProjectPath);
        }

        private string GenerateFileName(FileDialog dialog)
        {
            dialog.FileName = "PluralsightProject";
            dialog.DefaultExt = PluralsightProjectExtension;
            dialog.Filter = FileFilter;
            
            return (dialog.ShowDialog() ?? false) ? dialog.FileName : string.Empty;
        }
    }
}
