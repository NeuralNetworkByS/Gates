using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.model
{
    public class TrainingResultP
    {
        public float w1 = 0.00f;
        public float w2 = 0.00f;
        public float biasI = 1.00f;

        public List<String> raport = new List<String>();

        public virtual void initialize()
        {

        }
    }
}
