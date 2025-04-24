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
    /// Логика взаимодействия для PageWarehouses.xaml
    /// </summary>
    public partial class PageWarehouses : Page
    {
        Frame MainFrame;
        WarehouseViewModel viewModel;
        public PageWarehouses(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
            
            viewModel = new WarehouseViewModel();
            this.DataContext = viewModel;
            viewModel.ShowMessage += msg => MessageBox.Show(msg);
        }

        private void OpenAddPage(object sender, RoutedEventArgs e)
        {
           MainFrame.Navigate(new PageWarehousesWriteEdit(MainFrame));
        }

        private void EtitOpenPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWarehousesWriteEdit(MainFrame,viewModel.SelectedWarehouse));
        }
    }
}
