using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalculator.Models
{
    public class Expense
    {
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Name of this friend
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// List of this friend's payment items
        /// </summary>
        public List<Payment> Payments { get; set; }

        /// <summary>
        /// This is before simple paid amount not being adjusted by average
        /// </summary>
        public decimal PaymentTotal { get; set; }

        /// <summary>
        /// Original balance of each friend
        /// </summary>
        public decimal Balance { get; set; }        

        /// <summary>
        /// This is to set the balance to 0, but did not want to alter original balance
        /// </summary>
        public decimal BalanceAfterPayingBack { get; set; }

        // Show how much to payback
        public List<Payment> BackPayments { get; set; }

        /// <summary>
        /// This is the ultimate comments what a friend, who paid less than average, should do
        /// </summary>
        public string HowToPayAtTheEnd { get; set; }
    }
}
