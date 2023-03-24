using SoapFinalProject;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace SoapApi
{
    [TestClass]
    public class SoapTests
    {

        //Global Variable
        private readonly SoapFinalProject.CountryInfoServiceSoapTypeClient SoapApi =
             new SoapFinalProject.CountryInfoServiceSoapTypeClient(SoapFinalProject.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        private List<tCountryCodeAndName> GetCountryCodeAndNameList()
        {
            return SoapApi.ListOfCountryNamesByCode();
        }

        [TestMethod]
        private static tCountryCodeAndName GetRandomRecord(List<tCountryCodeAndName> countryList)
        {
            Random rndm = new Random();
            int rndmInt = rndm.Next(0, countryList.Count);

            var randomCountry = countryList[rndmInt];

            return randomCountry;
        }

        [TestMethod]
        public void FirsTest()
        {
            var countryList = GetCountryCodeAndNameList();
            var randomCountry = GetRandomRecord(countryList);
            var randomCountryDetails = SoapApi.FullCountryInfo(randomCountry.sISOCode);

            Assert.AreEqual(randomCountryDetails.sISOCode, randomCountry.sISOCode);
            Assert.AreEqual(randomCountryDetails.sName, randomCountry.sName);
        }

        [TestMethod]
        public void SecondTest()
        {
            var countryList = GetCountryCodeAndNameList();
            var randomCountry = GetRandomRecord(countryList);

            var top5 = countryList.OrderBy(o => o.sISOCode).Take(5);

            foreach (var country in top5)
            {
                Assert.AreEqual(country.sISOCode, country.sISOCode);
            }
        }
    }
}