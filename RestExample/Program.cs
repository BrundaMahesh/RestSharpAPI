using RestSharp;

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);

//GET
var getUserRequest = new RestRequest("users/2", Method.Get);
var getUserResponse = client.Execute(getUserRequest);
Console.WriteLine("GET Response: \n" + getUserResponse.Content);

//POST
var createUserRequest  = new RestRequest("users",Method.Post);
createUserRequest.AddParameter("name", "John Doe");
createUserRequest.AddParameter("job", "Software Developer");

var createUserResponse  = client.Execute(createUserRequest);
Console.WriteLine("POST Response: \n" + createUserResponse.Content);

//PUT
var  updateUserRequest = new RestRequest("users/2", Method.Put);
updateUserRequest.AddParameter("name", "Updated John Doe");
updateUserRequest.AddParameter("job", "Senior Software Developer");

var updateUserResponse = client.Execute(updateUserRequest);
Console.WriteLine("PUT Response: \n" + updateUserResponse.Content);

//DELETE
var deleteUserRequest = new RestRequest("users/2", Method.Delete);
var deleteUserResponse = client.Execute(deleteUserRequest);
Console.WriteLine("DELETE Response: \n" + deleteUserResponse.Content);
