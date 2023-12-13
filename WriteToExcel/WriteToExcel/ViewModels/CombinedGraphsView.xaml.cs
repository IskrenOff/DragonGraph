using ScottPlot;
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

        ScatterPlot slideForcePlot;
        ScatterPlot velocityPlot;
        ScatterPlot cushionForcePlot;
        ScatterPlot cushionPositionPlot;

        public void ProcessData()
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
            List<double> velocity = DataContainer.Instance.Velocity;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            List<double> cushionForce = DataContainer.Instance.CushionForce;
            List<double> cushionPosition = DataContainer.Instance.CushionPosition;
            double[] slideForceY = slideForce.ToArray();
            double[] velocityY = velocity.ToArray();
            double[] cushionForceY = cushionForce.ToArray();
            double[] cushionPositionY = cushionPosition.ToArray();
            double[] dataX = timeStamp.ToArray();

            var legend = CombinedGraphs.Plot.Legend();

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

                legend.FontSize = 13;
                legend.Location = Alignment.UpperRight;

                if (SlideForceBox.IsChecked == true)
                {
                    slideForcePlot = CombinedGraphs.Plot.AddScatter(dataX, slideForceY, System.Drawing.Color.Aquamarine, markerSize: 3, label: "Slide Force");
                    slideForcePlot.Smooth = true;
                    CombinedGraphs.Render();
                }
                if (VelocityBox.IsChecked == true)
                {
                    velocityPlot = CombinedGraphs.Plot.AddScatter(dataX, velocityY, System.Drawing.Color.GreenYellow, markerSize: 3, label: "Velocity");
                    velocityPlot.Smooth = true;
                    CombinedGraphs.Render();
                }
                if (CushionForceBox.IsChecked == true)
                {
                    cushionForcePlot = CombinedGraphs.Plot.AddScatter(dataX, cushionForceY, color: System.Drawing.Color.Magenta, markerSize: 3, label: "Cushion Force");
                    cushionForcePlot.Smooth = true;
                    //legend.Location = Alignment.UpperRight;
                    CombinedGraphs.Render();
                }
                if (CushionPositionBox.IsChecked == true)
                {
                    cushionPositionPlot = CombinedGraphs.Plot.AddScatter(dataX, cushionPositionY, color: System.Drawing.Color.Orange, markerSize: 3, label: "Cushin Position");
                    cushionPositionPlot.Smooth = true;
                    CombinedGraphs.Render();
                }


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


        private void SlideForce_Checked(object sender, RoutedEventArgs e)
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] slideForceY = slideForce.ToArray();
            double[] dataX = timeStamp.ToArray();

            var legend = CombinedGraphs.Plot.Legend();

            if (slideForce.Count == 0 || slideForce == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                slideForcePlot = CombinedGraphs.Plot.AddScatter(dataX, slideForceY, System.Drawing.Color.Aquamarine, markerSize: 3, label: "Slide Force");
                slideForcePlot.Smooth = true;
                legend.Location = Alignment.UpperRight;
                CombinedGraphs.Render();
            }
            
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

            var legend = CombinedGraphs.Plot.Legend();

            if (velocity.Count == 0 || velocity == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                velocityPlot = CombinedGraphs.Plot.AddScatter(dataX, velocityY, System.Drawing.Color.GreenYellow, markerSize: 3, label: "Velocity");
                velocityPlot.Smooth = true;
                legend.Location = Alignment.UpperRight;
                CombinedGraphs.Render();
            }
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

            if (cushionForce.Count == 0 || cushionForce == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                cushionForcePlot = CombinedGraphs.Plot.AddScatter(dataX, cushionForceY, color: System.Drawing.Color.Magenta, markerSize: 3, label: "Cushion Force");
                cushionForcePlot.Smooth = true;
                CombinedGraphs.Render();
            }        
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

            if (cushionPosition.Count == 0 || cushionPosition == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                cushionPositionPlot = CombinedGraphs.Plot.AddScatter(dataX, cushionPositionY, color: System.Drawing.Color.Orange, markerSize: 3, label : "Cushion Position");
                cushionPositionPlot.Smooth = true;
                CombinedGraphs.Render();
            }          
        }

        private void CushionPosition_Unchecked(object sender, RoutedEventArgs e)
        {
            CombinedGraphs.Plot.Remove(cushionPositionPlot);
            CombinedGraphs.Refresh();
        }
    }
}
