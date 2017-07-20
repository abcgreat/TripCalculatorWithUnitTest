using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalculator.Models
{
    /// <summary>
    /// Keeps Transacted amount and direction of a payment
    /// </summary>
    public class Payment
    {
        [Required]
        public decimal Amount { get; set; }

        public int FriendIdFrom { get; set; }
        public int FriendIdTo { get; set; }
    }
}
