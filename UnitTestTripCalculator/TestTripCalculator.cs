using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TripCalculator.Models;

namespace UnitTestTripCalculator
{
    [TestClass]
    public class TestTripCalculator
    {
        // Input
        /*
        [{"id":9,"name":"Friend9","payments":[{"amount":1},{"amount":2}]},
        {"id":2,"name":"Friend2","payments":[{"amount":12},{"amount":12}]},
        {"id":4,"name":"Friend4","payments":[{"amount":1},{"amount":12},{"amount":11},{"amount":13},{"amount":15}]},
        {"id":5,"name":"Friend5","payments":[{"amount":1},{"amount":11},{"amount":11},{"amount":8},{"amount":3}]},
        {"id":8,"name":"Friend8","payments":[{"amount":1},{"amount":12},{"amount":11},{"amount":13}]}]
        */
        // Output
        /*
[
    {
        "id": 9,
        "name": "Friend9",
        "payments": [
            {
                "amount": 1,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 2,
                "friendIdFrom": 0,
                "friendIdTo": 0
            }
        ],
        "paymentTotal": 3,
        "balance": -27,
        "balanceAfterPayingBack": 0,
        "backPayments": [
            {
                "amount": 22,
                "friendIdFrom": 9,
                "friendIdTo": 4
            },
            {
                "amount": 5,
                "friendIdFrom": 9,
                "friendIdTo": 8
            }
        ],
        "howToPayAtTheEnd": "Friend9 pays $22.00 to Friend4.Friend9 pays $5.00 to Friend8."
    },
    {
        "id": 2,
        "name": "Friend2",
        "payments": [
            {
                "amount": 12,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 12,
                "friendIdFrom": 0,
                "friendIdTo": 0
            }
        ],
        "paymentTotal": 24,
        "balance": -6,
        "balanceAfterPayingBack": 0,
        "backPayments": [
            {
                "amount": 0,
                "friendIdFrom": 2,
                "friendIdTo": 4
            },
            {
                "amount": 2,
                "friendIdFrom": 2,
                "friendIdTo": 8
            },
            {
                "amount": 4,
                "friendIdFrom": 2,
                "friendIdTo": 5
            }
        ],
        "howToPayAtTheEnd": "Friend2 pays $0.00 to Friend4.Friend2 pays $2.00 to Friend8.Friend2 pays $4.00 to Friend5."
    },
    {
        "id": 5,
        "name": "Friend5",
        "payments": [
            {
                "amount": 1,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 11,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 11,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 8,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 3,
                "friendIdFrom": 0,
                "friendIdTo": 0
            }
        ],
        "paymentTotal": 34,
        "balance": 4,
        "balanceAfterPayingBack": 0,
        "backPayments": null,
        "howToPayAtTheEnd": "Friend2 pays $4.00 to you."
    },
    {
        "id": 8,
        "name": "Friend8",
        "payments": [
            {
                "amount": 1,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 12,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 11,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 13,
                "friendIdFrom": 0,
                "friendIdTo": 0
            }
        ],
        "paymentTotal": 37,
        "balance": 7,
        "balanceAfterPayingBack": 0,
        "backPayments": null,
        "howToPayAtTheEnd": "Friend9 pays $5.00 to you.Friend2 pays $2.00 to you."
    },
    {
        "id": 4,
        "name": "Friend4",
        "payments": [
            {
                "amount": 1,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 12,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 11,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 13,
                "friendIdFrom": 0,
                "friendIdTo": 0
            },
            {
                "amount": 15,
                "friendIdFrom": 0,
                "friendIdTo": 0
            }
        ],
        "paymentTotal": 52,
        "balance": 22,
        "balanceAfterPayingBack": 0,
        "backPayments": null,
        "howToPayAtTheEnd": "Friend9 pays $22.00 to you.Friend2 pays $0.00 to you."
    }
]
         */


        static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        
        [TestMethod]
        public void TestGetDefaultRecords()
        {
            List<Expense> expenses = GetSampleRecords();

            //Read
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63118/");
                var response = client.GetStringAsync("expense").Result;

                Assert.IsTrue(response.Contains(":null,"));

                Assert.IsTrue(response.Contains("balanceAfterPayingBack\":0.0"));

                Assert.IsTrue(response.Contains("howToPayAtTheEnd\":null"));

                Assert.IsFalse(response.Contains("balanceAfterPayingBack\":0.01,"));

            }            
        }

        [TestMethod]
        public void TestPostMethod()
        {
            List<Expense> expenses = GetSampleRecords();

            // Post
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63118/");
                var response = client.PostAsJsonAsync("expense", expenses).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    Assert.IsTrue(responseString.Contains("Friend1 pays $13.34 to Friend3.Friend1 pays $3.33 to Friend2."));

                    Assert.IsTrue(responseString.Contains("Friend1 pays $3.33 to you."));

                    Assert.IsTrue(responseString.Contains("Friend1 pays $13.34 to you."));

                    Assert.IsTrue(responseString.Contains("balanceAfterPayingBack\":0.01,"));
                }
            }

            // Put is the same as Post
            // Keep this for future reference
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:63118/");
            //    var response = client.PutAsJsonAsync("expense", expenses).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseString = response.Content.ReadAsStringAsync().Result;
            //    }
            //}

            //Delete Call
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:63118/");
            //    var response = client.DeleteAsync("expense/1").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseString = response.Content.ReadAsStringAsync().Result;
            //    }
            //}

        }

        internal List<Expense> GetSampleRecords()
        {
            List<Expense> expenses = new List<Expense>();

            var newExpense1 = new Expense();
            newExpense1.Id = 1;
            newExpense1.Name = "Friend1";

            var payment1 = new Payment();
            payment1.Amount = 10;

            var payments1 = new List<Payment>();
            payments1.Add(payment1);
            newExpense1.Payments = payments1;
            //

            var newExpense2 = new Expense();
            newExpense2.Id = 2;
            newExpense2.Name = "Friend2";

            var payment2 = new Payment();
            payment2.Amount = 30;

            var payments2 = new List<Payment>();
            payments2.Add(payment2);
            newExpense2.Payments = payments2;
            //

            var newExpense3 = new Expense();
            newExpense3.Id = 3;
            newExpense3.Name = "Friend3";

            var payment3 = new Payment();
            payment3.Amount = 40;

            var payments3 = new List<Payment>();
            payments3.Add(payment3);
            newExpense3.Payments = payments3;

            return expenses;
        }
        
    }
}
