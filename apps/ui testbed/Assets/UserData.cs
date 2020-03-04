using System;
using System.Collections.Generic;
using System.Threading;

public enum UserResponse { worst=0, med_worst=1,med=2, med_good=3, good=4 };


public class UserRecord : IComparable<UserRecord>
{
    public UserRecord(DateTime date)
    {
        this.date = date;
        this.responses = new Dictionary<string, Response>();        
    }

    public int CompareTo(UserRecord obj)
    {
        if (obj == null) return 1;

        // CompareTo() method 
        return date.CompareTo( (obj as UserRecord).date);
    }

    public void AddResponse(String dimension, Response response)
    {
        this.responses.Add(dimension, response);
    }

    public class Response
    {
        public Response()
        {
            var v = Enum.GetValues(typeof(UserResponse));
            response = (UserResponse)v.GetValue(UserData.rand.Next(v.Length));
            //response = UserResponse.med;

            narrative = "";
        }

        public UserResponse response;
        public String narrative;
    }

    public Dictionary<String, Response> responses;
    public DateTime date;

    public override string ToString()
    {
        return date.ToString();
    }
};


public class HowamiQuestion
{
    public string title;
    public string detail;

    public HowamiQuestion(string title, string detail)
    {
        this.title = title;
        this.detail = detail;
    }
}


public class UserData
{
    public static Random rand = new Random();

    public Dictionary<DateTime, UserRecord> data;

    public UserData()
    {

        data = new Dictionary<DateTime, UserRecord>();
    }

    public void Init()
    {
        //create some dummy data

        var date = new DateTime(2019, 1, 1, 8, 0, 0);


        while( date < DateTime.Now)
        {                 
            var record = new UserRecord(date);

            foreach (var label in dimensionLabels)
            {

                var res = new UserRecord.Response();

                res.narrative = date.ToString() + "\n" + label + "\n" + "Here's my narrative";
                record.AddResponse(label, res);
            }

            data.Add(record.date, record);

            date = date.AddDays(UserData.rand.Next(2, 4));

            date = date.AddHours(UserData.rand.Next(3, 14));
        }
    }

    public List<UserRecord> GetCurrentRespones(DateTime time)
    {        
        var results = new List<UserRecord>();

        foreach (var kvp in data)
        {
            if( (kvp.Key.Month == time.Month)
              &&(kvp.Key.Year == time.Year)
              )
            {
                results.Add(kvp.Value);
            }
        }

        if (results.Count > 0)
        {
            results.Sort();
        }

        return results;

    }

    public HowamiQuestion[] questions =
    {
          new HowamiQuestion("How am I in my role:", "In the last month, have I been clear about my role in the job?"),
          new HowamiQuestion("How am I with the demands of life:", "In the last month, how demanding has my work and personal life been?"),
          new HowamiQuestion("How am I with the support I receive:", "In the last month, how much support have I received in work and personal life?"),
          new HowamiQuestion("How am I with my relationships:", "In the last month, have the relationships I have at work been positive?"),
          new HowamiQuestion("How am I with the control I have:", "In the last month, do I feel I have had enough sat in how I do my work?"),
          new HowamiQuestion("How am I with the change:", "In the last month, have any changes in my work been well communicated with me?"),
    };

    public string[] dimensionLabels = { "ROLE", "DEMANDS", "SUPPORT", "RELATIONSHIPS", "CONTROL", "CHANGE" };

    public string GetDimensionLabel(int i)
    {
        return dimensionLabels[i];
    }

    public String[] monthlyFeedback =
    {
        //0 - worst
        "worst - everything is terrible",
        //1 - med worst
        "med worst - everything is fairly terrible",
        //2 - med
        "med  - everything is ok",
        //3 - med good
        "med good  - everything is better than ok",
        //4 - good
        "good  - everything is good",
    };

    public Dictionary<String, String[]> dimension_performance_lookup = new Dictionary<String, String[]>()
    {
        {"ROLE",            new String[]{"role_worst", "role_med_worst", "role_med", "role_med_good", "role_good" } },
        {"DEMANDS",         new String[]{"demands_worst", "demands_med_worst", "demands_med", "demands_med_good", "demands_good" } },
        {"SUPPORT",         new String[]{"support_worst", "support_med_worst", "support_med", "support_med_good", "support_good" } },
        {"RELATIONSHIPS",   new String[]{"rel_worst", "rel_med_worst", "rel_med", "rel_med_good", "rel_good" } },
        {"CONTROL",         new String[]{"control_worst", "control_med_worst", "control_med", "control_med_good", "control_good" } },
        {"CHANGE",          new String[]{"change_worst", "change_med_worst", "change_med", "change_med_good", "change_good" } }, 
     };

    public string GetQuestionResponse(UserRecord record, String dimensionLabel)
    {
        return dimension_performance_lookup[dimensionLabel][(int)(record.responses[dimensionLabel].response)];
    }

    public string GetQuestionResponseNarrative(UserRecord record, String dimensionLabel)
    {
        if (record.responses[dimensionLabel].narrative.Length > 0)
        {
            return record.responses[dimensionLabel].narrative;
        }

        return "No narrative entered";
    }

    public class HistoricData
    {
        public HistoricData()
        {
            time = DateTime.Now;
            data = new List<UserRecord>();
        }

        public DateTime time;

        public List<UserRecord> data;

        public int GetScore()
        {
            if (data.Count == 0)
            {
                return 0;
            }

            var score = 0;
            var count = 0;
            foreach (var item in data)
            {
                foreach (var kvp in item.responses)
                {
                    score += (int)kvp.Value.response;

                    count++;
                }
            }

            score = (int)((score / (float)count) + 0.5f);

            return score;
        }
    };

    public List<HistoricData> GetHistoricData(DateTime fromHere, int months=6)
    {
        var testDate = fromHere;

        var result = new List<HistoricData>();
        
        for (var month = 0; month < months; month++)
        {
            var currentData = new HistoricData();
            currentData.time = testDate;
            result.Add(currentData);
            
            foreach (var kvp in data)
            {
                if ((kvp.Key.Year == testDate.Year) && (kvp.Key.Month == testDate.Month))
                {
                    currentData.data.Add(kvp.Value);
                }
            }
            
            currentData.data.Sort();
            
            testDate = testDate.AddMonths(-1);
        }

        return result;
    }

    public int GetScoreForMonth(DateTime time)
    {
        var data = GetCurrentRespones(time);
        var score = 0;
        var count = 0;
        foreach (var item in data)
        {
            foreach (var kvp in item.responses)
            {
                score += (int) kvp.Value.response;

                count++;
            }
        }

        return (int) ((score / (float) count) + 0.5f);
    }

    public String GetMonthlyFeedback(DateTime time)
    {
        var data = GetCurrentRespones(time);

        if (data.Count == 0)
        {
            return "No entries for this month - why are we here?";
        }

        var score = GetMonthlyFeedbackAsInt(time);

        return "score is: " + score +"\n" + monthlyFeedback[score];
    }

    public int GetMonthlyFeedbackAsInt(DateTime time)
    {
        var data = GetCurrentRespones(time);

        if (data.Count == 0)
        {
            return 0;
        }

        var score = 0;
        var count = 0;
        foreach (var item in data)
        {
            foreach (var kvp in item.responses)
            {
                score += (int)kvp.Value.response;

                count++;
            }
        }

        score = (int)((score / (float)count) + 0.5f);

        return score;
    }

    public int GetFeedbackAsInt(DateTime time)
    {
        var data = GetCurrentRespones(time);

        if (data.Count == 0)
        {
            return 0;
        }

        var score = 0;
        var count = 0;
        foreach (var item in data)
        {
            foreach (var kvp in item.responses)
            {
                score += (int)kvp.Value.response;

                count++;
            }
        }

        score = (int)((score / (float)count) + 0.5f);

        return score;
    }

    public int GetHistoricFeedbackAsInt(List<UserData.HistoricData> data)
    {        
        if (data.Count == 0)
        {
            return 0;
        }

        var score = 0;
        var count = 0;
        foreach (var item in data)
        {
            score += item.GetScore();

            count++;
        }

        score = (int)((score / (float)count) + 0.5f);

        return score;
    }
};
