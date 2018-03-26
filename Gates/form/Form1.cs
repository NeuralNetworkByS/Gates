using Gates.config;
using Gates.model;
using Gates.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gates
{
    public partial class Form1 : Form
    {

        private GateTrainingValuesContainer gateTrValuesContainer = new GateTrainingValuesContainer();
        private List<float[]> verificationData = new List<float[]>();
        private TrainingService trainingService = new TrainingService();

        private TrainingSetings trainingSetings = new TrainingSetings();
        private TrainingResult trainingResult;
        private ChartVisualization chartVisualization = new ChartVisualization();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            setUpGateTrainingValuesView(ref gateTrValuesContainer.x1Values, ref gateTrValuesContainer.x2Values, gateTrValuesContainer.results["OR"]);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void initializeTrainingSettings()
        {
            trainingSetings.gateType = TrainingSetings.GateType.OR;
            trainingSetings.activiationFunction = TrainingSetings.ActiviationFunction.JUMP;
            trainingSetings.neuronsNumbers = TrainingSetings.NeuronsNumbers.ONE;
        }


        private void setUpGateTrainingValuesView(ref List<float> x1Values, ref List<float> x2Values, List<float> result)
        {
            GateTrainingValuesView.Rows.Clear();
            GateTrainingValuesView.RowCount = gateTrValuesContainer.RowCount;
            GateTrainingValuesView.ColumnCount = gateTrValuesContainer.ColumnCount;

            List<String[]> rows = new List<String[]>();
            String[] titleRow = { "x1", "x2", "d" };

            GateTrainingValuesView.Rows.Clear();

            GateTrainingValuesView.Rows.Add(titleRow);
           

            for (int i = 0; i < gateTrValuesContainer.RowCount - 1; i++)
            {
                String[] row = {
                    x1Values[i].ToString(),
                    x2Values[i].ToString(),
                    result[i].ToString()
                };

                GateTrainingValuesView.Rows.Add(row);
            }
        }

        private void initVerificationData()
        {
            float[] values1 = { 0.05f, 0.05f };
            float[] values2 = { 0.09f, 0.05f };
            float[] values3 = { 0.05f, 0.95f };
            float[] values4 = { 0.95f, 0.95f };

            verificationData.Add(values1);
            verificationData.Add(values2);
            verificationData.Add(values3);
            verificationData.Add(values4);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ORRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ORRB.Checked)
            {
                DataTraningView.Text = "OR";
                trainingSetings.gateType = TrainingSetings.GateType.OR;
                String gateType = (string)trainingSetings.gateType.ToString();
                setUpGateTrainingValuesView(ref gateTrValuesContainer.x1Values, ref gateTrValuesContainer.x2Values,
                     gateTrValuesContainer.results[gateType]);
                
            }
        }

        private void ANDRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ANDRB.Checked)
            {
                DataTraningView.Text = "AND";
                trainingSetings.gateType = TrainingSetings.GateType.AND;
                String gateType = (string)trainingSetings.gateType.ToString();
                setUpGateTrainingValuesView(ref gateTrValuesContainer.x1Values, ref gateTrValuesContainer.x2Values, 
                     gateTrValuesContainer.results[gateType]);
                
            }
        }

        private void XORRB_CheckedChanged(object sender, EventArgs e)
        {
            if (XORRB.Checked)
            {
                DataTraningView.Text = "XOR";
                trainingSetings.gateType = TrainingSetings.GateType.XOR;
                String gateType = (string)trainingSetings.gateType.ToString();
                setUpGateTrainingValuesView(ref gateTrValuesContainer.x1Values, ref gateTrValuesContainer.x2Values,
                     gateTrValuesContainer.results[gateType]);
                
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
           
        }

        private void DataTraningView_Enter(object sender, EventArgs e)
        {

        }

        private void JumpFunctionRB_CheckedChanged(object sender, EventArgs e)
        {
            trainingSetings.activiationFunction = TrainingSetings.ActiviationFunction.JUMP;
        }

        private void SigmoidFunctionRB_CheckedChanged(object sender, EventArgs e)
        {
            trainingSetings.activiationFunction = TrainingSetings.ActiviationFunction.SIGMOID;
        }

        private void OneNeuronRB_CheckedChanged(object sender, EventArgs e)
        {
            trainingSetings.neuronsNumbers = TrainingSetings.NeuronsNumbers.ONE;
        }

        private void TwoNeuronsTB_CheckedChanged(object sender, EventArgs e)
        {
            trainingSetings.neuronsNumbers = TrainingSetings.NeuronsNumbers.TWO;
        }

        private void TrainingStartButton_Click(object sender, EventArgs e)
        {
            try
            {
                trainingSetings.learningRate = float.Parse(LearningRateTB.Text, CultureInfo.InvariantCulture);
                trainingResult = trainingService.train(trainingSetings, gateTrValuesContainer);
                showTrainingRaport(trainingResult.raport);
            }
            catch (NotImplementedException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (FormatException exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        public void showTrainingRaport(List<String> raport)
        {
            TrainingLogLB.Items.Clear();

            for (int i = 0; i < raport.Count; i++)
            {
                TrainingLogLB.Items.Add(raport[i]);
            }
        }

        private void ChartButton_Click(object sender, EventArgs e)
        {
            chartVisualization.showChart(trainingResult, trainingSetings);
        }
    }
}
