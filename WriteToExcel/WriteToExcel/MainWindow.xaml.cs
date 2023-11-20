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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

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

        private void HomeButton_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton? homeRadioButton = sender as System.Windows.Controls.RadioButton; 
            if (homeRadioButton != null && homeRadioButton.IsChecked == true) 
            {
                contentArea.Content = new WriteToExcel.ViewModels.HomeView();
            }
        }

        private void Graph_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton radioButton1 = sender as System.Windows.Controls.RadioButton;

            if (radioButton1 != null && radioButton1.IsChecked == true)
            {
                //contentArea.Content = new WriteToExcel.ViewModels.HomeView();
            }
        }
    }
}
