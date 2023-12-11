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
            List<double> velocity = DataContainer.Instance.Velocity;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] slideForceY = slideForce.ToArray();
            double[] dataX = timeStamp.ToArray();



            if (slideForce.Count == 0 || slideForce == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }

            else
            {
                CombinedGraphs.Plot.Clear();

                CombinedGraphs.Plot.XLabel("X Axis ");
                CombinedGraphs.Plot.YLabel("Y Axis");
                CombinedGraphs.Plot.Title("Combined Graphs");

                //Customize the layout and labels colors
                CombinedGraphs.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                CombinedGraphs.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                CombinedGraphs.Plot.XAxis.Label(color: System.Drawing.Color.White);
                CombinedGraphs.Plot.YAxis.Label(color: System.Drawing.Color.White);

                CombinedGraphs.Plot.AddScatter(dataX, slideForceY, color: System.Drawing.Color.Aquamarine, markerSize: 3).Smooth = true;
                CombinedGraphs.Render();

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
    }
}
