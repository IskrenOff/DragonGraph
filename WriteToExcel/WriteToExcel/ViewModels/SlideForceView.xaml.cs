using ScottPlot;
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
    /// Interaction logic for SlideForceView.xaml
    /// </summary>
    public partial class SlideForceView : UserControl
    {

        public void ProcessData()
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] dataY = slideForce.ToArray();
            double[] dataX = timeStamp.ToArray();

            SlideForce.Plot.AddScatter(dataX, dataY);
            SlideForce.Refresh();
        }

        public SlideForceView()
        {
            InitializeComponent();
            ProcessData();
        }       
    }
}
