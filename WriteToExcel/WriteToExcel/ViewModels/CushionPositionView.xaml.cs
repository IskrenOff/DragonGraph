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

                CushionPosition.Plot.AddScatter(dataX, dataY, color: System.Drawing.Color.Orange);
                CushionPosition.Render();


                //Adding crosshaid lines
                var crosshairVertical = CushionPosition.Plot.AddVerticalLine(0);
                var crosshairHorizontal = CushionPosition.Plot.AddHorizontalLine(0);

                //Color of the crosshair
                crosshairVertical.Color = System.Drawing.Color.Red;
                crosshairHorizontal.Color = System.Drawing.Color.Red;

                // Create text objects to display crosshair position
                var labelVertical = CushionPosition.Plot.AddText("0", x: 0, y: 0, color: System.Drawing.Color.Yellow);
                var labelHorizontal = CushionPosition.Plot.AddText("0", x: 0, y: 0, color: System.Drawing.Color.Yellow);

                // Subscribe to the MouseMoved event to update the crosshair position
                CushionPosition.MouseMove += (s, e) =>
                {
                    // Get the mouse coordinates in plot space
                    (double mouseX, double mouseY) = CushionPosition.GetMouseCoordinates();

                    // Update the crosshair position with the mouse coordinates
                    crosshairVertical.X = mouseX;
                    crosshairHorizontal.Y = mouseY;



                    // Update the labels to display crosshair position
                    labelVertical.X = mouseX;
                    labelVertical.Y = mouseY - 500; // adjust label position
                    labelVertical.Label = mouseX.ToString("F2");

                    labelHorizontal.Y = mouseY;
                    labelHorizontal.X = mouseX + 5; // adjust label position
                    labelHorizontal.Label = mouseY.ToString("F2");


                    // Redraw the plot
                    CushionPosition.Render();
                };
            }
            
        }
    }
}
