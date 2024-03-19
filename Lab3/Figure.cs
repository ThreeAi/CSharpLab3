using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public enum JoinType
    {
        DrawPolygone,
        DrawClosedCurve,
        FillCurve,
        DrawBeziers
    }
    internal class Figure
    {
        public List<Point> Points { set; get; }
        public List<bool> inversionX { set; get; }
        public List<bool> inversionY { set; get; }
        public Color PointColor { set; get; }
        public Color LineColor { set; get; }
        private JoinType? joinType;
        public JoinType? JoinType
        {
            get { return joinType; }
            set
            {
                if (value == Lab3.JoinType.DrawBeziers && Points.Count() % 3 == 1 && Points.Count() >= 4)
                {
                    joinType = value;
                }
                else if (Points.Count() >= 3 && value != Lab3.JoinType.DrawBeziers)
                {
                    joinType = value;
                }
            }
        }

        public Figure()
        {
            Points = new List<Point>();
            inversionX = new List<bool>();
            inversionY = new List<bool>();
            LineColor = Color.Black;
            PointColor = Color.Black;
            JoinType = null;
        }

        public void AddPoint(Point point)
        {
            Console.WriteLine(joinType);
            if (joinType == Lab3.JoinType.DrawBeziers) return;
            Points.Add(point);
            inversionX.Add(false);
            inversionY.Add(false);

        }

        public bool FindPoint(int x, int y, out int index)
        {
            index = -1;
            for (int i = 0; i < Points.Count; i++)
            {
                Point point = Points[i];
                if (Math.Abs(point.X + 4 - x) <= 6 && Math.Abs(point.Y + 4 - y) <= 6)
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        public void moveUp(int speed, int height, bool down = false)
        {
            List<Point> points = new List<Point>();
            for (int i = 0;i < Points.Count;i++)
            {
                int y;
                if (down ? inversionY[i] : !inversionY[i])
                {
                    y = Points[i].Y - speed;
                    if (y < 0)
                    {
                        y = -y;
                        inversionY[i] = !inversionY[i];
                    }
                }
                else
                {
                    y = Points[i].Y + speed;
                    if (y > height)
                    {
                        y = height - (y - height);
                        inversionY[i] = !inversionY[i];
                    }
                }
                points.Add(new Point(Points[i].X, y));
            }
            Points = points;
        }

        public void moveDown(int speed, int height)
        {
            moveUp(speed, height, true);
        }

        public void moveLeft(int speed, int width, bool left = false)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < Points.Count; i++)
            {
                int x;
                if (left ? inversionX[i] : !inversionX[i])
                {
                    x = Points[i].X - speed;
                    if (x < 0)
                    {
                        x = -x;
                        inversionX[i] = !inversionX[i];
                    }
                }
                else
                {
                    x = Points[i].X + speed;
                    if (x > width)
                    {
                        x = width - (x - width);
                        inversionX[i] = !inversionX[i];
                    }
                }
                points.Add(new Point(x, Points[i].Y));
            }
            Points = points;
        }
        public void moveRight(int speed, int width)
        {
            moveLeft(speed, width, true);
        }
    }
}
