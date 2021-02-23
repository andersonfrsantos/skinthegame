using Newtonsoft.Json;

namespace Model
{
    public class JsonToModelConverter
    {

        public void ModeloFixtureInPlay(string pResponseContent) 
        {

            FixturesInPlay.RootObject _FixturesInPlay = new FixturesInPlay.RootObject();
            _FixturesInPlay = JsonConvert.DeserializeObject<FixturesInPlay.RootObject>(pResponseContent);

        }

        public void ModeloFixtureStatistics(string pResponseContent)
        {

            FixturesStatistics.RootObject _FixturesStatistics = new FixturesStatistics.RootObject();
            _FixturesStatistics = JsonConvert.DeserializeObject<FixturesStatistics.RootObject>(pResponseContent);

        }


    }
}
