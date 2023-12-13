using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using RestExampleWithNUnit.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExampleWithNUnit
{
    [TestFixture]
    public class ReqResTests:CoreCodes
    {

        [Test]
        [Order(0)]
        public void GetSingleUserTest()
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser Test Started");

            var request = new RestRequest("users/2", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                UserData? user = userdata?.Data;

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(2));
                Log.Information("User Id matches with fetch");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Get Single User Test passed all Asserts.");

                test.Pass("GetSingleUserTest passed all Asserts.");
            }
            catch(AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }
        }

        [Test]
        [Order(1)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser Test Started");

            var request = new RestRequest("users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "John Doe", job = "Sofware Developer" });

            var response = client.Execute(request);
            try
            {

                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");

                Assert.IsNotEmpty(user.Email);
                Log.Information("CreateUser test passed all Asserts");

                test.Pass("CreateUserTest passed all Asserts.");
            }
            catch(AssertionException)
            {
                test.Fail("Create User test failed");
            }
        }

        [Test]
        [Order(2)]
        public void UpdateUserTest()
        {
            test = extent.CreateTest("Update User");
            Log.Information("UpdateUserTest Started");

            var request = new RestRequest("users/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Updated John Doe", job = "Senior Sofware Developer" });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User updated and returned");

                Log.Information("UpdateUser test passed all Asserts");

                test.Pass("UpdateUserTest passed all Asserts.");
            }
            catch(AssertionException)
            {
                test.Fail("Update User test failed");
            }
        }

        [Test]
        [Order(3)]
        public void DeleteUserTest()
        {
            test = extent.CreateTest("Delete User");
            Log.Information("DeleteUserTest Started");

            var request = new RestRequest("users/2", Method.Delete);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information("User deleted");
                Log.Information("Delete User test passed");

                test.Pass("Delete User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User test failed");
            }
        }

        [Test]
        [Order(4)]
        public void GetNonExistingUserTest()
        {
            test = extent.CreateTest("Get Non-Existing User");
            Log.Information("Get Non-Exixting User Test Started");

            var request = new RestRequest("users/999", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("Get Non-Existing User test passed");

                test.Pass("Get Non-Existing User test passed");
            }
            catch(AssertionException)
            {
                test.Fail("Get Non-Existing User test failed");
            }
        }
    }
}
