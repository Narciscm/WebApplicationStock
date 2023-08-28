using System;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Client.Services.PersonService
{
    public interface IPersonService
    {
        //List of users form Db
        List<UserAccount> Persons { get; set; }

        //Add a task to get the users
        Task GetPersons();

        //Add a task to create a new user
        Task CreatePersons(UserAccount userAccount);

        //Add a task to delete a user
        Task DeletePersons(UserAccount userAccount);
    }
}
