using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    [TestClass]
    public class TypicodeApp
    {
        private RestClient client;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the RestClient
            client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        [TestMethod]
        public void TestUserExists_Karianne()
        {
            var request = new RestRequest("users", Method.Get);
            var response = client.Execute(request);

            Assert.IsTrue(response.IsSuccessful, "Failed to get users");

            var users = JArray.Parse(response.Content);
            var userExists = false;

            foreach (var user in users)
            {
                if (user["username"].ToString() == "Karianne")
                {
                    userExists = true;
                    break;
                }
            }

            Assert.IsTrue(userExists, "User 'Karianne' does not exist");
        }

        [TestMethod]
        public void TestAddNewPost()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddJsonBody(new
            {
                title = "New Post Title",
                body = "This is the body of the new post.",
                userId = 1
            });

            var response = client.Execute(request);

            Assert.IsTrue(response.IsSuccessful, "Failed to create a new post");

            var post = JObject.Parse(response.Content);
            Assert.IsNotNull(post["id"], "Post ID is missing in the response");
            Console.WriteLine($"New post created with ID: {post["id"]}");
        }

        [TestMethod]
        public void TestResponseTime()
        {
            var request = new RestRequest("posts", Method.Get);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var response = client.Execute(request);
            watch.Stop();

            var responseTime = watch.ElapsedMilliseconds;
            long threshold = 900; // Set the threshold for response time (in milliseconds)

            Console.WriteLine($"Response time: {responseTime}ms");

            Assert.IsTrue(responseTime <= threshold, $"Test failed: Response time exceeded the threshold of {threshold}ms. Actual response time: {responseTime}ms");
        }

        [TestCleanup]
        public void Cleanup()
        {
            client = null;
        }
    }
}
   
