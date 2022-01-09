using NewTry.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewTry
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        Points point1;
        Points point2;

        public Form1()
        { 
            InitializeComponent();

            var rnd = new Random();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            marker = new Marker(pbMain.Width / 2 + 1, pbMain.Height / 2 + 1, 0);
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            point1 = new Points(rnd.Next(pbMain.Width - 200), rnd.Next(pbMain.Height - 50), 0);
            point2 = new Points(rnd.Next(pbMain.Width - 200), rnd.Next(pbMain.Height - 50), 0);
            
            player.OnPointOverlap += (p) =>
            { 
                var i = int.Parse(txtTab.Text);
                i++;
                txtTab.Text = Convert.ToString(i);
                objects.Remove(p);

                if (p == point1)
                {
                    point1 = new Points(rnd.Next(pbMain.Width - 200), rnd.Next(pbMain.Height - 50), 0);
                    objects.Add(point1);
                }
                if (p == point2)
                {
                    point2 = new Points(rnd.Next(pbMain.Width - 200), rnd.Next(pbMain.Height - 50), 0);
                    objects.Add(point2);
                }
            };

            objects.Add(marker);
            objects.Add(player);
            objects.Add(point1);
            objects.Add(point2);
        }

        private void pbmain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            foreach (var obj in objects)
            { 
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var rnd = new Random();
            updatePlayer();

            if (point1.height <= 15)
            {
                objects.Remove(point1);
                point1 = new Points(rnd.Next(pbMain.Width - 200), rnd.Next(pbMain.Height - 50), 0);
                objects.Add(point1);
            }
            else
            {
                Smoll(point1);
            }
            if (point2.height <= 15)
            {
                objects.Remove(point2);
                point2 = new Points(rnd.Next(pbMain.Width - 200), rnd.Next(pbMain.Height - 50), 0);
                objects.Add(point2);
            }
            else
            {
                Smoll(point2);
            }

            pbMain.Invalidate();
        }

        private void Smoll(Points points)
        {
            points.height *= 0.99f;
            points.weight *= 0.99f;
        }
        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }
    }
}

