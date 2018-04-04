using Gates.config;
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
        private TrainingResultP tResult;
        private TrainingResultMP tResultMP;

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

           
            return tResult;
        }

        public TrainingResultMP trainXOR(TrainingSetings settings, GateTrainingValuesContainer valuesContainer)
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


            return tResultMP;
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
            trainByTwoNeuronsByJump(activationFunctions.jumpActivationFuntion);
        }

        private void trainBySigmoidFunctionAndOneNeuron()
        {
            trainByOneNeuron(activationFunctions.sigmoidActivationFunction);
        }

        private void trainBySigmoidFunctionAndTwoNeuron()
        {
            trainByTwoNeuronsBySigmoid(activationFunctions.sigmoidActivationFunction);
        }

        private void trainByOneNeuron(ActivatonFunctions.Del activationFunction)
        {
            if (trainingSetings.gateType == TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Brak możliwości trenowanie XOR jednym nuronem");
            }

            tResult = new TrainingResultP();
            firstLineBuilder.Append(" pojedynczy neuron.");

            tResult.raport.Add(firstLineBuilder.ToString());

            bool isWeightChanged = true;
            Random random = new Random();

            float w1 = (float) random.NextDouble();
            float w2 = (float)random.NextDouble();
            float bias = 1.00f;
            float e = 0.00f;
            float y = 0.00f;

            tResult.raport.Add("Początkowe w1: " + w1);
            tResult.raport.Add("Początkowe w2: " + w2);
            tResult.raport.Add("Początkowy bias: " + bias);


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
                    y = activationFunctions.correctionForSigmoid(y);

                    if (y != desireResults[i])
                    {
                        tResult.raport.Add("");
                        tResult.raport.Add("desire = " + desireResults[i] + ", y = " + y + ", e: " + e);
                        tResult.raport.Add("Zmiana w1, w2, bias");
                        
                        w1 = w1 + trainingSetings.learningRate * (desireResults[i] - y) * valuesContainer.x1Values[i];
                        w2 = w2 + trainingSetings.learningRate * (desireResults[i] - y) * valuesContainer.x2Values[i];
                        bias = bias + trainingSetings.learningRate * (desireResults[i] - y);

                        tResult.raport.Add("w1: " + w1);
                        tResult.raport.Add("w2: " + w2);
                        tResult.raport.Add("bias: " + bias);

                        isWeightChanged = true;
                    }
                }

            }

            tResult.w1 = w1;
            tResult.w2 = w2;
            tResult.biasI = bias;

            tResult.raport.Add("Finalne w1: " + w1);
            tResult.raport.Add("Finalene w2: " + w2);
            tResult.raport.Add("Finalne bias: " + bias);
            tResult.raport.Add("Liczba epok: " + epochs);
        }

        private void trainByTwoNeuronsBySigmoid(ActivatonFunctions.Del activationFuncation)
        {
            firstLineBuilder.Append(" dwa neurony.");
            //throw new NotImplementedException("Not implemented");

            if (trainingSetings.gateType != TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Brak możliwość trenowania z wykorzystaniem 2 neuronów tylko bramki xor");
            }

            

            tResultMP = new TrainingResultMP();
            tResultMP.initialize();

            firstLineBuilder.Append(" sieć.");
            tResultMP.raport.Add(firstLineBuilder.ToString());

            tResultMP.raport.Add("Początkowe w1: " + tResultMP.w1);
            tResultMP.raport.Add("Początkowe w2: " + tResultMP.w2);
            tResultMP.raport.Add("Początkowe w3: " + tResultMP.w3);
            tResultMP.raport.Add("Początkowe w4: " + tResultMP.w4);

            tResultMP.raport.Add("Początkowe wh1: " + tResultMP.wh1);
            tResultMP.raport.Add("Początkowe w4: " + tResultMP.wh2);

            tResultMP.raport.Add("Początkowy biasI: " + tResultMP.biasI);
            tResultMP.raport.Add("Początkowy biasI2: " + tResultMP.biasI2);
            tResultMP.raport.Add("Początkowy biasO: " + tResultMP.biasO);



            bool isChanged = true;

            float gradientOutput = 0.00f;
            float gradientH1 = 0.00f;
            float gradientH2 = 0.00f;
            float output = 0.00f;

            int epochs = 0;

            while (isChanged)
            {
                isChanged = false;

                for (int i = 0; i < valuesContainer.results.Count; i++)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine("x1: {0}, x2: {1}, d: {2}", valuesContainer.x1Values[i], valuesContainer.x2Values[i], valuesContainer.results[trainingSetings.gateType.ToString()][i]);

                    tResultMP.h1 =
                    activationFuncation(tResultMP.w1 * valuesContainer.x1Values[i] + tResultMP.w3 * valuesContainer.x2Values[i] + tResultMP.biasI);

                    tResultMP.h2 =
                    activationFuncation(tResultMP.w2 * valuesContainer.x1Values[i] + tResultMP.w4 * valuesContainer.x2Values[i] + tResultMP.biasI2);

                    output = tResultMP.h1 * tResultMP.wh1 + tResultMP.h2 * tResultMP.wh2 + tResultMP.biasO;
                    Debug.WriteLine("Output przed aktywacją: " + output);
                    output = activationFuncation(output);
                    output = activationFunctions.correctionForSigmoid(output);
                    Debug.WriteLine("Output po aktywacji: " + output);

                    Debug.WriteLine("Oczekiwana wartość: " + valuesContainer.results[trainingSetings.gateType.ToString()][i]);


                    if (output != valuesContainer.results[trainingSetings.gateType.ToString()][i])
                    {
                        tResultMP.raport.Add("");
                        tResultMP.raport.Add("Zmiana wag: " + tResultMP.w1);
                        tResultMP.raport.Add("output: " + output + ", desired: " + valuesContainer.results[trainingSetings.gateType.ToString()][i] + ".");
                        Debug.WriteLine("Zmiana wag itp");
                        isChanged = true;

                        gradientOutput = (valuesContainer.results[trainingSetings.gateType.ToString()][i] - output) * output * (1 - output);
                        Debug.WriteLine("gradientOtput: " + gradientOutput);
                        gradientH1 = gradientOutput * tResultMP.wh1 * tResultMP.h1 * (1 - tResultMP.h1);
                        gradientH2 = gradientOutput * tResultMP.wh2 * tResultMP.h2 * (1 - tResultMP.h2);

                        tResultMP.biasO += trainingSetings.learningRate * gradientOutput;
                        Debug.WriteLine("trainingSetings.learningRate * gradientOutput: " + trainingSetings.learningRate * gradientOutput);
                        tResultMP.wh1 += trainingSetings.learningRate * gradientOutput * tResultMP.h1;
                        tResultMP.wh2 += trainingSetings.learningRate * gradientOutput * tResultMP.h2;

                        tResultMP.biasI += trainingSetings.learningRate * gradientH1;
                        tResultMP.w1 += trainingSetings.learningRate * gradientH1 * valuesContainer.x1Values[i];
                        tResultMP.w3 += trainingSetings.learningRate * gradientH1 * valuesContainer.x1Values[i];

                        tResultMP.biasI2 += trainingSetings.learningRate * gradientH2;
                        tResultMP.w2 += trainingSetings.learningRate * gradientH2 * valuesContainer.x2Values[i];
                        tResultMP.w4 += trainingSetings.learningRate * gradientH2 * valuesContainer.x2Values[i];

                        Debug.WriteLine("biasO: " + tResultMP.biasO);
                        Debug.WriteLine("wh1: " + tResultMP.wh1);
                        Debug.WriteLine("wh2: " + tResultMP.wh2);

                        Debug.WriteLine("biasI: " + tResultMP.biasI);
                        Debug.WriteLine("w1: " + tResultMP.w1);
                        Debug.WriteLine("w2: " + tResultMP.w2);

                        Debug.WriteLine("biasI2: " + tResultMP.biasI2);
                        Debug.WriteLine("w2: " + tResultMP.w2);
                        Debug.WriteLine("w4: " + tResultMP.w4);

                        tResultMP.raport.Add("w1: " + tResultMP.w1);
                        tResultMP.raport.Add("w2: " + tResultMP.w2);
                        tResultMP.raport.Add("w3: " + tResultMP.w3);
                        tResultMP.raport.Add("w4: " + tResultMP.w4);

                        tResultMP.raport.Add("wh1: " + tResultMP.wh1);
                        tResultMP.raport.Add("wh2: " + tResultMP.wh2);

                        tResultMP.raport.Add("biasI: " + tResultMP.biasI);
                        tResultMP.raport.Add("biasI2: " + tResultMP.biasI2);
                        tResultMP.raport.Add("biasO: " + tResultMP.biasO);


                    }

                }

                epochs++;
            }

            tResultMP.raport.Add("Epochs: " + epochs + ".");
            Debug.WriteLine("Trening Zakończony");

        }


        private void trainByTwoNeuronsByJump(ActivatonFunctions.Del activationFuncation)
        {
            firstLineBuilder.Append(" dwa neurony.");
            //throw new NotImplementedException("Not implemented");

            if (trainingSetings.gateType != TrainingSetings.GateType.XOR)
            {
                throw new NotImplementedException("Brak możliwość trenowania z wykorzystaniem 2 neuronów tylko bramki xor");
            }



            tResultMP = new TrainingResultMP();
            tResultMP.initialize();

            firstLineBuilder.Append(" sieć.");
            tResultMP.raport.Add(firstLineBuilder.ToString());

            tResultMP.raport.Add("Początkowe w1: " + tResultMP.w1);
            tResultMP.raport.Add("Początkowe w2: " + tResultMP.w2);
            tResultMP.raport.Add("Początkowe w3: " + tResultMP.w3);
            tResultMP.raport.Add("Początkowe w4: " + tResultMP.w4);

            tResultMP.raport.Add("Początkowe wh1: " + tResultMP.wh1);
            tResultMP.raport.Add("Początkowe w4: " + tResultMP.wh2);

            tResultMP.raport.Add("Początkowy biasI: " + tResultMP.biasI);
            tResultMP.raport.Add("Początkowy biasI2: " + tResultMP.biasI2);
            tResultMP.raport.Add("Początkowy biasO: " + tResultMP.biasO);



            bool isChanged = true;

            float gradientOutput = 0.00f;
            float gradientH1 = 0.00f;
            float gradientH2 = 0.00f;
            float output = 0.00f;

            int epochs = 0;

            while (isChanged)
            {
                isChanged = false;

                for (int i = 0; i < valuesContainer.results.Count; i++)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine("x1: {0}, x2: {1}, d: {2}", valuesContainer.x1Values[i], valuesContainer.x2Values[i], valuesContainer.results[trainingSetings.gateType.ToString()][i]);

                    tResultMP.h1 =
                    activationFuncation(tResultMP.w1 * valuesContainer.x1Values[i] + tResultMP.w3 * valuesContainer.x2Values[i] + tResultMP.biasI);

                    tResultMP.h2 =
                    activationFuncation(tResultMP.w2 * valuesContainer.x1Values[i] + tResultMP.w4 * valuesContainer.x2Values[i] + tResultMP.biasI2);

                    output = tResultMP.h1 * tResultMP.wh1 + tResultMP.h2 * tResultMP.wh2 + tResultMP.biasO;
                    Debug.WriteLine("Output przed aktywacją: " + output);
                    output = activationFuncation(output);
                    Debug.WriteLine("Output po aktywacji: " + output);

                    Debug.WriteLine("Oczekiwana wartość: " + valuesContainer.results[trainingSetings.gateType.ToString()][i]);


                    if (output != valuesContainer.results[trainingSetings.gateType.ToString()][i])
                    {
                        tResultMP.raport.Add("");
                        tResultMP.raport.Add("Zmiana wag: " + tResultMP.w1);
                        tResultMP.raport.Add("output: " + output + ", desired: " + valuesContainer.results[trainingSetings.gateType.ToString()][i] + ".");
                        Debug.WriteLine("Zmiana wag itp");
                        isChanged = true;

                        gradientOutput = (valuesContainer.results[trainingSetings.gateType.ToString()][i] - output);
                        Debug.WriteLine("gradientOtput: " + gradientOutput);
                        gradientH1 = gradientOutput * tResultMP.wh1;
                        gradientH2 = gradientOutput * tResultMP.wh2;

                        tResultMP.biasO += trainingSetings.learningRate * gradientOutput;
                        Debug.WriteLine("trainingSetings.learningRate * gradientOutput: " + trainingSetings.learningRate * gradientOutput);
                        tResultMP.wh1 += trainingSetings.learningRate * gradientOutput * tResultMP.h1;
                        tResultMP.wh2 += trainingSetings.learningRate * gradientOutput * tResultMP.h2;

                        tResultMP.biasI += trainingSetings.learningRate * gradientH1;
                        tResultMP.w1 += trainingSetings.learningRate * gradientH1 * valuesContainer.x1Values[i];
                        tResultMP.w3 += trainingSetings.learningRate * gradientH1 * valuesContainer.x1Values[i];

                        tResultMP.biasI2 += trainingSetings.learningRate * gradientH2;
                        tResultMP.w2 += trainingSetings.learningRate * gradientH2 * valuesContainer.x2Values[i];
                        tResultMP.w4 += trainingSetings.learningRate * gradientH2 * valuesContainer.x2Values[i];

                        Debug.WriteLine("biasO: " + tResultMP.biasO);
                        Debug.WriteLine("wh1: " + tResultMP.wh1);
                        Debug.WriteLine("wh2: " + tResultMP.wh2);

                        Debug.WriteLine("biasI: " + tResultMP.biasI);
                        Debug.WriteLine("w1: " + tResultMP.w1);
                        Debug.WriteLine("w2: " + tResultMP.w2);

                        Debug.WriteLine("biasI2: " + tResultMP.biasI2);
                        Debug.WriteLine("w2: " + tResultMP.w2);
                        Debug.WriteLine("w4: " + tResultMP.w4);

                        tResultMP.raport.Add("w1: " + tResultMP.w1);
                        tResultMP.raport.Add("w2: " + tResultMP.w2);
                        tResultMP.raport.Add("w3: " + tResultMP.w3);
                        tResultMP.raport.Add("w4: " + tResultMP.w4);

                        tResultMP.raport.Add("wh1: " + tResultMP.wh1);
                        tResultMP.raport.Add("wh2: " + tResultMP.wh2);

                        tResultMP.raport.Add("biasI: " + tResultMP.biasI);
                        tResultMP.raport.Add("biasI2: " + tResultMP.biasI2);
                        tResultMP.raport.Add("biasO: " + tResultMP.biasO);


                    }

                }

                epochs++;
            }

            tResultMP.raport.Add("Epochs: " + epochs + ".");
            Debug.WriteLine("Trening Zakończony");

        }

    }
}
