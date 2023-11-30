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
        public List<double> slideForce { get;  set; }
        public List<double> velocity {  get; set; }
        public List<double> cushionForce {  get; set; }
        public List<double> cushionPosition {  get; set; }
        public List<double> timeStamp { get; set; }

        public HomeView()
        {
            InitializeComponent();
            SetIntroductoryText();
        }

        private void SetIntroductoryText()
        {
            string introductoryText = "Welcome to Dragon graph Beta Version 0.0.17!\n\n" +
                "This beta version is designed to read data from CSV files and display it in the form of  " +
                "interactive graphs. Your feedback is invaluable to me as I continue to improve this application.\n\n" +
                "Features:\n\n" +
                "Import CSV files containing data.\n" +
                "Visualize data using graphs and charts.\n" +
                "Explore and analyze datasets efficiently.\n\n" +
                "Bug Reporting:\n\n" +
                "If you encounter any bugs or issues, I encourage you to reach out to me. Your insights \n" +
                "helps me enhance the app’s functionality and user experience. \n\n" +
                "Contact: Iskren.Mirev@meconet.net \n\n" +
                "Note: This version is still under development. I appreciate your patience and \n" +
                "support as I work to make this app even better. \n\n" +
                "Thank you for being a part of this beta release. Your contributions make a difference! \n";

            IntroductoryTextBlock.Text = introductoryText;
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

            slideForce = new List<double>();
            velocity = new List<double>();
            cushionForce = new List<double>();
            cushionPosition = new List<double>();
            timeStamp = new List<double>();

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

                if (DataContainer.Instance != null && slideForce.Count > 0 && timeStamp.Count > 0 && velocity.Count > 0 && cushionForce.Count > 0 && cushionPosition.Count > 0)
                {
                    DataContainer.Instance.SlideForce = slideForce;
                    DataContainer.Instance.Velocity = velocity;
                    DataContainer.Instance.CushionForce = cushionForce;
                    DataContainer.Instance.CushionPosition = cushionPosition;
                    DataContainer.Instance.TimeStamp = timeStamp;
                }
            }
        }

        

    }
}

