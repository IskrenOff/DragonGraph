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
    /// Interaction logic for CushionForceView.xaml
    /// </summary>
    public partial class CushionForceView : UserControl
    {
        public CushionForceView()
        {
            InitializeComponent();
            ProcessData();
        }

        public void ProcessData()
        {
            List<double> cushionForce = DataContainer.Instance.CushionForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] dataY = cushionForce.ToArray();
            double[] dataX = timeStamp.ToArray();

            if (cushionForce == null || cushionForce.Count == 0)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                CushionForce.Plot.Clear();

                CushionForce.Plot.XLabel("Time Stamp (mm/s)");
                CushionForce.Plot.YLabel("Cushion Force (kN)");
                CushionForce.Plot.Title("Cushion Force Graph");

                //Customize the layout and labels colors
                CushionForce.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                CushionForce.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                CushionForce.Plot.XAxis.Label(color: System.Drawing.Color.White);
                CushionForce.Plot.YAxis.Label(color: System.Drawing.Color.White);

                CushionForce.Plot.AddScatter(dataX, dataY, color: System.Drawing.Color.Magenta, markerSize: 3).Smooth = true;
                CushionForce.Render();

                Crosshair cross = CushionForce.Plot.AddCrosshair(25, .5);

                // Subscribe to the MouseMoved event to update the crosshair position
                CushionForce.MouseMove += (s, e) =>
                {
                    // Get the mouse coordinates in plot space
                    (double mouseX, double mouseY) = CushionForce.GetMouseCoordinates();

                    // Update the crosshair position with the mouse coordinates
                    cross.X = mouseX;
                    cross.Y = mouseY;

                    CushionForce.Render();
                };
            }

            
        }
    }
}
