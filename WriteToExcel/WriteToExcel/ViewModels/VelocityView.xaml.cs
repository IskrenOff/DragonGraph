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
    /// Interaction logic for VelocityView.xaml
    /// </summary>
    public partial class VelocityView : UserControl
    {
        public VelocityView()
        {
            InitializeComponent();
            ProcessData();
        }

        public void ProcessData()
        {
            List<double> velocity = DataContainer.Instance.Velocity;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] dataY = velocity.ToArray();
            double[] dataX = timeStamp.ToArray();

            if (velocity == null || velocity.Count == 0)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                Velocity.Plot.Clear();

                Velocity.Plot.XLabel("Time Stamp");
                Velocity.Plot.YLabel("Velocity");
                Velocity.Plot.Title("Velocity Graph");

                //Customize the layout and labels colors
                Velocity.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                Velocity.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                Velocity.Plot.XAxis.Label(color: System.Drawing.Color.White);
                Velocity.Plot.YAxis.Label(color: System.Drawing.Color.White);

                Velocity.Plot.AddScatter(dataX, dataY, color: System.Drawing.Color.Aquamarine, markerSize: 3).Smooth = true;
                Velocity.Render();

                Crosshair cross = Velocity.Plot.AddCrosshair(25, .5);

                // Subscribe to the MouseMoved event to update the crosshair position
                Velocity.MouseMove += (s, e) =>
                {
                    // Get the mouse coordinates in plot space
                    (double mouseX, double mouseY) = Velocity.GetMouseCoordinates();

                    // Update the crosshair position with the mouse coordinates
                    cross.X = mouseX;
                    cross.Y = mouseY;

                    Velocity.Render();
                };
            }

           
        }
    }
}
