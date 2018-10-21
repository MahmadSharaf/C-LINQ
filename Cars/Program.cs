﻿using System;
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
            var cars = ProcessFile("fuel.csv");
            var query1 = cars.OrderByDescending(c => c.Combined)     //Order by can not be used more than once, while then by
                            .ThenBy(c => c.Name)                     //is used instead and then by can be used as much as needed
                            .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016); 
            // First returns one item which is the top of the sequence but if there is no items to return an exception is thrown. while
            // First and default returns the first or the default value which in this case is null
            var query2 = from car in cars
                         orderby car.Combined descending, car.Name ascending
                         select car;                                // select here is just an identity protection not a transforming method
            Console.WriteLine(query1.Name);
            foreach (var car in query2.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }
        }

        private static List<Cars> ProcessFile(string path)
        {
            //? Extension method syntax
            return
                File.ReadAllLines(path)             // Read all lines from the provided path file
                    .Skip(1)                        // skips first line
                    .Where(line => line.Length > 1)
                    .Select(Cars.ParseFromCsv)      // It maps from one form to another
                    .ToList();                      // Convert this into a concrete list to avoid rereading the file
            //! Equivalent to each other
            //? Query sytax
            //x var query = from line in File.ReadAllLines(path).Skip(1)
            //x             where line.Length > 1
            //x             select Cars.ParseFromCsv(line);
            //x 
            //x return query.ToList();
        }
    }
}
