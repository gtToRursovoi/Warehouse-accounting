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
    /// Логика взаимодействия для PageWarehouseProductsWrite.xaml
    /// </summary>
    public partial class PageWarehouseProductsWrite : Page
    {
        Frame MainFrame;
        WarehouseProductViewModel viewModel;
        public PageWarehouseProductsWrite(Frame maimframe,WarehouseProduct warehouseProductToEdit = null)
        {
            InitializeComponent();
            MainFrame = maimframe;
            viewModel = new WarehouseProductViewModel(warehouseProductToEdit);
            viewModel.ShowMessage += msg => MessageBox.Show(msg);
            this.DataContext = viewModel;
        }

        private void close_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWarehouseProducts(MainFrame));
        }
    }
}
