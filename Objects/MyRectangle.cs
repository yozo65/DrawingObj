using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTry.Objects
{
    class MyRectangle : BaseObject
    {
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Blue), -25, -15, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), -25, -15, 50, 30);
        }
    }
}
