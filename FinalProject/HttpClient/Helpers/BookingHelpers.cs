using HttpClientLibrary.DataModels;
using HttpClientLibrary.Resources;
using HttpClientLibrary.Tests.TestData;
using Newtonsoft.Json;
using System.Text;


namespace HttpClientLibrary.Helpers
{
    /// <summary>
    /// Class containing all methods for booking and token
    /// </summary>
    public class BookingHelpers
    {
        /// <summary>
        /// Send Post request to create new booking
        /// </summary>
        public static async Task<HttpResponseMessage> CreateNewBookingData()
        {
            //initialization
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(GenerateBookingData.demoBooking());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            //Post Request
            return await httpClient.PostAsync(BookingEndpoints.GetURL(BookingEndpoints.BookingEndPoint), postRequest);
        }

        /// <summary>
        /// Send Get request to get booking id
        /// </summary>
        public static async Task<HttpResponseMessage> GetBookingDataById(long id)
        {
            //initialization
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //Get Request
            return await httpClient.GetAsync(BookingEndpoints.GetURL(BookingEndpoints.BookingEndPoint) + "/" + id);
        }

        /// <summary>
        /// Send Put request to update booking
        /// </summary>
        public static async Task<HttpResponseMessage> UpdateBookingDataById(long id, Booking objectModel)
        {
            //initialization
            var httpClient = new HttpClient();
            var token = await CreateToken();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            var request = JsonConvert.SerializeObject(objectModel);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            //put request
            return await httpClient.PutAsync(BookingEndpoints.GetURL(BookingEndpoints.BookingEndPoint) + "/" + id, putRequest);
        }

        /// <summary>
        /// Send Delete request to delete booking
        /// </summary>
        public static async Task<HttpResponseMessage> DeleteBookingDataById(long id)
        {
            //initialization
            var httpClient = new HttpClient();
            var token = await CreateToken();

            //delete request
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + token);

            return await httpClient.DeleteAsync(BookingEndpoints.GetURL(BookingEndpoints.BookingEndPoint) + "/" + id);

        }

        /// <summary>
        /// Send Post request to create token
        /// </summary>
        public static async Task<string> CreateToken()
        {
            //initialization
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var request = JsonConvert.SerializeObject(GenerateTokenData.generateToken());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            //Post Request
            var httpResponse = await httpClient.PostAsync(BookingEndpoints.GetURL(BookingEndpoints.AuthenticateUser), postRequest);
            var token = JsonConvert.DeserializeObject<TokenResponse>(httpResponse.Content.ReadAsStringAsync().Result);

            return token.Token;
        }
    }
}
