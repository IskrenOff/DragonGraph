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

            CushionForce.Plot.Clear();

            CushionForce.Plot.XLabel("Time Stamp");
            CushionForce.Plot.YLabel("Cushion Force");
            CushionForce.Plot.Title("Cushion Force Graph");

            //Customize the layout and labels colors
            CushionForce.Plot.Style(ScottPlot.Style.Blue1);
            var bnColor = System.Drawing.ColorTranslator.FromHtml("#1C0E49");
            CushionForce.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
            CushionForce.Plot.XAxis.Label(color: System.Drawing.Color.White);
            CushionForce.Plot.YAxis.Label(color: System.Drawing.Color.White);

            CushionForce.Plot.AddScatter(dataX, dataY, color: System.Drawing.Color.Magenta);
            CushionForce.Render();


            //Adding crosshaid lines
            var crosshairVertical = CushionForce.Plot.AddVerticalLine(0);
            var crosshairHorizontal = CushionForce.Plot.AddHorizontalLine(0);

            //Color of the crosshair
            crosshairVertical.Color = System.Drawing.Color.Red;
            crosshairHorizontal.Color = System.Drawing.Color.Red;

            // Create text objects to display crosshair position
            var labelVertical = CushionForce.Plot.AddText("0", x: 0, y: 0, color: System.Drawing.Color.Yellow);
            var labelHorizontal = CushionForce.Plot.AddText("0", x: 0, y: 0, color: System.Drawing.Color.Yellow);

            // Subscribe to the MouseMoved event to update the crosshair position
            CushionForce.MouseMove += (s, e) =>
            {
                // Get the mouse coordinates in plot space
                (double mouseX, double mouseY) = CushionForce.GetMouseCoordinates();

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
                CushionForce.Render();
            };
        }
    }
}
