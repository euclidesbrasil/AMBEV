using Ambev.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.ValueObjects
{
    public class Rating : ValueObject
    {
        public double Rate { get; private set; }
        public int Count { get; private set; }

        // Construtor para garantir integridade
        public Rating(double rate, int count)
        {
            if (rate < 0 || rate > 5)
                throw new ArgumentException("Rate must be between 0 and 5.");
            if (count < 0)
                throw new ArgumentException("Count must be a non-negative number.");

            Rate = rate;
            Count = count;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rate;
            yield return Count;
        }
    }
}
