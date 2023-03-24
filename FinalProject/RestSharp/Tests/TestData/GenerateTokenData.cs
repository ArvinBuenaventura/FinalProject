using RestSharpLibrary.DataModels;

namespace RestSharpLibrary.Tests.TestData
{
    public class GenerateTokenData
    {
        public static TokenModel generateToken()
        {
            return new TokenModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
