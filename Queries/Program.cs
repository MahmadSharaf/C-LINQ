using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>
            {
                new Movie { Title="The Dark Knight", Rating=8.2f, Year=2008},
                new Movie { Title="The King's Speech", Rating=8.0f, Year=2010},
                new Movie { Title="Casablanca", Rating=8.5f, Year=1942},
                new Movie { Title="Star Wars V", Rating=8.7f, Year=1980},

            };

            // Where operator just like Filter method in MyLINQ; it depends on lazy operation(deferred execution) which works only we it has to work 
            // while Count operator is not deferred execution, otherwise it forces the code to execute fully.
            // So in this scenario, it will loops twice, one fully and one lazily. So to solve this, ToList() operator has to be used as 
            // will save the result in a list which multiple operators can be done the query without looping more than once. But this will
            // lose the deferred execution advantages. 
            // One more disadvantage of deferred execution is throwing exceptions. It skips throwing the exception and it skips the current process. 
            //x var query = movies.Where(m => m.Year > 2000);
            var query = movies.Where(m => m.Year > 2000).ToList();
            Console.WriteLine(query.Count());            
            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }
            

        }
    }
}

//? Deferred Execution:
//! It is called lazy execution because it executes only when its execution is needed and starts from the last place has been executed.
//! Advantages: Saves time and memory for unneeded data executions.
//! Disadvantages: Does not throw exceptions, the loop can not be used by other operators as the loop execution is unpredicted.
//! It can be categorized into two categories:
//! Streaming operators "Where": It only need to read through the source data up until the point the operator produces a result.
//! Non-streaming operators "OrderBy" : It has to run through all the data to figure out what should be the result.