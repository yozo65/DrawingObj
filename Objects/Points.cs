using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewTry.Objects
{
    class Points : BaseObject
    {
        public Action<Points> OnTimeLeft;
        public float height = 60;
        public float weight = 60;
        public Points(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -30, -30, this.height, this.weight);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-30, -30, 60, 60);
            return path;
        }
       /* public void ChangeSize(Graphics g, float Scale)
        {
            g.ScaleTransform(Scale, Scale);
        }*/
    }
}
