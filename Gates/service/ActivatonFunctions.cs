using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.service
{
    public class ActivatonFunctions
    {

        public delegate float Del(float x);
        public float maxError = 0.00f;

        public float jumpActivationFuntion(float x)
        {
            float result = 1.00f;

            if (x < 0)
            {
                result = 0.00f;
            }

            return result;
        }

        public float sigmoidActivationFunction(float x)
        {
            float result = 1.00f / (1.00f + ((float)Math.Pow(Math.E, -x)));

            return result;
        }

        public float correctionForSigmoid(float result)
        {
            float errorUp = 1.00f - result;

            if (result <= maxError)
            {
                result = 0.00f;
            }
            else if (errorUp <= maxError)
            {
                result = 1.00f;
            }

            return result;
        }
    }
}
