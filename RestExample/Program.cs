using RestSharp;

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);

var getUserRequest = new RestRequest("users/2", Method.Get);