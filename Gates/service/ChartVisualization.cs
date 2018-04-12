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

        private List<PointInfo> list0;
        private List<PointInfo> list1;

        public void showChart(TrainingResultP trainingResult, TrainingSetings trainSettings)
        {
            list0 = new List<PointInfo>();
            list1 = new List<PointInfo>();
            activatonFunctions.maxError = trainSettings.maxError;

            preparePointList(trainingResult, trainSettings.activiationFunction);

            float[,] table0 = changeListToArray(list0);
            float[,] table1 = changeListToArray(list1);

            ChartForm chartForm = new ChartForm(table0, table1);
            chartForm.Show();
        }

        public void showChartForXor(XorTrResult xorTrResult, TrainingSetings trainSettings)
        {
            list0 = new List<PointInfo>();
            list1 = new List<PointInfo>();
            activatonFunctions.maxError = trainSettings.maxError;

            preparePointListForXor(xorTrResult, trainSettings.activiationFunction);

            float[,] table0 = changeListToArray(list0);
            float[,] table1 = changeListToArray(list1);

            ChartForm chartForm = new ChartForm(table0, table1);
            chartForm.Show();
        }

        public void preparePointListForXor(XorTrResult xorTrResult, TrainingSetings.ActiviationFunction activationFunction)
        {

            float x1 = 0.01f;
            float x2 = 0.01f;

            for (int x = 0; x <= 100; x++)
            {
                x2 = 0.01f;
                for (int y = 0; y <= 100; y++)
                {

                    PointInfo point = preparePointForXor(xorTrResult, x1, x2, activationFunction);
                    point.color = activatonFunctions.correctionForSigmoid(point.color);

                    if (point.color == 0.00f)
                    {
                        list0.Add(point);
                    }
                    else
                    {
                        list1.Add(point);
                    }

                    x2 += 0.01f;
                }

                x1 += 0.01f;
            }
        }

        public void preparePointList(TrainingResultP trainingResult, TrainingSetings.ActiviationFunction activationFunction)
        {

            float x1 = 0.01f;
            float x2 = 0.01f;

            for (int x = 0; x <= 100; x++)
            {
                x2 = 0.01f;
                for (int y = 0; y <= 100; y++)
                {
                  
                    PointInfo point = preparePoint(trainingResult, x1, x2, activationFunction);

                    if (point.color == 0.00f)
                    {
                        list0.Add(point);
                    }
                    else
                    {
                        list1.Add(point);
                    }

                    x2 += 0.01f;
                }

                x1 += 0.01f;
            }
        }

        public PointInfo preparePointForXor(XorTrResult xorTrResult, float x, float y, TrainingSetings.ActiviationFunction activiationFunction)
        {
            xorTrResult.hNeuron1.inputs = new float[] { x, y };
            xorTrResult.hNeuron2.inputs = new float[] { x, y };

            xorTrResult.oNeuron.inputs = new float[] { xorTrResult.hNeuron1.output(), xorTrResult.hNeuron2.output() };

            return new PointInfo(x, y, xorTrResult.oNeuron.output());
        }

        public PointInfo preparePoint(TrainingResultP trainingResult, float x, float y, TrainingSetings.ActiviationFunction activiationFunction)
        {

            float e = x * trainingResult.w1 + y * trainingResult.w1 + trainingResult.biasI;
            float result = 0.00f;

           
            if (activiationFunction == TrainingSetings.ActiviationFunction.JUMP)
            {
                result = activatonFunctions.jumpActivationFuntion(e);
            }
            else
            {
                result = activatonFunctions.sigmoidActivationFunction(e);
                result = activatonFunctions.correctionForSigmoid(result);
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
