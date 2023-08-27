using System;
using System.Net.Http.Json;
using WebApplicationStock.Client.Services.PersonService;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Client.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;

        public PersonService(HttpClient http)
        {
            _http = http;
        }
        public List<UserAccount> Persons { get; set; } = new List<UserAccount>();

        public async Task GetPersons()
        {
            Persons = await _http.GetFromJsonAsync<List<UserAccount>>("api/account");
        }
    }

}
