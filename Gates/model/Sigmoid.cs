using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.model
{
    class Sigmoid
    {
        public static float output(float x)
        {
            return  1.00f / (1.00f + ((float) Math.Exp(-x)));
        }

        public static float derivative(float x)
        {
            return x * (1 - x);
        }
    }
}
