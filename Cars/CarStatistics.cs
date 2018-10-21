using System;

namespace Cars
{
    public class CarStatistics
    {
        public CarStatistics()
        {
            Max = int.MinValue;
            Min = int.MaxValue;
            Total = 0;
            Count = 0;
        }

        public CarStatistics Accumlate(Cars c)
        {

            Max = Math.Max(Max, c.Combined);
            Min = Math.Min(Min, c.Combined);
            Total += c.Combined;
            Count += 1;
            Avg = Total / Count;
            return this;
        }

        internal CarStatistics Compute()
        {
            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Avg { get; set; }

        
    }
}