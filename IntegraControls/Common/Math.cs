using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IntegraControls.Common
{
    public static class Math
    {
        public static double Distance(Point a, Point b)
        {
            double dX = a.X - b.X;
            double dY = a.Y - b.Y;

            return dX * dX + dY * dY;
        }
    }

    
}
