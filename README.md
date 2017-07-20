# TripCalculatorWithUnitTest
Friends go to a trip and share the costs at the end of the trip.

#Code Example

Show what the library does as concisely as possible, developers should be able to figure out how your project solves their problem by looking at the code example. Make sure the API you are showing off is obvious, and that your code is short and concise.

#Motivation

When friends go trip togethe, they may have a hard time to split the cost.  This application will help out to split the cost.
When one cent is not distributed correctly, friends can sort that out without using this app.

#Installation

Steps to Deploy ASP.NET Core to IIS

Before you deploy, you need to make sure that WebHostBuilder is configured properly to use Kestrel and IIS. Your web.config should also exist and look similar to our example above.

Step 1: Publish to a File Folder


Step 2: Copy Files to Preferred IIS Location

Now you need to copy your publish output to where you want the files to live. If you are deploying to a remote server, you may want to zip up the files and move to the server. If you are deploying to a local dev box, you can copy them locally.

Step 3: Create Application in IIS

First, create a new IIS Application Pool. You will want to create one under the .NET CLR version of “No Managed Code“. Since IIS only works as a reverse proxy, it isn’t actually executing any .NET code.

Second, create your new application under your existing IIS Site, or create a new IIS site. Either way, you will want to pick your new IIS Application Pool and point it at the folder you copied your ASP.NET publish output files to.

Step 4: Load Your App!

At this point, your application should load just fine. If it does not, check the output logging from it. Within your web.config file you define how IIS starts up your ASP.NET Core process.
