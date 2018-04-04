﻿using Gates.config;
using Gates.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gates.service
{
    public class TrainingService
    {

        private TrainingSetings trainingSetings;
        private GateTrainingValuesContainer valuesContainer;

        private StringBuilder firstLineBuilder;
        private TrainingResultP trainingResult;

        private ActivatonFunctions activationFunctions = new ActivatonFunctions();


        public TrainingResultP train(TrainingSetings settings, GateTrainingValuesContainer valuesContainer)
        {
            this.trainingSetings = settings;
            this.valuesContainer = valuesContainer;
            activationFunctions.maxError = trainingSetings.maxError;
            firstLineBuilder = new StringBuilder();
            firstLineBuilder.Append("Trenowanie: ");
            firstLineBuilder.Append(" bramka: " + trainingSetings.gateType.ToString() + ", ");

            switch (settings.activiationFunction)
            {
                case TrainingSetings.ActiviationFunction.JUMP:
                    firstLineBuilder.Append(" funkcja skokowa, ");
                    trainByJumpFunction();
                    break;
                case TrainingSetings.ActiviationFunction.SIGMOID:
                    firstLineBuilder.Append(" funkcja sigmoidowa, ");
                    trainBySigmoidFunction();
                    break;
                default:
                    throw new ArgumentException("Brak wybranej fukcji");
            }

            return trainingResult;
        }

        private void trainByJumpFunction()
        {
            switch (trainingSetings.neuronsNumbers)
            {
                case TrainingSetings.NeuronsNumbers.ONE:
                    trainByJumpFunctionAndOneNeuron();
                    break;
                case TrainingSetings.NeuronsNumbers.TWO:
                    trainByJumpFunctionAndTwoNeuron();
                    break;
                default:
                    throw new ArgumentException("Nieobsługiwana ilość neuronów");
            }
        }

        private void trainBySigmoidFunction()
        {
            switch (trainingSetings.neuronsNumbers)
            {
                case TrainingSetings.NeuronsNumbers.ONE:
                    trainBySigmoidFunctionAndOneNeuron();
                    break;
                case TrainingSetings.NeuronsNumbers.TWO:
                    trainBySigmoidFunctionAndTwoNeuron();
                    break;
                default:
                    throw new ArgumentException("Nieobsługiwana ilość neuornów");
            }
        }


        private void trainByJumpFunctionAndOneNeuron()
        {
            trainByOneNeuron(activationFunctions.jumpActivationFuntion);
        }

        private void trainByJumpFunctionAndTwoNeuron()
        {
            trainByTwoNeurons(activationFunctions.jumpActivationFuntion);
        }

        private void trainBySigmoidFunctionAndOneNeuron()
        {
            trainByOneNeuron(activationFunctions.sigmoidActivationFunction);
        }

        private void trainBySigmoidFunctionAndTwoNeuron()
        {
            trainByTwoNeurons(activationFunctions.sigmoidActivationFunction);
        }

        private void trainByOneNeuron(ActivatonFunctions.Del activationFunction)
        {
            if (trainingSetings.gateType == TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Brak możliwości trenowanie XOR jednym nuronem");
            }

            trainingResult = new TrainingResultP();
            firstLineBuilder.Append(" pojedynczy neuron.");

            trainingResult.raport.Add(firstLineBuilder.ToString());

            bool isWeightChanged = true;
            Random random = new Random();

            float w1 = (float) random.NextDouble();
            float w2 = (float)random.NextDouble();
            float bias = 1.00f;
            float e = 0.00f;
            float y = 0.00f;

            trainingResult.raport.Add("Początkowe w1: " + w1);
            trainingResult.raport.Add("Początkowe w2: " + w2);
            trainingResult.raport.Add("Początkowy bias: " + bias);


            List<float> desireResults = valuesContainer.results[trainingSetings.gateType.ToString()];

            if (desireResults == null)
            {
                Debug.WriteLine("DesireResults jest nullem");
            }
            else
            {
                Debug.WriteLine("desireResults");
                foreach (float result in desireResults)
                {
                    Debug.WriteLine(result);
                }
            }

            Debug.WriteLine("Typ bramki do szkolenia: " + trainingSetings.gateType);
            Debug.WriteLine("Funkcja aktywacyjna: " + trainingSetings.activiationFunction);


            int epochs = 0;
            while (isWeightChanged)
            {
                isWeightChanged = false;
                epochs++;

                for (int i = 0; i < valuesContainer.x1Values.Count; i++)
                {
                    e = w1 * valuesContainer.x1Values[i] + w2 * valuesContainer.x2Values[i] + bias;
                    y = activationFunction(e);

                    if (y != desireResults[i])
                    {
                        trainingResult.raport.Add("");
                        trainingResult.raport.Add("desire = " + desireResults[i] + ", y = " + y + ", e: " + e);
                        trainingResult.raport.Add("Zmiana w1, w2, bias");
                        
                        w1 = w1 + trainingSetings.learningRate * (desireResults[i] - y) * valuesContainer.x1Values[i];
                        w2 = w2 + trainingSetings.learningRate * (desireResults[i] - y) * valuesContainer.x2Values[i];
                        bias = bias + trainingSetings.learningRate * (desireResults[i] - y);

                        trainingResult.raport.Add("w1: " + w1);
                        trainingResult.raport.Add("w2: " + w2);
                        trainingResult.raport.Add("bias: " + bias);

                        isWeightChanged = true;
                    }
                }

            }

            trainingResult.w1 = w1;
            trainingResult.w2 = w2;
            trainingResult.biasI = bias;

            trainingResult.raport.Add("Finalne w1: " + w1);
            trainingResult.raport.Add("Finalene w2: " + w2);
            trainingResult.raport.Add("Finalne bias: " + bias);
            trainingResult.raport.Add("Liczba epok: " + epochs);
        }

        private void trainByTwoNeurons(ActivatonFunctions.Del activationFuncation)
        {
            firstLineBuilder.Append(" dwa neurony.");
            //throw new NotImplementedException("Not implemented");

            if (trainingSetings.gateType != TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Brak możliwość trenowania z wykorzystaniem 2 neuronów tylko bramki xor");
            }

            TrainingResultMP trainingResult = new TrainingResultMP();
            trainingResult.initialize();

            bool isChanged = true;

            float gradientOutput = 0.00f;
            float gradientH1 = 0.00f;
            float gradientH2 = 0.00f;
            float output = 0.00f;

            while (isChanged)
            {
                isChanged = false;

                for (int i = 0; i < valuesContainer.results.Count; i++)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine("x1: {0}, x2: {1}, d: {2}", valuesContainer.x1Values[i], valuesContainer.x2Values[i], valuesContainer.results[trainingSetings.gateType.ToString()][i]);

                    trainingResult.h1 =
                    activationFuncation(trainingResult.w1 * valuesContainer.x1Values[i] + trainingResult.w3 * valuesContainer.x2Values[i] + trainingResult.biasI);

                    trainingResult.h2 =
                    activationFuncation(trainingResult.w2 * valuesContainer.x1Values[i] + trainingResult.w4 * valuesContainer.x2Values[i] + trainingResult.biasI2);

                    output = trainingResult.h1 * trainingResult.wh1 + trainingResult.h2 * trainingResult.wh2 + trainingResult.biasO;
                    Debug.WriteLine("Output przed aktywacją: " + output);
                    output = activationFuncation(output);
                    Debug.WriteLine("Output po aktywacji: " + output);

                    Debug.WriteLine("Oczekiwana wartość: " + valuesContainer.results[trainingSetings.gateType.ToString()][i]);


                    if (output != valuesContainer.results[trainingSetings.gateType.ToString()][i])
                    {
                        Debug.WriteLine("Zmiana wag itp");
                        isChanged = true;

                        gradientOutput = (valuesContainer.results[trainingSetings.gateType.ToString()][i] - output);
                        Debug.WriteLine("gradientOtput: " + gradientOutput);
                        gradientH1 = gradientOutput * trainingResult.wh1;
                        gradientH2 = gradientOutput * trainingResult.wh2;

                        trainingResult.biasO += trainingSetings.learningRate * gradientOutput;
                        Debug.WriteLine("trainingSetings.learningRate * gradientOutput: " + trainingSetings.learningRate * gradientOutput);
                        trainingResult.wh1 += trainingSetings.learningRate * gradientOutput * trainingResult.h1;
                        trainingResult.wh2 += trainingSetings.learningRate * gradientOutput * trainingResult.h2;

                        trainingResult.biasI += trainingSetings.learningRate * gradientH1;
                        trainingResult.w1 += trainingSetings.learningRate * gradientH1 * valuesContainer.x1Values[i];
                        trainingResult.w3 += trainingSetings.learningRate * gradientH1 * valuesContainer.x1Values[i];

                        trainingResult.biasI2 += trainingSetings.learningRate * gradientH2;
                        trainingResult.w2 += trainingSetings.learningRate * gradientH2 * valuesContainer.x2Values[i];
                        trainingResult.w4 += trainingSetings.learningRate * gradientH2 * valuesContainer.x2Values[i];

                        Debug.WriteLine("biasO: " + trainingResult.biasO);
                        Debug.WriteLine("wh1: " + trainingResult.wh1);
                        Debug.WriteLine("wh2: " + trainingResult.wh2);

                        Debug.WriteLine("biasI: " + trainingResult.biasI);
                        Debug.WriteLine("w1: " + trainingResult.w1);
                        Debug.WriteLine("w2: " + trainingResult.w2);

                        Debug.WriteLine("biasI2: " + trainingResult.biasI2);
                        Debug.WriteLine("w2: " + trainingResult.w2);
                        Debug.WriteLine("w4: " + trainingResult.w4);


                    }

                }

                
            }

            Debug.WriteLine("Trening Zakończony");

        }

        


    }
}
