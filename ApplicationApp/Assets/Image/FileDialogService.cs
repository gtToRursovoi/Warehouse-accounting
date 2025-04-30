using Data_Management_Warehouse.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.Assets.Image
{
    public class FileDialogService : IFileDialogService
    {
        public string OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "Изображения (*.jpeg;*.png;*.jpg;*.gif)|*.jpeg;*.png;*.jpg;*.gif"
            };

            return dlg.ShowDialog() == true ? dlg.FileName : null;
        }
    }
}
