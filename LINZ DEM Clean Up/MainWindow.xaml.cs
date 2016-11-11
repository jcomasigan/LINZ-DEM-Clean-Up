using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LINZ_DEM_Clean_Up
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void chooseDirButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            dirTextBox.Text = dialog.SelectedPath;
        }

        private void cleanButton_Click(object sender, RoutedEventArgs e)
        {
            double maxHeight = 0;
            double.TryParse(maxHeightTxtBox.Text, out maxHeight);
            if (System.IO.Directory.Exists(dirTextBox.Text) && maxHeight > 0)
            {
                CleanDEM.Clean(dirTextBox.Text, maxHeight);
            }
        }
    }
}
