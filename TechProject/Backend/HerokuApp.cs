using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace Backend
{
    [TestClass]
    public class HerokuApp
    {
        private RestClient client;
        private const string baseUrl = "https://albums-collection-service.herokuapp.com";
        private const string albumEndpoint = "albums";
        private string albumId;

        [TestInitialize]
        public void Setup()
        {
            // Initialize RestClient for the API
            client = new RestClient(baseUrl);
        }

        // a. Check that a new album can be created
        [TestMethod]
        public void TestCreateDeleteAlbum()
        {
            // Step 1: Get the total number of albums
            var getRequest = new RestRequest(albumEndpoint, Method.Get);
            var getResponse = client.Execute(getRequest);            

            Assert.IsTrue(getResponse.IsSuccessful, "Failed to get albums list");
            var albums = JArray.Parse(getResponse.Content);
            int initialAlbumCount = albums.Count;
            Console.WriteLine($"The total number of albums is: {initialAlbumCount}");

            // Step 2: Create a new album
            var createRequest = new RestRequest(albumEndpoint, Method.Post);
            var newAlbum = new
            {
                title = "Test Album",
                artist = "Test Artist",
                genre = "Test Genre",
                label = "Test Label",
                songs = 5,
                year = 2025
            };
            createRequest.AddJsonBody(newAlbum);

            var createResponse = client.Execute(createRequest);
            Assert.IsTrue(createResponse.IsSuccessful, "Failed to create album");

            var createdAlbum = JObject.Parse(createResponse.Content);
            albumId = createdAlbum["album_id"].ToString();
            Assert.IsNotNull(albumId, "Album ID is missing in the response");

            // Step 3: Validate album was created
            var getNewAlbumBasedOnIdRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Get);
            Assert.IsTrue(client.Execute(getNewAlbumBasedOnIdRequest).IsSuccessful);

            // Step 4: Delete the created album
            var deleteRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Delete);
            var deleteResponse = client.Execute(deleteRequest);
            Assert.IsTrue(deleteResponse.IsSuccessful, "Failed to delete album");

            // Step 5: Validate the total number of albums decreased after deletion
            getResponse = client.Execute(getRequest);
            Assert.IsTrue(getResponse.IsSuccessful, "Failed to get albums list after deletion");

            albums = JArray.Parse(getResponse.Content);
            Assert.AreEqual(initialAlbumCount, albums.Count, "Album count did not decrease after deletion");
        }

        // b. Check that an album can be updated
        [TestMethod]
        public void TestUpdateAlbum()
        {
            // Step 1: Create a new album with full information
            var createNewAlbumRequest = new RestRequest(albumEndpoint, Method.Post);
            var newAlbum = new
            {
                title = "Original Album",
                artist = "Original Artist",
                genre = "Original Genre",
                label = "Original Label",
                songs = 3,
                year = 2000
            };
            createNewAlbumRequest.AddJsonBody(newAlbum);

            var createNewAlbumResponse = client.Execute(createNewAlbumRequest);
            Assert.IsTrue(createNewAlbumResponse.IsSuccessful, "Failed to create album");

            var createdAlbum = JObject.Parse(createNewAlbumResponse.Content);
            albumId = createdAlbum["album_id"].ToString();
            Assert.IsNotNull(albumId, "Album ID is missing in the response");

            // Step 2: Validate album was created
            var getNewAlbumBasedOnIdRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Get);
            Assert.IsTrue(client.Execute(getNewAlbumBasedOnIdRequest).IsSuccessful);

            // Step 3: Change the album title, songs, and year
            var updateAlbumRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Patch);
            var updatedAlbum = new
            {
                title = "Vlad Album",          
                songs = 30,
                year = 2007
            };
            updateAlbumRequest.AddJsonBody(updatedAlbum);

            var updateAlbumResponse = client.Execute(updateAlbumRequest);
            Assert.IsTrue(updateAlbumResponse.IsSuccessful, "Failed to update album");

            // Step 4: Validate album was updated
            var getUpdatedAlbumRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Get);
            var getUpdatedAlbumResponse = client.Execute(getUpdatedAlbumRequest);
            var updatedAlbumResponse = JObject.Parse(getUpdatedAlbumResponse.Content);

            Assert.AreEqual("Vlad Album", updatedAlbumResponse["title"].ToString());
            Assert.AreEqual(30, updatedAlbumResponse["songs"].ToObject<int>());
            Assert.AreEqual(2007, updatedAlbumResponse["year"].ToObject<int>());

            // Step 5: Delete the genre and year of the album
            var deleteAlbumFieldsRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Patch);
            var partialUpdateAlbum = new Dictionary<string, object>
{
                { "year", null },
                { "genre", null }};

            deleteAlbumFieldsRequest.AddJsonBody(partialUpdateAlbum);
            var partialUpdateAlbumResponse = client.Execute(deleteAlbumFieldsRequest);
            Assert.IsTrue(partialUpdateAlbumResponse.IsSuccessful, "Failed to partially update album");

            // Step 6: Validate album was updated
            var getRequestAfterDeleteOfGenreYear = new RestRequest($"{albumEndpoint}/{albumId}", Method.Get);
            var getResponseAfterDeleteOfGenreYear = client.Execute(getRequestAfterDeleteOfGenreYear);
            var deletedFieldsAlbumResponse = JObject.Parse(getResponseAfterDeleteOfGenreYear.Content);

            Assert.IsTrue(deletedFieldsAlbumResponse["genre"].Type == JTokenType.Null, "Genre was not deleted");
            Assert.IsTrue(deletedFieldsAlbumResponse["year"].Type == JTokenType.Null, "Year was not deleted");

            // Step 7: Delete created album
            var deleteAlbumRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Delete);
            var deleteAlbumResponse = client.Execute(deleteAlbumRequest);
            Assert.IsTrue(deleteAlbumResponse.IsSuccessful, "Failed to delete album");

            // Step 8: Validate album was not found after deletion
            var getDeletedAlbumRequest = new RestRequest($"{albumEndpoint}/{albumId}", Method.Get);
            var getDeletedAlbumResponse = client.Execute(getDeletedAlbumRequest);
            Assert.IsFalse(getDeletedAlbumResponse.IsSuccessful, "Album was not deleted");
        }
       
    }
}
