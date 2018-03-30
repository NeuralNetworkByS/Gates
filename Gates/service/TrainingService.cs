using Gates.config;
using Gates.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.service
{
    public class TrainingService
    {

        private TrainingSetings trainingSetings;
        private GateTrainingValuesContainer valuesContainer;

        private StringBuilder firstLineBuilder;
        private TrainingResult trainingResult;

        private ActivatonFunctions activationFunctions = new ActivatonFunctions();


        public TrainingResult train(TrainingSetings settings, GateTrainingValuesContainer valuesContainer)
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
            if (trainingSetings.gateType != TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Funkcja dwóch neuronó tylko dla XOR");
            }

            throw new NotImplementedException("Not implemented");
        }

        private void trainBySigmoidFunctionAndOneNeuron()
        {
            trainByOneNeuron(activationFunctions.sigmoidActivationFunction);
        }

        private void trainBySigmoidFunctionAndTwoNeuron()
        {
            if (trainingSetings.gateType != TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Funkcja dwóch neuronów tylko dla XOR");
            }
        }

        private void trainByOneNeuron(ActivatonFunctions.Del activationFunction)
        {
            if (trainingSetings.gateType == TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Brak możliwości trenowanie XOR jednym nuronem");
            }

            trainingResult = new TrainingResult();
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
            trainingResult.bias = bias;

            trainingResult.raport.Add("Finalne w1: " + w1);
            trainingResult.raport.Add("Finalene w2: " + w2);
            trainingResult.raport.Add("Finalne bias: " + bias);
            trainingResult.raport.Add("Liczba epok: " + epochs);
        }

        private void trainByTwoNeurons(ActivatonFunctions.Del activationFuncation)
        {
            firstLineBuilder.Append(" dwa neurony.");
            throw new NotImplementedException("Not implemented");
        }

        


    }
}
