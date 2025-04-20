using Data_Management_Warehouse.PageViewModel;
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

namespace ApplicationApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAgents.xaml
    /// </summary>
    public partial class PageAgents : Page
    {
        public AgentViewModel viewModel;
        public Frame MainFrame;
        public PageAgents(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
           viewModel = new AgentViewModel();
            this.DataContext = viewModel;
        }
        private void OpenAddPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAgentsWriteEdit(MainFrame));
            
        }

        private void EtitOpenPage(object sender, RoutedEventArgs e)
        {


            MainFrame.Navigate( new PageAgentsWriteEdit(MainFrame, viewModel.SelectedAgent));
            
            
        }
    }
}
