using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public static class ManufacturerExtension
    {
        public static IEnumerable<Manufacturer> ToManufacturer(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var column = line.Split(',');
                yield return new Manufacturer
                {
                    Name         =                column [ 0 ] , 
                    Headquarters =                column [ 1 ] , 
                    Year         = int . Parse  ( column [ 2 ] )  
                };
            }
        }
    }
}
