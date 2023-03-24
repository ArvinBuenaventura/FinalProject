using RestSharpLibrary.DataModels;
using RestSharp;
using RestSharpLibrary.Tests.TestData;
using RestSharpLibrary.Resources;

namespace RestSharpLibrary.Helpers
{
    /// <summary>
    /// Class containing all methods for booking and token
    /// </summary>
    public class BookingHelpers
    {
        /// <summary>
        /// Send Post request to create new booking
        /// </summary>
        public static async Task<RestResponse<BookingModels>> CreateNewBookingData(RestClient restClient)
        {
            //initialization
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            //post request
            var postRequest = new RestRequest(BookingEndpoints.GetURL(BookingEndpoints.BookingEndPoint)).AddJsonBody(GenerateBookingData.demoBooking());
            return await restClient.ExecutePostAsync<BookingModels>(postRequest);
        }

        /// <summary>
        /// Send Get request to get booking id
        /// </summary>
        public static async Task<RestResponse<Booking>> GetBookById(RestClient restClient, long id)
        {
            //initialization
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            //get request
            var getRequest = new RestRequest(BookingEndpoints.GetUri(BookingEndpoints.BookingEndPoint) + "/" + id);
            return await restClient.ExecuteGetAsync<Booking>(getRequest);

        }

        /// <summary>
        /// Send Put request to update booking
        /// </summary>
        public static async Task<RestResponse<Booking>> UpdateBookingData(RestClient restClient, long id, Booking objectModel)
        {
            //initialization
            restClient = new RestClient();
            var token = await CreateToken(restClient);

            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            //Put request
            var putRequest = new RestRequest(BookingEndpoints.GetUri(BookingEndpoints.BookingEndPoint) + "/"+ id).AddJsonBody(objectModel);
            return await restClient.ExecutePutAsync<Booking>(putRequest);
        }

        /// <summary>
        /// Send Delete request to delete booking
        /// </summary>
        public static async Task<RestResponse> DeleteBookingData(RestClient restClient, long id)
        {
            //initialization
            restClient = new RestClient();
            var token = await CreateToken(restClient);

            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            //delete request
            var deleteRequest = new RestRequest(BookingEndpoints.GetUri(BookingEndpoints.BookingEndPoint) + "/" + id);
            return await restClient.DeleteAsync(deleteRequest);
        }

        /// <summary>
        /// Send Post request to create token
        /// </summary>
        public static async Task<string> CreateToken(RestClient restClient)
        {
            //initialization
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            //post request
            var postRequest = new RestRequest(BookingEndpoints.GetURL(BookingEndpoints.AuthenticateUser)).AddJsonBody(GenerateTokenData.generateToken());
            var postResponse = await restClient.ExecutePostAsync<TokenResponse>(postRequest);

            return postResponse.Data.Token;
        }
    }
}
