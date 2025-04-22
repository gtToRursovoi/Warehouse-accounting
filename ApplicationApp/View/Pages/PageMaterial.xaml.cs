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
    /// Логика взаимодействия для PageMaterial.xaml
    /// </summary>
    public partial class PageMaterial : Page
    {
        MaterialsViewModel viewModel;
        Frame MainFrame { get; set; }
        public PageMaterial(Frame maimframe)
        {
            InitializeComponent();
             viewModel = new MaterialsViewModel();
            this.DataContext = viewModel;
            MainFrame = maimframe;
        }

        private void OpenAddPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageMaterialsWriteEdit(MainFrame));
        }

        private void EtitOpenPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageMaterialsWriteEdit(MainFrame,viewModel.SelectedMaterial));
        }
    }
}
