using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PopIt
{
    public abstract class Popper
    {
        public Color PopperColor;
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Popper(Rectangle rectangle, Color color)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
            PopperColor = color;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }



        public abstract void Draw(Graphics graphics);

        public abstract bool Hit(Point point);
    }
}