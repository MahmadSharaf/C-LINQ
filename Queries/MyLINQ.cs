using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{   //Extension class
    public static class MyLINQ
    {

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, 
                                                Func<T,bool> predicate) //Func works as the question answer in which receives the question and returns boolean
        {
            //x var result = new List<T>();
            foreach (var item in source)
            {
                
                if (predicate(item)) 
                {
                    yield return item; //! yield returns an IEnumerable type variable to the caller of 
                                       //! the function ,and it continues from the last place it was returned
                    //xresult.Add(item);
                }
            }
            //xreturn result;
        }
    }
}
