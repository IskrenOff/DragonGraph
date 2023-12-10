using FontAwesome.Sharp;
using Microsoft.VisualBasic.Devices;
using ScottPlot;
using ScottPlot.MarkerShapes;
using ScottPlot.Plottable;
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

        public SlideForceView()
        {
            InitializeComponent();
            ProcessData();

        }

        public void ProcessData()
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] dataY = slideForce.ToArray();
            double[] dataX = timeStamp.ToArray();

            if(slideForce.Count == 0 || slideForce == null) 
            {
                Content = new WriteToExcel.ViewModels.NoDataView();                              
            }

            else
            {
                SlideForce.Plot.Clear();

                SlideForce.Plot.XLabel("Time Stamp");
                SlideForce.Plot.YLabel("Slide Force");
                SlideForce.Plot.Title("Slide Force Graph");

                //Customize the layout and labels colors
                SlideForce.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                SlideForce.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                SlideForce.Plot.XAxis.Label(color: System.Drawing.Color.White);
                SlideForce.Plot.YAxis.Label(color: System.Drawing.Color.White);

                SlideForce.Plot.AddScatter(dataX, dataY, color: System.Drawing.Color.Aquamarine);
                SlideForce.Render();

                Crosshair cross = SlideForce.Plot.AddCrosshair(25, .5);
 
                // Subscribe to the MouseMoved event to update the Crosshair position
                SlideForce.MouseMove += (s, e) =>
                {
                    (double mouseX, double mouseY) = SlideForce.GetMouseCoordinates();
                    cross.X = mouseX;
                    cross.Y = mouseY;

                    SlideForce.Render();
                };
            }

                       
        }
    }
}
