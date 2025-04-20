using Data_Management_Warehouse.PageViewModel;
using Database.Service;
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
    /// Логика взаимодействия для PageAgentsWriteEdit.xaml
    /// </summary>
    public partial class PageAgentsWriteEdit : Page
    {
        Frame MainFrame;
        public PageAgentsWriteEdit(Frame maimframe, Agent agentEdit = null)
        {
            InitializeComponent();
            MainFrame = maimframe;
            var viewModel = new AgentViewModel(agentEdit);
            DataContext = viewModel;

            viewModel.OnError += msg => MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            viewModel.OnSuccess += msg => MessageBox.Show(msg, "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAgents(MainFrame));
        }
    }
}
