using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.config
{
    public class GateTrainingValuesContainer
    {
        public List<float> x1Values = new List<float>();
        public List<float> x2Values = new List<float>();

        public Dictionary<String, List<float>> results = new Dictionary<string, List<float>>();

        public int ColumnCount = 3;
        public int RowCount = 5;

        public GateTrainingValuesContainer()
        {
            configureModels();
        }

        private void configureModels()
        {
            configureInputValuesModel();

            configureOrResult();
            configureANDResult();
            configureXORResult();
        }

        private void configureInputValuesModel()
        {
      
            x1Values.Add(1.00f);
            x1Values.Add(0.00f);
            x1Values.Add(1.00f);
            x1Values.Add(0.00f);

            x2Values.Add(1.00f);
            x2Values.Add(1.00f);
            x2Values.Add(0.00f);
            x2Values.Add(0.00f);

        }

        public void configureOrResult()
        {

            List<float> result = new List<float>();

            result.Add(1.00f);
            result.Add(1.00f);
            result.Add(1.00f);
            result.Add(0.00f);

            results.Add("OR", result);
        }

        public void configureANDResult()
        {
            List<float> result = new List<float>();

            result.Add(1.00f);
            result.Add(0.00f);
            result.Add(0.00f);
            result.Add(0.00f);

            results.Add("AND", result);

            
        }

        public void configureXORResult()
        {
            List<float> result = new List<float>();

            result.Add(0.00f);
            result.Add(1.00f);
            result.Add(1.00f);
            result.Add(0.00f);

            results.Add("XOR", result);
        }
    }

    
}
