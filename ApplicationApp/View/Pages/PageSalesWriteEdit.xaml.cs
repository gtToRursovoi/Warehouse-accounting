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
    /// Логика взаимодействия для PageSalesWriteEdit.xaml
    /// </summary>
    public partial class PageSalesWriteEdit : Page
    {
        SalesViewModel viewModel;
        Frame MainFrame;
        public PageSalesWriteEdit(Frame mainFrame, Sales salestoedit = null)
        {
            InitializeComponent();
            viewModel = new SalesViewModel(salestoedit);
            this.DataContext = viewModel;
            MainFrame = mainFrame;
            viewModel.ShowMessage += msg => MessageBox.Show(msg);
        }
        private void close_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageSales(MainFrame));
        }
    }
}
