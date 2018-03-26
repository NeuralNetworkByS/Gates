using Gates.config;
using Gates.form;
using Gates.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.service
{
    public class ChartVisualization
    {
        private ActivatonFunctions activatonFunctions = new ActivatonFunctions();

        private List<PointInfo> list0 = new List<PointInfo>();
        private List<PointInfo> list1 = new List<PointInfo>();

        public void showChart(TrainingResult trainingResult, TrainingSetings trainSettings)
        {
            preparePointList(trainingResult, trainSettings);

            float[,] table0 = changeListToArray(list0);
            float[,] table1 = changeListToArray(list1);

            ChartForm chartForm = new ChartForm(table0, table1);
            chartForm.Show();
        }

        public void preparePointList(TrainingResult trainingResult, TrainingSetings trainSettings)
        {
           
            for (int x = -100; x <= 100; x++)
            {
                for (int y = -100; y <= 100; y++)
                {
                    PointInfo point = preparePoint(trainingResult, x, y, trainSettings.activiationFunction);

                    if (point.color == 0.00f)
                    {
                        list0.Add(point);
                    }
                    else
                    {
                        list1.Add(point);
                    }
                }
            }
        }

        public PointInfo preparePoint(TrainingResult trainingResult, int x, int y, TrainingSetings.ActiviationFunction activiationFunction)
        {

            float e = ((float)x) * trainingResult.w1 + ((float)y) * trainingResult.w1 + trainingResult.bias;
            float result = 0.00f;

            if (activiationFunction == TrainingSetings.ActiviationFunction.JUMP)
            {
                result = activatonFunctions.jumpActivationFuntion(e);
            }
            else
            {
                result = activatonFunctions.sigmoidActivationFunction(e);
            }

            return new PointInfo(x, y, result);
        }

        public float[,] changeListToArray(List<PointInfo> points)
        {
            float[,] finalPoints = new float[points.Count, 3];

            for (int i = 0; i < points.Count; i++)
            {
                finalPoints[i, 0] = points[i].x;
                finalPoints[i, 1] = points[i].y;
            }

            return finalPoints;
        }
    }
}
