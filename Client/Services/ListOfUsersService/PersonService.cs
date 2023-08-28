using System;
using System.Net.Http.Json;
using WebApplicationStock.Client.Services.PersonService;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Client.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;

        //Constructor with http param
        public PersonService(HttpClient http)
        {
            _http = http;
        }

        //Create a list of users from Db
        public List<UserAccount> Persons { get; set; } = new List<UserAccount>();

        //Create a task to get persons
        public async Task GetPersons()
        {
            Persons = await _http.GetFromJsonAsync<List<UserAccount>>("api/account");
        }

        
        //Create a task to create a new user
        public async Task CreatePersons(UserAccount userAccount)
        {
            //Persons = await _http.PostAsJsonAsync<List<UserAccount>>("api/account", userAccount);
        }

        //create a task to delete a user
        public async Task DeletePersons(UserAccount userAccount)
        {
            //Persons = await _http.DeleteAsync<List<UserAccount>>("api/account", userAccount);
        }
        
    }

}
