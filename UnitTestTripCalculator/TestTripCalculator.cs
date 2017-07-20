using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using TripCalculator.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        [TestMethod]
        public void WebApiIsReady()
        {


            //HttpResponseMessage response = await client.GetAsync(path);
            //if (response.IsSuccessStatusCode)
            //{
            //    expenses = await response.Content.ReadAsAsync<Expense>();
            //}
            //return expenses;


            //var client = new HttpClient(); // no HttpServer

            //var request = new HttpRequestMessage
            //{
            //    RequestUri = new Uri("http://localhost:63118/expense"),
            //    Method = HttpMethod.Get
            //};

            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //using (var response = client.SendAsync(request).Result)
            //{
            //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //}
        }
    }
}
