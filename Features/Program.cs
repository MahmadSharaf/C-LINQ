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

            

            // This uses extension method implemented in features namespace
            Console.WriteLine(developers.Count());

            // using GetEnumerator to iterate through IEnumerable variable
            IEnumerator<Employee> enumerator = sales.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }

            // Using lambda expression
            foreach (var employee in developers.Where( e => e.Name.StartsWith("S")))
            {
                Console.WriteLine("Start with 'S': " + employee.Name);
            }

            //Queries
            Console.WriteLine("****Queries****");
            // Using method syntax
            var query1 = developers.Where(e => e.Name.Length == 5).OrderBy(e => e.Name);
            // Using SQL-like sytax
            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;

            foreach (var employee in query1)
            {
                Console.WriteLine(employee.Name);

            }
            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);

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

//? var:
//! The complier inferes the type of the variable.
//  It can be used anywhere except:
//! *Defining parameters to a method.
//! *Defining a field of a class.
//! *Unintialized variables
