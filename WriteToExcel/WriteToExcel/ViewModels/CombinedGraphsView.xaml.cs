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
using WriteToExcel;

namespace DragonGraph.ViewModels
{
    /// <summary>
    /// Interaction logic for CombinedGraphsView.xaml
    /// </summary>
    public partial class CombinedGraphsView : UserControl
    {
        

        public CombinedGraphsView()
        {
            InitializeComponent();
            ProcessData();

        }
        public void ProcessData()
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
          

            if (slideForce.Count == 0 || slideForce == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }

            else
            {
                CombinedGraphs.Plot.Clear();
                CombinedGraphs.Refresh();

                CombinedGraphs.Plot.XLabel("X Axis ");
                CombinedGraphs.Plot.YLabel("Y Axis");
                CombinedGraphs.Plot.Title("Combined Graphs");

                //Customize the layout and labels colors
                CombinedGraphs.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                CombinedGraphs.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                CombinedGraphs.Plot.XAxis.Label(color: System.Drawing.Color.White);
                CombinedGraphs.Plot.YAxis.Label(color: System.Drawing.Color.White);

                

                Crosshair cross = CombinedGraphs.Plot.AddCrosshair(25, .5);

                // Subscribe to the MouseMoved event to update the Crosshair position
                CombinedGraphs.MouseMove += (s, e) =>
                {
                    (double mouseX, double mouseY) = CombinedGraphs.GetMouseCoordinates();
                    cross.X = mouseX;
                    cross.Y = mouseY;

                    
                    CombinedGraphs.Render();
                };
            }
        }

        ScatterPlot slideForcePlot;
        ScatterPlot velocityPlot;
        ScatterPlot cushionForcePlot;
        ScatterPlot cushionPositionPlot;
        ScatterPlot timeStampPlot;

        private void SlideForce_Checked(object sender, RoutedEventArgs e)
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] slideForceY = slideForce.ToArray();
            double[] dataX = timeStamp.ToArray();
            
            slideForcePlot = CombinedGraphs.Plot.AddScatter(dataX, slideForceY,markerSize: 3);
            slideForcePlot.Smooth = true;
            CombinedGraphs.Render();
        }

        private void SlideForce_Unchecked(object sender, RoutedEventArgs e)
        {
            CombinedGraphs.Plot.Remove(slideForcePlot);
            CombinedGraphs.Refresh();
        }

        private void Velocity_Checked(object sender, RoutedEventArgs e)
        {
            List<double> velocity = DataContainer.Instance.Velocity;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] velocityY = velocity.ToArray();
            double[] dataX = timeStamp.ToArray();

            velocityPlot = CombinedGraphs.Plot.AddScatter(dataX, velocityY, markerSize: 3);
            velocityPlot.Smooth = true;
            CombinedGraphs.Render();
        }

        private void Velocity_Unchecked(object sender, RoutedEventArgs e)
        {
            CombinedGraphs.Plot.Remove(velocityPlot);
            CombinedGraphs.Refresh();
        }

        private void CushionForce_Checked(object sender, RoutedEventArgs e)
        {
            List<double> cushionForce = DataContainer.Instance.CushionForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] cushionForceY = cushionForce.ToArray();
            double[] dataX = timeStamp.ToArray();

            cushionForcePlot = CombinedGraphs.Plot.AddScatter(dataX, cushionForceY, markerSize: 3);
            cushionForcePlot.Smooth = true;
            CombinedGraphs.Render();
        }

        private void CushionForce_Unchecked(object sender, RoutedEventArgs e)
        {
            CombinedGraphs.Plot.Remove(cushionForcePlot);
            CombinedGraphs.Refresh();
        }

        private void CushionPosition_Checked(object sender, RoutedEventArgs e)
        {
            List<double> cushionPosition = DataContainer.Instance.CushionPosition;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] cushionPositionY = cushionPosition.ToArray();
            double[] dataX = timeStamp.ToArray();

            cushionPositionPlot = CombinedGraphs.Plot.AddScatter(dataX, cushionPositionY, markerSize: 3);
            cushionPositionPlot.Smooth = true;
            CombinedGraphs.Render();
        }

        private void CushionPosition_Unchecked(object sender, RoutedEventArgs e)
        {
            CombinedGraphs.Plot.Remove(cushionPositionPlot);
            CombinedGraphs.Refresh();
        }
    }
}
