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
using System.Collections;
using ClosedXML.Excel;


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
        public List<double> punchForce {  get; set; }
        public List<double> timeStamp { get; set; }

        public HomeView()
        {
            InitializeComponent();

            slideForce = new List<double>();
            velocity = new List<double>();
            cushionForce = new List<double>();
            cushionPosition = new List<double>();
            punchForce = new List<double>();
            timeStamp = new List<double>();

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
                if (slideForce.SequenceEqual(slideForce) && slideForce.Count != 0)
                {
                    MessageBox.Show("The data is already collected !");
                }
                else
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
            }
            else
            {
                MessageBox.Show("Please enter a valid file path, or use Excel to make the graphs manually !!!");
            }
        }



        private void ReadExtractedData(string csvFilePath)
        {
           
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

                for (int i = 0; i < slideForce.Count; i++)
                {
                    double punchFiller = slideForce[i] - cushionForce[i];
                    punchForce.Add(punchFiller);
                }

                if (DataContainer.Instance != null && slideForce.Count > 0 && timeStamp.Count > 0 && 
                    velocity.Count > 0 && cushionForce.Count > 0 && cushionPosition.Count > 0 && punchForce.Count > 0)
                {
                    DataContainer.Instance.SlideForce = slideForce;
                    DataContainer.Instance.Velocity = velocity;
                    DataContainer.Instance.CushionForce = cushionForce;
                    DataContainer.Instance.CushionPosition = cushionPosition;
                    DataContainer.Instance.PunchForce = punchForce;
                    DataContainer.Instance.TimeStamp = timeStamp;
                }
            }
            
        }

        private void CreateExcel_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Save Excel File";
            saveFileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                // Create a new Excel package
                using (var workBook = new XLWorkbook())
                {
                    // Add a new worksheet
                    var excelSheet = workBook.Worksheets.Add("Data");

                    // Define the column headers (if needed)
                    excelSheet.Cell(1, 1).Value = "Slide Force";
                    excelSheet.Cell(1, 2).Value = "Velocity";
                    excelSheet.Cell(1, 3).Value = "Cushion Force";
                    excelSheet.Cell(1, 4).Value = "Cushion Position";
                    excelSheet.Cell(1, 5).Value = "Time Stamp";

                    List<double> slideForce = DataContainer.Instance.SlideForce;
                    List<double> velocity = DataContainer.Instance.Velocity;
                    List<double> cushionForce = DataContainer.Instance.CushionForce;
                    List<double> cushionPosition = DataContainer.Instance.CushionPosition;
                    List<double> timeStamp = DataContainer.Instance.TimeStamp;

                    // Check if data lists are not null and have the same length
                    if (slideForce != null && velocity != null &&
                        cushionForce != null && cushionPosition != null &&timeStamp != null &&
                        slideForce.Count == velocity.Count &&
                        velocity.Count == cushionForce.Count &&
                        cushionForce.Count == cushionPosition.Count &&
                        cushionPosition.Count == timeStamp.Count)
                    {
                        // Write data vertically
                        for (int i = 0; i < slideForce.Count; i++)
                        {
                            excelSheet.Cell(i + 2, 1).Value = slideForce[i];
                            excelSheet.Cell(i + 2, 2).Value = velocity[i];
                            excelSheet.Cell(i + 2, 3).Value = cushionForce[i];
                            excelSheet.Cell(i + 2, 4).Value = cushionPosition[i];
                            excelSheet.Cell(i + 2, 5).Value = timeStamp[i];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data lists are null or have different lengths.");
                    }

                    // Save the Excel package to the chosen file path
                    var file = new FileInfo(saveFileDialog.FileName);
                    workBook.SaveAs(saveFileDialog.FileName);
                }

                MessageBox.Show("Excel file saved successfully.");
            }
        }
    }
}

