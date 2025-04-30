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
    /// Логика взаимодействия для PageSales.xaml
    /// </summary>
    public partial class PageSales : Page
    {
        Frame MainFrame;
        SalesViewModel viewModel;
        public PageSales(Frame mainFrame)
        {
            InitializeComponent();
            viewModel = new SalesViewModel();
            this.DataContext = viewModel;
            MainFrame = mainFrame;
            viewModel.ShowMessage += msg => MessageBox.Show(msg);
        }

        private void OpenAddPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageSalesWriteEdit(MainFrame));
        }

        private void EtitOpenPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageSalesWriteEdit(MainFrame,viewModel.SelectedSale));
        }
    }
}
