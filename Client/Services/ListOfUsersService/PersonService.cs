using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http.Json;
using WebApplicationStock.Client.Pages;
using WebApplicationStock.Client.Services.PersonService;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Client.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        //Constructor with http param
        public PersonService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        //Create a list of users from Db
        public List<UserAccount> Persons { get; set; } = new List<UserAccount>();

        //Create a task to get persons
        public async Task GetPersons()
        {
            Persons = await _http.GetFromJsonAsync<List<UserAccount>>("api/account");
        }


        //Create a task to get persons by ID
        public async Task<UserAccount> GetPersonById(int id)
        {
            var result = await _http.GetFromJsonAsync<UserAccount>($"api/account/{id}");
            if (result != null)
                return result;
            throw new Exception("User not found");
        }

        //Create a task to create a new user
        /* public async Task CreatePersons(UserAccount userAccount)
         {
             //Persons = await _http.PostAsJsonAsync<List<UserAccount>>("api/account", userAccount);
         }

          //create a task to delete a user
         public async Task DeletePersons(UserAccount userAccount)
         {
             //Persons = await _http.DeleteAsync<List<UserAccount>>("api/account", userAccount);
         }
        */

        //Create a task to create a new user
        public async Task CreatePersons(UserAccount userAccount)
        {
            var result = await _http.PostAsJsonAsync("api/account", userAccount);
            await SetUser(result);
        }

        private async Task SetUser(HttpResponseMessage result) 
        {
            var response = await result.Content.ReadFromJsonAsync<List<UserAccount>>();
            Persons = response;
            _navigationManager.NavigateTo("userslist");
        }

        //create a task to delete a user
        public async Task DeletePerson(int id)
        {
            await _http.DeleteAsync($"api/account/{id}");
        }

        //Update a user
        public async Task UpdateUser(UserAccount userAccount)
        {
            var result = await _http.PutAsJsonAsync($"api/account/{userAccount.Id}", userAccount);
            await SetUser(result);
        }
    }

}
