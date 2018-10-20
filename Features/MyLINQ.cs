using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    // Extension Method: has to be static 
    public static class MyLINQ
    {
        //This method not an extension method without 'this'. 
        //'this' makes this method be invoked directly through MyLINQ.Count() or anyplace where IEnumerable variable is used
        public static int Count<T>(this IEnumerable<T> sequence )
        {
            int count = 0;
            foreach (var item in sequence)
            {                
                count += 1;
            }
            return count;
        }
    }
}
