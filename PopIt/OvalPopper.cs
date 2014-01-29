using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PopIt
{
    public sealed class OvalPopper : RoundPopper
    {
        public OvalPopper(Rectangle rectangle, Color color)
            : base(rectangle, color)
        {
            // Constructor to enable us to call base constructor
        }

        public override void Draw(Graphics graphics)
        {
            // Draw rectangle
            //graphics.DrawRectangle(new Pen(base.PopperColor), base.X, base.Y, base.Width, base.Height);
            graphics.DrawEllipse(new Pen(base.PopperColor), new Rectangle(base.X, base.Y, base.Height /2, base.Height)); 
        }

        public override bool Hit(Point point)
        {
            // Call abstract base class method
            return base.GetRectangle().Contains(point);
        }
    }
}