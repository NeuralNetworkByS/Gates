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

        public XorTrResult trainXOR(TrainingSetings settings, GateTrainingValuesContainer valuesContainer)
        {
            this.trainingSetings = settings;
            this.valuesContainer = valuesContainer;
            activationFunctions.maxError = trainingSetings.maxError;
            firstLineBuilder = new StringBuilder();
            firstLineBuilder.Append("Trenowanie: ");
            firstLineBuilder.Append(" bramka: " + trainingSetings.gateType.ToString() + ", ");


            XorTrResult xorTrResult = trainByTwoNeuronsBySigmoid();

            return xorTrResult;
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
            //trainByTwoNeuronsByJump(activationFunctions.jumpActivationFuntion);
        }

        private void trainBySigmoidFunctionAndOneNeuron()
        {
            trainByOneNeuron(activationFunctions.sigmoidActivationFunction);
        }

        private void trainBySigmoidFunctionAndTwoNeuron()
        {
            trainByTwoNeuronsBySigmoid();
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

        private XorTrResult trainByTwoNeuronsBySigmoid()
        {
            Debug.WriteLine("Działa");
            float[,] inputs =
            {
                { 0, 0},
                { 0, 1},
                { 1, 0},
                { 1, 1}
            };

            float[] results = { 0, 1, 1, 0 };

            Neuron hiddenNeuron1 = new Neuron();
            Neuron hiddenNeuron2 = new Neuron();
            Neuron outputNeuron = new Neuron();
            List<String> raport = new List<String>();

            hiddenNeuron1.randomizeWeights();
            raport.Add("Inicjalizacja: ");
            raport.Add("hiddenNeuron1.weights[0]: " + hiddenNeuron1.weights[0]);
            raport.Add("hiddenNeuron1.weights[1]: " + hiddenNeuron1.weights[1]);
            hiddenNeuron2.randomizeWeights();
            raport.Add("hiddenNeuron2.weights[0]: " + hiddenNeuron2.weights[0]);
            raport.Add("hiddenNeuron2.weights[1]: " + hiddenNeuron2.weights[1]);
            outputNeuron.randomizeWeights();
            raport.Add("outputNeuron.weights[0]: " + outputNeuron.weights[0]);
            raport.Add("outputNeuron.weights[1]: " + outputNeuron.weights[1]);

            bool isChanged = true;

            int epochs = 0;
            while (isChanged)
            {
                isChanged = false;

                for (int i = 0; i < 4; i++)
                {
                    // 1) forward propagation (calculates output)
                    hiddenNeuron1.inputs = new float[] { inputs[i, 0], inputs[i, 1] };
                    hiddenNeuron2.inputs = new float[] { inputs[i, 0], inputs[i, 1] };

                    outputNeuron.inputs = new float[] { hiddenNeuron1.output(), hiddenNeuron2.output() };
                    float result = outputNeuron.output();
                    Debug.WriteLine("{0} xor {1} = {2}", inputs[i, 0], inputs[i, 1], result);

                    // 2) back propagation (adjusts weights)

                    // adjusts the weight of the output neuron, based on its error

                    result = activationFunctions.correctionForSigmoid(result);
                    Debug.WriteLine("output po korekcji: " + result);

                    if (result != results[i])
                    {
                        isChanged = true;

                        outputNeuron.error = Sigmoid.derivative(outputNeuron.output()) * (results[i] - outputNeuron.output());
                        outputNeuron.adjustWeights(trainingSetings.learningRate);

                        // then adjusts the hidden neurons' weights, based on their errors
                        hiddenNeuron1.error = Sigmoid.derivative(hiddenNeuron1.output()) * outputNeuron.error * outputNeuron.weights[0];
                        hiddenNeuron2.error = Sigmoid.derivative(hiddenNeuron2.output()) * outputNeuron.error * outputNeuron.weights[1];

                        hiddenNeuron1.adjustWeights(trainingSetings.learningRate);
                        hiddenNeuron2.adjustWeights(trainingSetings.learningRate);

                        raport.Add("Zmiana wag: ");
                        raport.Add("hiddenNeuron1.weights[0]: " + hiddenNeuron1.weights[0]);
                        raport.Add("hiddenNeuron1.weights[1]: " + hiddenNeuron1.weights[1]);
                        raport.Add("hiddenNeuron2.weights[0]: " + hiddenNeuron2.weights[0]);
                        raport.Add("hiddenNeuron2.weights[1]: " + hiddenNeuron2.weights[1]);
                        raport.Add("outputNeuron.weights[0]: " + outputNeuron.weights[0]);
                        raport.Add("outputNeuron.weights[1]: " + outputNeuron.weights[1]);
                    }
                }
                epochs++;
            }

            raport.Add("Ilość epochs: " + epochs);
            return new XorTrResult(hiddenNeuron1, hiddenNeuron2, outputNeuron, raport);
        }
    }
}
