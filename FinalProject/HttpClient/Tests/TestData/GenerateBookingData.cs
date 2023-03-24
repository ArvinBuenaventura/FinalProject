using HttpClientLibrary.DataModels;

namespace HttpClientLibrary.Tests.TestData
{
    public class GenerateBookingData
    {
        public static Booking demoBooking()
        {
            return new Booking
            {
                Firstname = "Milkty",
                Lastname = "Cutie",
                Totalprice = 116565,
                Depositpaid = true,
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Parse("2023-03-23"),
                    Checkout = DateTime.Parse("2023-03-23")
                },
                Additionalneeds = "Spoon"
            };
        }
    }
}
