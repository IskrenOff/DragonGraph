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

namespace WriteToExcel.ViewModels
{
    /// <summary>
    /// Interaction logic for NoDataView.xaml
    /// </summary>
    public partial class NoDataView : UserControl
    {
        public NoDataView()
        {
            InitializeComponent();
            NoDataMessage();
        }

        public void NoDataMessage()
        {
            string noData = "The data has gone on vacation! Without it, I can't make a graph. Please bring the data back from its trip!";

            NoDataTextBlock.Text = noData;
        }
    }
}
