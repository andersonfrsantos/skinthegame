using RestSharp;
using System;

namespace Api
{
    public class ApiFootball
    {

        public IRestResponse GetfixturesInPlay()
        {
            var client = new RestClient("https://api-football-v1.p.rapidapi.com/v2/fixtures/live?timezone=Europe%2FLondon");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "09d8e82c24msh1ec947362d3bd13p1d85bejsn2cd394f9e24a");
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            return response;

        }

        public IRestResponse GetFixturesStatistics(Int64 FixtureId)
        {
            var client = new RestClient("https://api-football-v1.p.rapidapi.com/v2/statistics/fixture/"+ FixtureId  + "/");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "09d8e82c24msh1ec947362d3bd13p1d85bejsn2cd394f9e24a");
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            return response;

        }



    }
}
