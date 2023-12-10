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
    /// Interaction logic for CushionPositionView.xaml
    /// </summary>
    public partial class CushionPositionView : UserControl
    {
        public CushionPositionView()
        {
            InitializeComponent();
            ProcessData();
        }

        public void ProcessData()
        {
            List<double> cushionPosition = DataContainer.Instance.CushionPosition;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] dataY = cushionPosition.ToArray();
            double[] dataX = timeStamp.ToArray();

            if (cushionPosition == null || cushionPosition.Count == 0)
            {
                Content = new WriteToExcel.ViewModels.NoDataView();
            }
            else
            {
                CushionPosition.Plot.Clear();

                CushionPosition.Plot.XLabel("Time Stamp");
                CushionPosition.Plot.YLabel("Cushion Position");
                CushionPosition.Plot.Title("Cushion Position Graph");

                //Customize the layout and labels colors
                CushionPosition.Plot.Style(ScottPlot.Style.Blue1);
                var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
                CushionPosition.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
                CushionPosition.Plot.XAxis.Label(color: System.Drawing.Color.White);
                CushionPosition.Plot.YAxis.Label(color: System.Drawing.Color.White);

                CushionPosition.Plot.AddScatter(dataX, dataY, color: System.Drawing.Color.Orange).Smooth = true;
                CushionPosition.Render();

                Crosshair cross = CushionPosition.Plot.AddCrosshair(25, .5);

                // Subscribe to the MouseMoved event to update the crosshair position
                CushionPosition.MouseMove += (s, e) =>
                {
                    // Get the mouse coordinates in plot space
                    (double mouseX, double mouseY) = CushionPosition.GetMouseCoordinates();

                    // Update the crosshair position with the mouse coordinates
                    cross.X = mouseX;
                    cross.Y = mouseY;

                    CushionPosition.Render();
                };
            }
            
        }
    }
}
