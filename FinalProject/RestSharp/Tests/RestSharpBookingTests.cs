using RestSharpLibrary.DataModels;
using RestSharpLibrary.Helpers;                                                                                                
using System.Net;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace RestSharpLibrary.Tests
{
    [TestClass]
    public class RestSharpBookingTests : ApiBaseTest
    {
        [TestMethod]
        public async Task CreateNewBookingDetails()
        {
            //Creating and retrieving new booking data
            var postReponse = await BookingHelpers.CreateNewBookingData(RestClient);
            var getReponse = await BookingHelpers.GetBookById(RestClient, postReponse.Data.Bookingid);

            //passing values from the retrieved data
            var createdBookingData = postReponse.Data;
            var retrievedBookingData = getReponse.Data;

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, postReponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(createdBookingData.Booking.Firstname, retrievedBookingData.Firstname, "First Name is not matching.");
            Assert.AreEqual(createdBookingData.Booking.Lastname, retrievedBookingData.Lastname, "Last Name is not matching.");
            Assert.AreEqual(createdBookingData.Booking.Totalprice, retrievedBookingData.Totalprice, "Total Price is not matching.");
            Assert.AreEqual(createdBookingData.Booking.Depositpaid, retrievedBookingData.Depositpaid, "Deposit Paid is not matching.");
            Assert.AreEqual(createdBookingData.Booking.Bookingdates.Checkin, retrievedBookingData.Bookingdates.Checkin, "Checkin date is not matching.");
            Assert.AreEqual(createdBookingData.Booking.Bookingdates.Checkout, retrievedBookingData.Bookingdates.Checkout, "Checkout date is not matching.");
            Assert.AreEqual(createdBookingData.Booking.Additionalneeds, retrievedBookingData.Additionalneeds, "Additional needs is not matching.");

            //Cleanup
            var deleteRequest = await BookingHelpers.DeleteBookingData(RestClient, createdBookingData.Bookingid);


        }

        [TestMethod]
        public async Task UpdateFirstAndLastNames()
        {
            //create data and send put request
            var postResponse = await BookingHelpers.CreateNewBookingData(RestClient);
            var postCreatedBooking = postResponse.Data;
            Booking booking = new Booking()
            {
                Firstname = "Milkty01",
                Lastname = "Cutie01",
                Totalprice = 116565,
                Depositpaid = true,
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Parse("2023-03-23"),
                    Checkout = DateTime.Parse("2023-03-23")
                },
                Additionalneeds = "Spoon"
            };

            //Act
            var putResponse = await BookingHelpers.UpdateBookingData(RestClient, postCreatedBooking.Bookingid, booking);
            //passing putresponse data
            var updatedBookingData = putResponse.Data;

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(updatedBookingData.Firstname, booking.Firstname, "First Name is not matching.");
            Assert.AreEqual(updatedBookingData.Lastname, booking.Lastname, "Last Name is not matching.");
            Assert.AreEqual(updatedBookingData.Totalprice, postCreatedBooking.Booking.Totalprice, "Total price is not matching.");
            Assert.AreEqual(updatedBookingData.Depositpaid, postCreatedBooking.Booking.Depositpaid, "Deposit paid is not matching.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkin, postCreatedBooking.Booking.Bookingdates.Checkin, "Check in date is not matching.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkout, postCreatedBooking.Booking.Bookingdates.Checkout, "Check out date is not matching.");
            Assert.AreEqual(updatedBookingData.Additionalneeds, postCreatedBooking.Booking.Additionalneeds, "Additional needs is not matching.");


            //Cleanup
            var deleteRequest = await BookingHelpers.DeleteBookingData(RestClient, postCreatedBooking.Bookingid);


        }
        [TestMethod]
        public async Task DeleteBookingData()
        {
            //Creating a new booking data
            var postResponse = await BookingHelpers.CreateNewBookingData(RestClient);
            var postCreatedBooking = postResponse.Data;

            //Act
            var deleteResponse = await BookingHelpers.DeleteBookingData(RestClient, postCreatedBooking.Bookingid);

            //Assertion
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "Status code is not equal to 201");

        }
        [TestMethod]
        public async Task GetInvalidBookingId()
        {
            //creation of invalid data and sending request
            var InvalidCode = "19191919199191919";
            var postResponse = await BookingHelpers.CreateNewBookingData(RestClient);
            var postCreatedBooking = postResponse.Data;

            //Act
            var getResponse = await BookingHelpers.GetBookById(RestClient, (long)Convert.ToDouble(InvalidCode));


            //Assertion
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "Status code is not equal to 404");

            //Cleanup
            var deleteRequest = await BookingHelpers.DeleteBookingData(RestClient, postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
        }
    }
}