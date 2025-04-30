using ApplicationApp.Assets.Image;
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
    /// Логика взаимодействия для PageProductsWriteEdit.xaml
    /// </summary>
    public partial class PageProductsWriteEdit : Page
    {
        Frame MainFrame;
        ProductsViewModel viewModel;
        public PageProductsWriteEdit(Frame frame,Product productToEdit = null)
        {
            InitializeComponent();
            MainFrame = frame;
            viewModel = new ProductsViewModel(new FileDialogService(),productToEdit);
            viewModel.ShowMessage += msg => MessageBox.Show(msg);
            this.DataContext = viewModel;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageProducts(MainFrame));
        }
    }
}
