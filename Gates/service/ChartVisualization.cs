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

        public void showChartForXor(TrainingResultMP tResultMP, TrainingSetings trainSettings)
        {
            list0 = new List<PointInfo>();
            list1 = new List<PointInfo>();
            activatonFunctions.maxError = trainSettings.maxError;

            preparePointListForXor(tResultMP, trainSettings.activiationFunction);

            float[,] table0 = changeListToArray(list0);
            float[,] table1 = changeListToArray(list1);

            ChartForm chartForm = new ChartForm(table0, table1);
            chartForm.Show();
        }

        public void preparePointListForXor(TrainingResultMP trainingResult, TrainingSetings.ActiviationFunction activationFunction)
        {

            float x1 = 0.01f;
            float x2 = 0.01f;

            for (int x = 0; x <= 100; x++)
            {
                x2 = 0.01f;
                for (int y = 0; y <= 100; y++)
                {

                    PointInfo point = preparePointForXor(trainingResult, x1, x2, activationFunction);

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

        public PointInfo preparePointForXor(TrainingResultMP trainingResult, float x, float y, TrainingSetings.ActiviationFunction activiationFunction)
        {

            float h1 = x * trainingResult.w1 + y + trainingResult.w3 + trainingResult.biasI;
            float h2 = x * trainingResult.w2 + y + trainingResult.w4 + trainingResult.biasI2;

            if (activiationFunction == TrainingSetings.ActiviationFunction.JUMP)
            {
                h1 = activatonFunctions.jumpActivationFuntion(h1);
                h2 = activatonFunctions.jumpActivationFuntion(h2);
            }
            else
            {
                h1 = activatonFunctions.sigmoidActivationFunction(h1);
                h1 = activatonFunctions.correctionForSigmoid(h1);
                h2 = activatonFunctions.sigmoidActivationFunction(h2);
                h2 = activatonFunctions.correctionForSigmoid(h2);
            }

            float result = h1 * trainingResult.wh1 + h2 * trainingResult.wh2 + trainingResult.biasO;


            if (activiationFunction == TrainingSetings.ActiviationFunction.JUMP)
            {
                result = activatonFunctions.jumpActivationFuntion(result);
            }
            else
            {
                result = activatonFunctions.sigmoidActivationFunction(result);
                result = activatonFunctions.correctionForSigmoid(result);
            }

            return new PointInfo(x, y, result);
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
