using System;
using WebApplicationStock.Shared;

namespace WebApplicationStock.Client.Services.PersonService
{
    public interface IPersonService
    {
        List<UserAccount> Persons { get; set; }
        Task GetPersons();

    }
}
