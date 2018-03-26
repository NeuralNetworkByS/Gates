using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gates.model
{
    public class PointInfo
    {
        public PointInfo(float x, float y, float color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public float x;
        public float y;
        public float color;

        public override string ToString()
        {
            return "x: " + x.ToString() + ", y: " + y.ToString() + ", color: " + color.ToString();
        }
    }
}
