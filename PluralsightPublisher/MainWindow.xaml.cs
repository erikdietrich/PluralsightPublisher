using Microsoft.Win32;
using PluralsightPublisher.DataAccess;
using PluralsightPublisher.Domain;
using PluralsightPublisher.Presentation;
using PluralsightPublisher.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;

namespace PluralsightPublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PluralsightProjectExtension = ".plpr";
        private static string FileFilter { get { return string.Format("({0})|*{0}", PluralsightProjectExtension); } }

        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            var domainRoot = new DomainRoot();
            var projectRepository = new ProjectRepository(new BasicXmlDocument(), new BasicWorkspaceBuilder(), domainRoot);
            var moduleRepository = new ModuleRepository(new BasicXmlDocument(), domainRoot);
            _viewModel = new MainWindowViewModel(projectRepository, moduleRepository);
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            var pathOfFile = GenerateFileName(new OpenFileDialog());
            if(!string.IsNullOrEmpty(pathOfFile))
                _viewModel.LoadProject(pathOfFile);
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            var pathOfFileToCreate = GenerateFileName(new SaveFileDialog());
            if (!string.IsNullOrEmpty(pathOfFileToCreate)) 
                _viewModel.CreateNewProject(pathOfFileToCreate);
        }

        private static string GenerateFileName(FileDialog dialog)
        {
            dialog.FileName = "PluralsightProject";
            dialog.DefaultExt = PluralsightProjectExtension;
            dialog.Filter = FileFilter;
            
            return (dialog.ShowDialog() ?? false) ? dialog.FileName : string.Empty;
        }
    }
}
