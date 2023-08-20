using Blazored.SessionStorage;
using System.Text.Json;
using System.Text;

namespace WebApplicationStock.Client.Extensions
{
    public static class SessionStorageServiceExtension
    {
        //Create a method to save data by encoding the json format of the object
        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService, string key, T item)
        {
            //Create an object
            var itemJson = JsonSerializer.Serialize(item);
            //Covert the json string to bytes
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
            //Covert the bytes to base64string
            var base64Json = Convert.ToBase64String(itemJsonBytes);
            //Save the base64string in the session storage
            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        //Create a method to read data from the session storage
        public static async Task<T> ReadEncryptedItemAsync<T>(this ISessionStorageService sessionStorageService, string key)
        {
            //Read the base64string from the session storage
            var base64Json = await sessionStorageService.GetItemAsync<string>(key);
            //Covert the base64string to bytes
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            //Read the json from bytes
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            //Save the object
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return item;
        }
    }
}
