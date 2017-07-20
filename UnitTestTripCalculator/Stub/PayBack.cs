using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalculator.Models
{
    /// <summary>
    /// This is for a object to output
    /// </summary>
    public class PayBack
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Contains Amount, Friends: from and to
        /// </summary>
        public List<Payment> Payments { get; set; }

        /// <summary>
        /// This is the ultimate comments what a friend, who paid less than average, should do
        /// </summary>
        public string HowToPayAtTheEnd { get; set; }
    }
}
