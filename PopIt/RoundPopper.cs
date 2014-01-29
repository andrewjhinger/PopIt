using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PopIt
{
    public class RoundPopper : Popper
    {

        public RoundPopper(Rectangle rectangle, Color color)
            : base(rectangle, color)
        {
            // Constructor to enable us to call base constructor
        }


        public override void Draw(Graphics graphics)
        {
            // Draw rectangle
            graphics.DrawRectangle(new Pen(base.PopperColor), base.X, base.Y, base.Width, base.Height);
        }

        public override bool Hit(Point point)
        {
            // Call abstract base class method
            return base.GetRectangle().Contains(point);
        }
    }
}