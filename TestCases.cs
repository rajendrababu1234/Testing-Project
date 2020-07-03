using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Configuration;
using Optivum_APITESTING_FRAMEWORK.Models;
using Optivum_APITESTING_FRAMEWORK.Utilities.Models;
using Optivum_APITESTING_FRAMEWORK.Utilities.Helper;

namespace Optivum_APITESTING_FRAMEWORK.TestCases
{
    [TestFixture]
    public class TestCases
    {
        private string baseURL=ConfigurationManager.AppSettings["BaseUrl"].ToString();

        [TestCase]
        public async Task GetTestCase()
        {
            await Task.Run(() =>
            {
                try
                {
                    RequestModel model = new RequestModel();
                    model.endPoint = baseURL + "employees";
                    model.contentType = "application/json";
                    model.method = "GET";
                    var response = WebAPICall.GetRequest(model).GetResponse() as HttpWebResponse;
                    EmployeeModel Result = JsonConvert.DeserializeObject<EmployeeModel>(WebAPICall.UnPack(response));
                    Assert.AreEqual("success", Result.status);
                }
                catch
                {
                    Assert.IsTrue(false);
                }
            });
        }

        [TestCase]
        public async Task PostTestCase()
        {
            await Task.Run(() =>
            {
                try
                {
                    RequestModel model = new RequestModel();
                    model.endPoint = baseURL + "create";
                    model.contentType = "application/json";
                    model.method = "POST";

                    #region PostData
                    Data createData = new Data();
                    createData.age = "22";
                    createData.name = "Testing";
                    createData.salary = "1000";
                    #endregion

                    #region Data Serialization
                    var data = JsonConvert.SerializeObject(createData);
                    model.content = data;
                    #endregion

                    var response = WebAPICall.GetRequest(model).GetResponse() as HttpWebResponse;
                    CreateResponseModel Result = JsonConvert.DeserializeObject<CreateResponseModel>(WebAPICall.UnPack(response));
                    Assert.AreEqual("success", Result.status);
                }
                catch
                {
                    Assert.IsTrue(false);
                }
            });
        }
    }
}
