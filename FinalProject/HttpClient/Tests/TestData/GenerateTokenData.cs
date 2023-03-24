using HttpClientLibrary.DataModels;

namespace HttpClientLibrary.Tests.TestData
{
    public class GenerateTokenData
    {
        public static TokenModels generateToken()
        {
            return new TokenModels
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
