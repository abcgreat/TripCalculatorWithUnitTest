using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TripCalculator.Models;
using System.Net;
using System.Net.Http;

namespace TripCalculator.Controllers
{
    [Produces("application/json")]
    [Route("/{controll}")]
    public class ExpenseController : Controller
    {
        static List<Expense> expenses = new List<Expense>()
        {
            new Expense() {Id = 1, Name = "Friend1"},
            new Expense() {Id = 2, Name = "Friend2"},
            new Expense() {Id = 3, Name = "Friend3"}
        };

        [HttpGet]
        public IEnumerable<Expense> Get()
        {
            return expenses;
        }

        public Expense Get(int id)
        {
            return expenses.FirstOrDefault(s => s.Id == id);
        }

        [AcceptVerbs("POST", "PUT")]
        public List<Expense> CalculatedResults([FromBody] List<Expense> expenseRecords)
        {
            if (ModelState.IsValid)
            {
                var calculatedExpense = new List<Expense>(expenseRecords);
                decimal totalExpense = 0;
                decimal averageExpense = 0;

                decimal totalBalance = 0;

                //var friends = new List<Friend>();
                IDictionary<int, string> friends = new Dictionary<int, string>();

                // Calculate the total amount
                // Make a dictionary of friends as well
                totalExpense = GetTotalExpense(calculatedExpense, friends);

                // Average
                if (calculatedExpense.Count > 0)
                    averageExpense = totalExpense / calculatedExpense.Count;

                SetEachBalance(calculatedExpense, averageExpense, totalBalance);

                var sortedExpense = new List<Expense>(calculatedExpense.OrderBy(o => o.Balance));

                var passBalanceCheck = false; // set it to true at the start

                // Pay back to balance
                int index = 0;
                int theLastIndex = sortedExpense.Count - 1;
                decimal checkBalance = 0;

                // This is a part to calculate
                while (passBalanceCheck == false)
                {
                    if (sortedExpense[index].BalanceAfterPayingBack < 0)
                    {
                        PayOthers(index, theLastIndex, sortedExpense, friends);
                    }

                    checkBalance += sortedExpense[index].BalanceAfterPayingBack;

                    if (index == theLastIndex)
                    {
                        if (index == theLastIndex && Math.Abs(checkBalance) <= (decimal)0.01)
                        {
                            passBalanceCheck = true;
                        }
                        else
                        {
                            index = 0; // keep on doing it
                            checkBalance = 0;
                        }
                    }
                    else
                    {
                        ++index;
                    }
                }

                return sortedExpense;
            }
            else
            {
                // just return passed object
                return expenseRecords;
            }

        }

        /// <summary>
        /// Function to calculate and calls itself recursively
        /// </summary>
        /// <param name="indexForward"></param>
        /// <param name="indexBackward"></param>
        /// <param name="sortedExpense"></param>
        /// <param name="friends"></param>
        private void PayOthers(int indexForward, int indexBackward, List<Expense> sortedExpense, IDictionary<int, string> friends)
        {
            if (sortedExpense[indexForward].BackPayments is null)
            {
                sortedExpense[indexForward].BackPayments = new List<Payment>();
            }

            var payment = new Payment();
            payment.FriendIdFrom = sortedExpense[indexForward].Id;
            payment.FriendIdTo = sortedExpense[indexBackward].Id;
            
            if (Math.Abs(sortedExpense[indexForward].BalanceAfterPayingBack) <= Math.Abs(sortedExpense[indexBackward].BalanceAfterPayingBack))
            {
                payment.Amount = Math.Abs(sortedExpense[indexForward].BalanceAfterPayingBack);
                
                sortedExpense[indexBackward].BalanceAfterPayingBack -= payment.Amount;  // subtraction
                sortedExpense[indexForward].BalanceAfterPayingBack += Math.Abs(payment.Amount); // addition

                sortedExpense[indexForward].BackPayments.Add(payment);
                
                sortedExpense[indexForward].HowToPayAtTheEnd += string.Format("{0} pays {1:C} to {2}.", friends[payment.FriendIdFrom], Math.Abs(payment.Amount), friends[payment.FriendIdTo]);
                sortedExpense[indexBackward].HowToPayAtTheEnd += string.Format("{0} pays {1:C} to you.", friends[payment.FriendIdFrom], Math.Abs(payment.Amount));

            }
            else // for the case of a > b
            {
                payment.Amount = sortedExpense[indexBackward].BalanceAfterPayingBack;

                sortedExpense[indexForward].BalanceAfterPayingBack += payment.Amount;  // add the positive value
                sortedExpense[indexBackward].BalanceAfterPayingBack -= payment.Amount;  // subtraction

                sortedExpense[indexForward].BackPayments.Add(payment);
                
                sortedExpense[indexForward].HowToPayAtTheEnd += string.Format("{0} pays {1:C} to {2}.", friends[payment.FriendIdFrom], Math.Abs(payment.Amount), friends[payment.FriendIdTo]);
                sortedExpense[indexBackward].HowToPayAtTheEnd += string.Format("{0} pays {1:C} to you.", friends[payment.FriendIdFrom], Math.Abs(payment.Amount));
                
                // Call it recursively
                PayOthers(indexForward, indexBackward - 1, sortedExpense, friends);
            }
        }
        
        /// <summary>
        /// Calculates balance of each record
        /// </summary>
        /// <param name="calculatedExpense"></param>
        /// <param name="averageExpense"></param>
        /// <param name="totalBalance"></param>
        private void SetEachBalance(List<Expense> calculatedExpense, decimal averageExpense, decimal totalBalance)
        {
            foreach (Expense eachExpense in calculatedExpense)
            {
                var balance = eachExpense.PaymentTotal - averageExpense;

                // do not do the math when balance is 0.
                if (balance > 0)
                {
                    // Any fraction owe should become 1 cent

                    // Assumption is that friends would feel that friend who deligently made payments would not feel discrouraged next time, thus, may get 1 cent more in this particular situation.
                    // Some friends may gain so much of cents afterall.
                    // This may encouage to pay more than average every time.
                    eachExpense.Balance = Math.Round((Math.Ceiling((eachExpense.PaymentTotal - averageExpense) * 1000) + 5) / 1000, 2);
                }
                if (balance < 0)
                {
                    // Any fraction owe should become 1 cent
                    // Assumption is that friends would feel alright to give 1 cent more for the people who deligently made payments
                    eachExpense.Balance = (-1) * Math.Round((Math.Ceiling((averageExpense - eachExpense.PaymentTotal) * 1000) + 5) / 1000, 2);
                }

                totalBalance += eachExpense.Balance;
                eachExpense.BalanceAfterPayingBack = eachExpense.Balance;
            }
        }

        /// <summary>
        /// Calculate how much was spent during the trips
        /// </summary>
        /// <param name="calculatedExpense"></param>
        /// <param name="friends"></param>
        /// <returns></returns>
        private decimal GetTotalExpense(List<Expense> calculatedExpense, IDictionary<int, string> friends)
        {
            decimal totalExpense = 0;
            decimal subTotalExpense = 0;

            foreach (Expense eachExpense in calculatedExpense)
            {
                subTotalExpense = 0;

                if (eachExpense.Payments.Count > 0)
                {
                    foreach (Payment eachPayment in eachExpense.Payments)
                    {
                        subTotalExpense += eachPayment.Amount;
                    }

                    totalExpense += subTotalExpense;
                    eachExpense.PaymentTotal = subTotalExpense;
                }

                // get id and name to a dictionary
                // Assumes unique Id
                friends.Add(eachExpense.Id, eachExpense.Name);
            }

            return totalExpense;
        }

    }
}