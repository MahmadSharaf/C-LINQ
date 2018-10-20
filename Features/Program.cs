using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //! List and array collections can implement interface of IEnumerable 
            //x Employee[] developers = new Employee[]
            IEnumerable < Employee > developers = new Employee[]
            {
                new Employee{ Id = 1, Name= "Scott"},
                new Employee{ Id = 2, Name= "Chris"}
            };

            //x List<Employee> sales = new List<Employee>()
            IEnumerable < Employee > sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex"}
            };

            IEnumerator<Employee> enumerator = sales.GetEnumerator();

            // This uses extension method implemented in features namespace
            Console.WriteLine(developers.Count());
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
        }
    }
}

//? IEnumerable:
//! The beautiful thing about IEnumerable is that  you can hide just about any sort of data structure or operation  behind this interface.
//! Every time MoveNext is called, it is not know if the underlying enumerator just has to access a new element in an array or index the
//! item of a list or follow the pointer in the next item of a list or follow the pointer in a tree or even make a call back to the database
//! server to fetch the next record out of the table, So IEnumerable is the perfect interface for hiding the source of data, and that is why
//! 98% of LINQ features that can do things like ordering, filtering, counting, aggregating, they all can work against IEnumerable of T.

//? LINQ:
//! LINQ works by using extension methods, which we will w

//? Extesnion Methods:
//! allow implementing functionality against interface type definition without changing that underlying type,  