using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gates.model;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Gates.form
{
    public partial class ChartForm : Form
    {
        private float[,] list0;
        private float[,] list1;
   
        public ChartForm()
        {
            InitializeComponent();
        }

        public ChartForm(float[,] list0, float[,] list1)
        {
            InitializeComponent();
            this.list0 = list0;
            this.list1 = list1;
            
        }

        // Initial plot setup, modify this as needed
        private void ilPanel1_Load(object sender, EventArgs e)
        {
            ILPlotCube plotCube = new ILPlotCube();

            ILPoints points = new ILPoints();
            points.Positions = list0;
            points.Color = Color.Red;

            ILPoints points2 = new ILPoints();
            points2.Positions = list1;
            points2.Color = Color.Blue;

            plotCube.Add(points);
            plotCube.Add(points2);

            ilPanel1.Scene.Add(plotCube);

        }

        /// <summary>
        /// Example update function used for dynamic updates to the plot
        /// </summary>
        /// <param name="A">New data, matching the requirements of your plot</param>
        public void Update(ILInArray<double> A)
        {
            using (ILScope.Enter(A))
            {

                // fetch a reference to the plot
                var plot = ilPanel1.Scene.First<ILLinePlot>(tag: "mylineplot");
                if (plot != null)
                {
                    // make sure, to convert elements to float
                    plot.Update(ILMath.tosingle(A));
                    //
                    // ... do more manipulations here ...

                    // finished with updates? -> Call Configure() on the changes 
                    plot.Configure();

                    // cause immediate redraw of the scene
                    ilPanel1.Refresh();
                }

            }
        }

        /// <summary>
        /// Example computing module as private class 
        /// </summary>
        private class Computation : ILMath
        {
            /// <summary>
            /// Create some test data for plotting
            /// </summary>
            /// <param name="ang">end angle for a spiral</param>
            /// <param name="resolution">number of points to plot</param>
            /// <returns>3d data matrix for plotting, points in columns</returns>
            public static ILRetArray<float> CreateData(int ang, int resolution)
            {
                using (ILScope.Enter())
                {
                    ILArray<float> A = linspace<float>(0, ang * pi, resolution);
                    ILArray<float> Pos = zeros<float>(3, A.S[1]);
                    Pos["0;:"] = sin(A);
                    Pos["1;:"] = cos(A);
                    Pos["2;:"] = A;
                    return Pos;
                }
            }

        }
    }
}
