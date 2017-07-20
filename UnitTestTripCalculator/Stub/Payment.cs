using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalculator.Models
{
    /// <summary>
    /// Keeps Transacted amount and direction of a payment
    /// </summary>
    public class Payment
    {
        public decimal Amount { get; set; }

        public int FriendIdFrom { get; set; }
        public int FriendIdTo { get; set; }
    }
}
