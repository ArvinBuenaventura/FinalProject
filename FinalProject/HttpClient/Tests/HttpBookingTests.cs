using HttpClientLibrary.DataModels;
using Newtonsoft.Json;
using System.Net;
using HttpClientLibrary.Helpers;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HttpClientLibrary.Tests
{
    [TestClass]
    public class BookingTests
    {
        [TestMethod]
        public async Task CreateNewBookingDetails()
        {
            //Creating a new booking data
            var postResponse = await BookingHelpers.CreateNewBookingData();

            //Act
            var postCreatedBooking = JsonConvert.DeserializeObject<BookingModels>(postResponse.Content.ReadAsStringAsync().Result);
            var getResponse = await BookingHelpers.GetBookingDataById(postCreatedBooking.Bookingid);

            //Check if getResponse is not null
            Assert.IsNotNull(getResponse);

            var getCreatedBooking = JsonConvert.DeserializeObject<Booking>(getResponse.Content.ReadAsStringAsync().Result);

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(getCreatedBooking.Firstname, postCreatedBooking.Booking.Firstname, "First Name is not matching.");
            Assert.AreEqual(getCreatedBooking.Lastname, postCreatedBooking.Booking.Lastname, "Last Name is not matching.");
            Assert.AreEqual(getCreatedBooking.Totalprice, postCreatedBooking.Booking.Totalprice, "Total price is not matching.");
            Assert.AreEqual(getCreatedBooking.Depositpaid, postCreatedBooking.Booking.Depositpaid, "Deposit paid is not matching.");
            Assert.AreEqual(getCreatedBooking.Bookingdates.Checkin, postCreatedBooking.Booking.Bookingdates.Checkin, "Check in date is not matching.");
            Assert.AreEqual(getCreatedBooking.Bookingdates.Checkout, postCreatedBooking.Booking.Bookingdates.Checkout, "Check out date is not matching.");
            Assert.AreEqual(getCreatedBooking.Additionalneeds, postCreatedBooking.Booking.Additionalneeds, "Additional needs is not matching.");

            //Cleanup
            var deleteRequest = await BookingHelpers.DeleteBookingDataById(postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");
        }

        [TestMethod]
        public async Task UpdateFirstAndLastNames()
        {
            //creating object and sending put request
            var postResponse = await BookingHelpers.CreateNewBookingData();
            var postCreatedBooking = JsonConvert.DeserializeObject<BookingModels>(postResponse.Content.ReadAsStringAsync().Result);

            //booking object initialize
            Booking bookingUpdate = new Booking()
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
            var putResponse = await BookingHelpers.UpdateBookingDataById(postCreatedBooking.Bookingid, bookingUpdate);
            var updatedBookingData = JsonConvert.DeserializeObject<Booking>(putResponse.Content.ReadAsStringAsync().Result);

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(updatedBookingData.Firstname, bookingUpdate.Firstname, "First Name is not matching.");
            Assert.AreEqual(updatedBookingData.Lastname, bookingUpdate.Lastname, "Last Name is not matching.");
            Assert.AreEqual(updatedBookingData.Totalprice, bookingUpdate.Totalprice, "Total price is not matching.");
            Assert.AreEqual(updatedBookingData.Depositpaid, bookingUpdate.Depositpaid, "Deposit paid is not matching.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkin, postCreatedBooking.Booking.Bookingdates.Checkin, "Check in date is not matching.");
            Assert.AreEqual(updatedBookingData.Bookingdates.Checkout, postCreatedBooking.Booking.Bookingdates.Checkout, "Check out date is not matching.");
            Assert.AreEqual(updatedBookingData.Additionalneeds, bookingUpdate.Additionalneeds, "Additional needs is not matching.");

            //Cleanup
            var deleteRequest = await BookingHelpers.DeleteBookingDataById(postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");

        }

        [TestMethod]
        public async Task DeleteBookingData()
        {
            //Creating a new booking data
            var postResponse = await BookingHelpers.CreateNewBookingData();
            var postCreatedBooking = JsonConvert.DeserializeObject<BookingModels>(postResponse.Content.ReadAsStringAsync().Result);

            //Act
            var deleteResponse = await BookingHelpers.DeleteBookingDataById(postCreatedBooking.Bookingid);

            //Assertion
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "Status code is not equal to 201");

        }

        [TestMethod]
        public async Task GetInvalidBookingId()
        {
            //creation of invalid data and sending request
            var InvalidCode = "19191919199191919";
            var postResponse = await BookingHelpers.CreateNewBookingData();
            var postCreatedBooking = JsonConvert.DeserializeObject<BookingModels>(postResponse.Content.ReadAsStringAsync().Result);

            //Act
            var getResponse = await BookingHelpers.GetBookingDataById((long)Convert.ToDouble(InvalidCode));

            //Assertion
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "Status code is not equal to 404");

            //Cleanup
            var deleteRequest = await BookingHelpers.DeleteBookingDataById(postCreatedBooking.Bookingid);
            Assert.AreEqual(HttpStatusCode.Created, deleteRequest.StatusCode, "Status code is not equal to 201");

        }
    }
}