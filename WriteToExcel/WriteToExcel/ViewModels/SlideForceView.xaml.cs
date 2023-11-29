using ScottPlot;
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

        public void ProcessData()
        {
            List<double> slideForce = DataContainer.Instance.SlideForce;
            List<double> timeStamp = DataContainer.Instance.TimeStamp;
            double[] dataY = slideForce.ToArray();
            double[] dataX = timeStamp.ToArray();

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

            //Adding crosshaid lines
            var crosshairVertical = SlideForce.Plot.AddVerticalLine(0);
            var crosshairHorizontal = SlideForce.Plot.AddHorizontalLine(0);

            //Color of the crosshair
            crosshairVertical.Color = System.Drawing.Color.HotPink;
            crosshairHorizontal.Color = System.Drawing.Color.HotPink;

            // Create text objects to display crosshair position
            var labelVertical = SlideForce.Plot.AddText("0", x: 0, y: 0, color: System.Drawing.Color.Yellow);
            var labelHorizontal = SlideForce.Plot.AddText("0", x: 0, y: 0, color: System.Drawing.Color.Yellow);


            // Subscribe to the MouseMoved event to update the crosshair position
            SlideForce.MouseMove += (s, e) =>
            {
                // Get the mouse coordinates in plot space
                (double mouseX, double mouseY) = SlideForce.GetMouseCoordinates();

                // Update the crosshair position with the mouse coordinates
                crosshairVertical.X = mouseX;
                crosshairHorizontal.Y = mouseY;

                // Update the labels to display crosshair position
                labelVertical.X = mouseX;
                labelVertical.Y = mouseY - 500; // adjust label position
                labelVertical.Label = mouseX.ToString("F2");

                labelHorizontal.X = mouseX + 5; // adjust label position
                labelHorizontal.Y = mouseY;
                labelHorizontal.Label = mouseY.ToString("F2");

                // Redraw the plot
                SlideForce.Render();

            };
        }

        public SlideForceView()
        {
            InitializeComponent();
            ProcessData();

        }

    }
}
