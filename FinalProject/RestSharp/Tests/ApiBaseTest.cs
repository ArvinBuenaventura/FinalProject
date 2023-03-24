using RestSharp;
using RestSharpLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLibrary.Tests
{
    public class ApiBaseTest
    {
        public RestClient RestClient { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            RestClient = new RestClient();

        }

    }
}

