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
using System.Runtime.InteropServices;
using System.Runtime;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Windows.Interop;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace WriteToExcel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();  
            SetIntroductoryText();
        }

        private void SetIntroductoryText()
        {
            string introductoryText = "Welcome to Dragon graph Beta Version App!\n\n" +
                "This beta version is designed to read data from CSV files and display it in the form of  " +
                "interactive graphs. Your feedback is invaluable to me as I continue to improve this application.\n\n" +
                "Features:\n\n" +
                "Import CSV files containing data.\n"+
                "Visualize data using graphs and charts.\n"+
                "Explore and analyze datasets efficiently.\n\n" +
                "Bug Reporting:\n\n" +
                "If you encounter any bugs or issues, I encourage you to reach out to me. Your insights \n"+
                "helps me enhance the app’s functionality and user experience. \n\n"+
                "Contact: Iskren.Mirev@meconet.net \n\n"+
                "Note: This version is still under development. I appreciate your patience and \n" +
                "support as I work to make this app even better. \n\n" +
                "Thank you for being a part of this beta release. Your contributions make a difference! \n" ;

            IntroductoryTextBlock.Text = introductoryText;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage (IntPtr nWnd, int wMsg, int wParam, int lParam);
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        //Close the APP on click
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Minimize the APP window on click
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Maximize the APP window on click
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState== WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState= WindowState.Normal;
            }
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        //Home view 
        private void HomeButton_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton? homeRadioButton = sender as System.Windows.Controls.RadioButton; 
            if (homeRadioButton != null && homeRadioButton.IsChecked == true) 
            {
                contentArea.Content = new WriteToExcel.ViewModels.HomeView();
            }
        }

        //Slide Force graph view
        private void SlideForce_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton? slideForceButton = sender as System.Windows.Controls.RadioButton;

            if ((slideForceButton != null && slideForceButton.IsChecked == true))
            {
                contentArea.Content = new WriteToExcel.ViewModels.SlideForceView();
            }

        }

        //Velocity graph view
        private void Velocity_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton? velocityButton = sender as System.Windows.Controls.RadioButton;

            if ((velocityButton != null && velocityButton.IsChecked == true))
            {
                contentArea.Content = new WriteToExcel.ViewModels.VelocityView();
            }
        }

        //Cushion Force graph view
        private void CushionForce_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton? cushionForceButton = sender as System.Windows.Controls.RadioButton;

            if ((cushionForceButton != null && cushionForceButton.IsChecked == true))
            {
                contentArea.Content = new WriteToExcel.ViewModels.CushionForceView();
            }
        }

        //Cushion Position graph view
        private void CushionPosition_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton? cushionPositionButton = sender as System.Windows.Controls.RadioButton;

            if ((cushionPositionButton != null && cushionPositionButton.IsChecked == true))
            {
                contentArea.Content = new WriteToExcel.ViewModels.CushionPositionView();
            }
        }

        //private void ShowNoDataMessage ()
        //{
        //    System.Windows.MessageBox.Show("No data available to display.", "No Data", MessageBoxButton.OK, MessageBoxImage.Information);
        //}
    }
}
