using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripCalculator.Models
{
    /// <summary>
    /// Keeps a name of friend and Id for a friend. 
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// This is a unique key for each friend
        /// </summary> 
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
