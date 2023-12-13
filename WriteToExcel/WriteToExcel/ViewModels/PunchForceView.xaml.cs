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
    /// Interaction logic for PunchForceView.xaml
    /// </summary>
    public partial class PunchForceView : UserControl
    {
        public PunchForceView()
        {
            InitializeComponent();
            ProcessData();

        }

        public void ProcessData()
        {
            List<double> punchForce = DataContainer.Instance.PunchForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] punchY = punchForce.ToArray();
            double[] dataX = timeStamp.ToArray();



            if (punchForce.Count == 0 || punchForce == null)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }

            else
            {
                PunchForce.Plot.Clear();

                PunchForce.Plot.XLabel("Time Stamp");
                PunchForce.Plot.YLabel("Punch Force (kN)");
                PunchForce.Plot.Title("Punch Force Graph");
                //Customize the layout and labels colors
                PunchForce.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                PunchForce.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                PunchForce.Plot.XAxis.Label(color: System.Drawing.Color.White);
                PunchForce.Plot.YAxis.Label(color: System.Drawing.Color.White);

                PunchForce.Plot.AddScatter(dataX, punchY, color: System.Drawing.Color.Yellow, markerSize: 3).Smooth = true;
                PunchForce.Render();

                Crosshair cross = PunchForce.Plot.AddCrosshair(25, .5);

                // Subscribe to the MouseMoved event to update the Crosshair position
                PunchForce.MouseMove += (s, e) =>
                {
                    (double mouseX, double mouseY) = PunchForce.GetMouseCoordinates();
                    cross.X = mouseX;
                    cross.Y = mouseY;

                    PunchForce.Render();
                };
            }
        }
    }
}
