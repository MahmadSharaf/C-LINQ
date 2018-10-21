using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{//Extension class
    public static class CarExtensions
    {
        public static IEnumerable<Cars> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var column = line.Split(',');
                yield return new Cars
                {
                    Year         = int   .Parse(column[0]),
                    Manufacturer =              column[1],
                    Name         =              column[2],
                    Displacement = double.Parse(column[3]),
                    Cylinders    = int   .Parse(column[4]),
                    City         = int   .Parse(column[5]),
                    Highway      = int   .Parse(column[6]),
                    Combined     = int   .Parse(column[7])
                }; 
            }
        }
    }
}
