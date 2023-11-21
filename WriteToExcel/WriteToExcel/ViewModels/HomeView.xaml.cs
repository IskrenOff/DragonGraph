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
using System.IO;

namespace WriteToExcel.ViewModels
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }
     
        private void ExtractButton_Click(object sender, RoutedEventArgs e)
        {
            //Retrieve the pasted file path
            string excelFilePath = excelFilePathTextBox.Text;

            if (!string.IsNullOrEmpty(excelFilePath) && System.IO.File.Exists(excelFilePath))
            {

            }
            else
            {
                MessageBox.Show("Please enter a valid Excel file path, or use Excel to make the graphs manually !!!");
            }
        }
    }
}
