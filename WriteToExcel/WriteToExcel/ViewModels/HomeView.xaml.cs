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
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Globalization;

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

        private void FindCSVFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Filter for CSF files
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            //Set initial directory
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (openFileDialog.ShowDialog() == true)
            {
                // Retrieve the selected file path and display it in the text box
                csvFilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void ExtractButton_Click(object sender, RoutedEventArgs e)
        {
            //Retrieve the pasted file path
            string csvFilePath = csvFilePathTextBox.Text;

            if (!string.IsNullOrEmpty(csvFilePath))
            {
                if (File.Exists(csvFilePath))
                {
                    // Call method to read CSV file data
                    ReadExtractedData(csvFilePath);
                }
                else
                {
                    MessageBox.Show("File does not exist at the specified path!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid file path, or use Excel to make the graphs manually !!!");
            }
        }


        private void ReadExtractedData(string csvFilePath)
        {

            List<double> slideForce = new List<double>();
            List<double> velocity = new List<double>();
            List<double> cushionForce = new List<double>();
            List<double> cushionPosition = new List<double>();
            List<double> timeStamp = new List<double>();

            //Taking the row data as array of strings
            //string[] columnNames = { "SlideForce", "Velocity", "CushionForce", "CushionPosition", "TimeStamp" };

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                //Skip header rows are any unnecessary data before the target data
                bool isTargetData = false;
                while (!parser.EndOfData && !isTargetData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length > 0 && fields[0].Trim() == "[Penn Data]")
                    {
                        isTargetData = true;
                    }
                }


                //Filling the Lists with the columns data
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length >= 5)
                    {
                        double value1, value2, value3, value4, value5;
                        if (double.TryParse(fields[0], NumberStyles.Any, CultureInfo.InvariantCulture, out value1) &&
                            double.TryParse(fields[1], NumberStyles.Any, CultureInfo.InvariantCulture, out value2) &&
                            double.TryParse(fields[2], NumberStyles.Any, CultureInfo.InvariantCulture, out value3) &&
                            double.TryParse(fields[3], NumberStyles.Any, CultureInfo.InvariantCulture, out value4) &&
                            double.TryParse(fields[4], NumberStyles.Any, CultureInfo.InvariantCulture, out value5))
                        {
                            slideForce.Add(value1);
                            velocity.Add(value2);
                            cushionForce.Add(value3);
                            cushionPosition.Add(value4);
                            timeStamp.Add(value5);
                        }                        
                    }
                }
            }
        }
    }
}

