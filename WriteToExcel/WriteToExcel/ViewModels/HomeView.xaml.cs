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
            openFileDialog.InitialDirectory=Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

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

            if (!string.IsNullOrEmpty(csvFilePath) && System.IO.File.Exists(csvFilePath))
            {

                // Call method to read CSV file data
                ReadExtractedData(csvFilePath);
            }
            else
            {
                MessageBox.Show("Please enter a valid Excel file path, or use Excel to make the graphs manually !!!");
            }
        }
           

        private void ReadExtractedData(string csvFilePath)
        {
            //To be checked , not sure if this is going to work ??????
            //ReadExtractedData extractedData = new ReadExtractedData
            //{
            //    SlideForce = new List<double>(),
            //    Velocity = new List<double>(),
            //    CushionForce = new List<double>(),
            //    CushionPosition = new List<double>(),
            //    TimeStamp = new List<double>(),
            //};

            List<double> slideForce = new List<double>();
            List<double> velocity = new List<double>();
            List<double> cushionForce = new List<double>();
            List<double> cushionPosition = new List<double>();
            List<double> timeStamp = new List<double>();

            //Taking the row data as array of strings
            string[] columnNames = { "SlideForce", "Velocity", "CushionForce", "CushionPosition", "TimeStamp" };

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

                //Extract data based on specific pattern
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length >= 5)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (double.TryParse(fields[i], out double value))
                            {
                                //Cycle through the columns
                                int columnIndex = i % columnNames.Length;

                                switch (columnNames[columnIndex])
                                {
                                    case "slideForce":
                                        slideForce.Add(value);
                                        break;
                                    case "velocity":
                                        velocity.Add(value);
                                        break;
                                    case "cushionForce":
                                        cushionForce.Add(value);
                                        break;
                                    case "cushionPosition":
                                        cushionPosition.Add(value);
                                        break;
                                    case "timeStamp":
                                        timeStamp.Add(value);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("SlideForce:");
            Console.WriteLine(string.Join(", ", slideForce));

        }           
    }
    }
