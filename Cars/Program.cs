using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars          = ProcessCars         ( "fuel.csv"          ) ; 
            var manufacturers = ProcessManufactures ( "manufacturers.csv" ) ;

            // AGGREGATING

            var querySyntax_aggregating = from car2 in cars
                                          group car2 by car2.Manufacturer into carGroup
                                          select new
                                          {
                                              Name = carGroup.Key,
                                              Max = carGroup.Max(c => c.Combined),
                                              Min = carGroup.Min(c => c.Combined),
                                              Avg = carGroup.Average(c => c.Combined)
                                          } into agg
                                          orderby agg.Max descending
                                          select agg;

            var extensionSyntax_aggregating = cars.GroupBy(c => c.Manufacturer).Select(g =>
                                              {
                                                  var results = g.Aggregate(new CarStatistics(),
                                                                              (acc, c) => acc.Accumlate(c),
                                                                              acc => acc.Compute());
                                                  return new
                                                  {
                                                      Name = g.Key,
                                                      Avg = results.Avg,
                                                      Min = results.Min,
                                                      Max = results.Max
                                                  };
                                              })
                                              .OrderByDescending(r => r.Max);


            foreach (var item in extensionSyntax_aggregating)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine($"\t Max: {item.Max}");
                Console.WriteLine($"\t Min: {item.Min}");
                Console.WriteLine($"\t Avg: {item.Avg}");
            }

            // GROUPJOIN

            var querySyntax_groupjoin = from manufacturer in manufacturers
                                        join car in cars on manufacturer.Name equals car.Manufacturer into carGroup
                                        orderby manufacturer.Name
                                        select new
                                        {
                                            Manufacturer = manufacturer , 
                                            Cars         = carGroup       
                                        } into results
                                        group results by results.Manufacturer.Headquarters;

            var extensionSyntax_groupjoin = manufacturers.GroupJoin(cars,
                                                                    m => m.Name,
                                                                    c => c.Manufacturer,
                                                                    (m, c) => new
                                                                    { Manufacturer = m, Cars = c })
                                                         //x .OrderBy(m => m.Manufacturer.Name);
                                                         .GroupBy(m => m.Manufacturer.Headquarters);

            //foreach (var group in extensionSyntax_groupjoin)
            //{
            //    Console.WriteLine($"{group.Key}");
            //    foreach (var car in group.SelectMany(g => g.Cars).OrderByDescending(c => c.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t{ car.Name} : { car.Combined}");
            //    }
            //}
            //x foreach (var group in extensionSyntax_groupjoin)
            //x {
            //x     Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
            //x     foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
            //x     {
            //x         Console.WriteLine($"\t{ car.Name} : { car.Combined}");
            //x     }
            //x }

            // GROUP
            var querySyntax_group = from car in cars
                                    group car by car.Manufacturer.ToUpper() into x
                                    orderby x.Key
                                    select x;

            var extensionSyntax_group = cars.GroupBy(c => c.Manufacturer.ToUpper()).OrderBy(x => x.Key);

            //foreach (var group in extensionSyntax_group)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{ car.Name} : { car.Combined}");
            //    }
            //}

            // JOIN
            var querySyntax_join = from car in cars
                              join manufacturer in manufacturers 
                              on new { car.Manufacturer, car.Year} 
                              equals new { Manufacturer = manufacturer.Name,manufacturer.Year}
                              orderby car.Combined descending, car.Name ascending
                              select new
                              {
                                  manufacturer.Headquarters,
                                  car.Name,
                                  car.Combined
                              };

            var extensionSyntax_join = cars.Join(manufacturers,                                // The inner join data
                                            c => new { c.Manufacturer, c.Year },          // outerKeySelector
                                            m => new { Manufacturer = m.Name, m.Year },   // innerKeySelector
                                            (c, m) => new                                 // result data selector
                                            {
                                                m.Headquarters,
                                                c.Name,
                                                c.Combined
                                            })
                                        .OrderByDescending(x => x.Combined)
                                        .ThenBy           (x => x.Name);

            //foreach (var item in extensionSyntax_join.Take(10))
            //{
            //    Console.WriteLine($"{item.Headquarters} : {item.Name} :  {item.Combined}");
            //}











            var topEfficient = cars.OrderByDescending(c => c.Combined)     //Order by can not be used more than once, while then by
                            .ThenBy(c => c.Name)                           //is used instead and then by can be used as much as needed
                            .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);
            // First returns one item which is the top of the sequence but if there is no items to return an exception is thrown. while
            // First and default returns the first or the default value which in this case is null
            //x Console.WriteLine(topEfficient.Name);

            var query2 = from car in cars
                         orderby car.Combined descending, car.Name ascending
                         select car;     // select here is just an identity protection not a transforming method                        
            foreach (var car in query2.Take(10))
            {
               //x Console.WriteLine($"{car.Name} : {car.Combined}");
            }

            var isAva = cars.Any(c => c.Manufacturer == "Ford"); // is any item with this criteria is available
            var isAll = cars.All(c => c.Manufacturer == "Ford"); // are all items with this criteria is available
            //x Console.WriteLine(isAll);

            // SelectMany() splits the string into characters
            var result = cars.SelectMany(c => c.Name);
            foreach (var item in result)
            {
                //Console.WriteLine(item); 
            }
                        
        }

        private static List<Manufacturer> ProcessManufactures(string path)
        {
            return File.ReadAllLines(path)
                    .Where(c => c.Length>1)
                    .ToManufacturer()
                    .ToList();
        }

        private static List<Cars> ProcessCars(string path)
        {
            //? Extension method syntax
            return
                File.ReadAllLines(path)             // Read all lines from the provided path file
                    .Skip(1)                        // skips first line
                    .Where(line => line.Length > 1)
                    .ToCar()                        // This an extension method used instead of the below method
                    //x .Select(Cars.ParseFromCsv)      // It maps from one form to another
                    .ToList();                      // Convert this into a concrete list to avoid rereading the file
            //! Equivalent to each other
            //? Query syntax
            //x var query = from line in File.ReadAllLines(path).Skip(1)
            //x             where line.Length > 1
            //x             select Cars.ParseFromCsv(line);
            //x 
            //x return query.ToList();
        }
    }
}
