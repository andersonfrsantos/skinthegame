using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class FixturesStatistics
    {

        public class League
        {
            public string name { get; set; }
            public string country { get; set; }
            public string logo { get; set; }
            public string flag { get; set; }
        }

        public class HomeTeam
        {
            public int team_id { get; set; }
            public string team_name { get; set; }
            public string logo { get; set; }
        }

        public class AwayTeam
        {
            public int team_id { get; set; }
            public string team_name { get; set; }
            public string logo { get; set; }
        }

        public class Score
        {
            public string halftime { get; set; }
            public object fulltime { get; set; }
            public object extratime { get; set; }
            public object penalty { get; set; }
        }

        public class Event
        {
            public int elapsed { get; set; }
            public int? elapsed_plus { get; set; }
            public int team_id { get; set; }
            public string teamName { get; set; }
            public int? player_id { get; set; }
            public string player { get; set; }
            public int? assist_id { get; set; }
            public string assist { get; set; }
            public string type { get; set; }
            public string detail { get; set; }
            public object comments { get; set; }
        }

        public class Fixture
        {
            public int fixture_id { get; set; }
            public int league_id { get; set; }
            public League league { get; set; }
            public DateTime event_date { get; set; }
            public int event_timestamp { get; set; }
            public int firstHalfStart { get; set; }
            public int secondHalfStart { get; set; }
            public string round { get; set; }
            public string status { get; set; }
            public string statusShort { get; set; }
            public int elapsed { get; set; }
            public string venue { get; set; }
            public string referee { get; set; }
            public HomeTeam homeTeam { get; set; }
            public AwayTeam awayTeam { get; set; }
            public int goalsHomeTeam { get; set; }
            public int goalsAwayTeam { get; set; }
            public Score score { get; set; }
            public IList<Event> events { get; set; }
        }

        public class Api
        {
            public int results { get; set; }
            public IList<Fixture> fixtures { get; set; }
        }

        public class RootObject
        {
            public Api api { get; set; }
        }



    }
}
