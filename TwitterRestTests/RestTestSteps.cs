using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace TwitterRestTests
{
    [Binding]
    public class RestTestSteps
    {
        private string url;
        private string content;
        private WebClient wc = new WebClient();
        JObject response;
        HttpStatusCode httpStatus;

        [Given(@"I access the resource url ""(.*)""")]
        public void GivenIAccessTheResourceUrl(string resourceUrl)
        {
            this.url = "https://api.twitter.com" + resourceUrl;
        }

        [When(@"I retrieve the results")]
        public void WhenRetrieveTheResults()
        {
            try
            {
                this.content = wc.DownloadString(url);
                this.httpStatus = HttpStatusCode.OK;
            }
            catch (WebException we)
            {
                this.httpStatus = ((HttpWebResponse)we.Response).StatusCode;
            }
            if (this.httpStatus.Equals(HttpStatusCode.OK))
            {
                Assert.IsNotNullOrEmpty(this.content);
                this.response = (JObject)JToken.Parse(this.content);
            }
        }

        [Then(@"the status code should be (.*)")]
        public void ThenTheStatusCodeShouldBe(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)this.httpStatus);
        }

        [Then(@"it should have the field ""(.*)"" containing the value ""(.*)""")]
        public void ThenItShouldContainTheFieldContainingTheValue(string field, string value)
        {
            if (response != null)
            {
                JValue val = (JValue)this.response.GetValue(field);
                string valStr = val.Value<string>().Trim();
                Assert.IsNotNull(valStr);
                Assert.AreEqual(valStr, value.Trim());
            }
        }

        [Then(@"it should have the field ""(.*)"" containing the value (.*)")]
        public void ThenItShouldContainTheFieldContainingTheValue(string field, int value)
        {
            if (response != null)
            {
                JValue val = (JValue)this.response.GetValue(field);

                int valInt = val.Value<Int32>();
                Assert.AreEqual(valInt, value);
            }
        }
    }
}
