using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using Xero.Api.Core.Model;

namespace EmployeeRestSharpTestCase
{
    [TestClass]
    public class UnitTest1
        {
            RestClient client;

            [TestInitialize]
            public void Setup()
            {
                client = new RestClient("http://localhost:3000");
            }

            private IRestResponse getEmployeeList()
            {
                RestRequest request = new RestRequest("/employees", Method.GET);

                //act

                IRestResponse response = client.Execute(request);
                return response;
            }

            [TestMethod]
            public void onCallingGETApi_ReturnEmployeeList()
            {
                IRestResponse response = getEmployeeList();

                //assert
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
                List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                Assert.AreEqual(11, dataResponse.Count);
                foreach (var item in dataResponse)
                {
                    System.Console.WriteLine("id:"+ item.id + "Name:"+ item.Name + "Salary: " + item.Salary);
                }
            }


            [TestMethod]
            public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
            {
                RestRequest request = new RestRequest("/employees", Method.POST);
                JObject jObjectbody = new JObject();
                jObjectbody.Add("name", "Clark");
                jObjectbody.Add("Salary", "15000");
                request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

                //Act
                IRestResponse response = client.Execute(request);
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
                 //Assert
                Assert.AreEqual("Clark", dataResponse.LastName);
                Assert.AreEqual(15000,dataResponse.Salary);

            }

        }
    
}

