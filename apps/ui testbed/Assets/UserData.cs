using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

public enum UserResponse { worst=0, med_worst=1,med=2, med_good=3, good=4,unselected=-1};

[Serializable]
public class UserRecord : IComparable<UserRecord>
{
    public UserRecord(DateTime date)
    {
        this.date = date;
        this.responses = new Dictionary<UserData.Dimensions, Response>();        
    }

    public String DumpText()
    {
        var str = "";

        foreach ( var kvp in this.responses)
        {
            str += kvp.Key + " " + kvp.Value.DumpText();
            str += "\n";
        }

        return str;
    }

    public int CompareTo(UserRecord obj)
    {
        if (obj == null) return 1;

        // CompareTo() method 
        return date.CompareTo( (obj as UserRecord).date);
    }

    public void AddResponse(UserData.Dimensions dimension, Response response)
    {
        this.responses.Add(dimension, response);
    }

    [Serializable]
    public class Response
    {
        public Response()
        {
            var v = Enum.GetValues(typeof(UserResponse));
            response = (UserResponse)v.GetValue(UserData.rand.Next(v.Length));
            //response = UserResponse.med;

            narrative = "";
        }

        public String DumpText()
        {
            return "response="+response + " narrative=" + narrative;
        }

        public UserResponse response;
        public String narrative;
    }

    public Dictionary<UserData.Dimensions, Response> responses;
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

[Serializable]
public class UserData
{
    public static System.Random rand = new System.Random(0);

    public Dictionary<DateTime, UserRecord> data;

    Int32 major = 0;
    Int32 minor = 0;
    public bool video_watched = false;

    private readonly Int32 magic_number = 0x11223344;
    
    public UserData()
    {
        data = new Dictionary<DateTime, UserRecord>();
    }

    public void Init()
    {
        //create some dummy data

        var date = new DateTime(2019, 1, 1, 8, 0, 0);

        data = new Dictionary<DateTime, UserRecord>();

        while ( date < DateTime.Now)
        {                 
            var record = new UserRecord(date);

            //foreach (var label in dimensionLabels)   
            foreach (UserData.Dimensions label in Enum.GetValues(typeof(Dimensions)))
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
          new HowamiQuestion("How am I with the control I have:", "In the last month, do I feel I have had enough say in how I do my work?"),
          new HowamiQuestion("How am I with the change:", "In the last month, have any changes in my work been well communicated with me?"),
    };

    public enum Dimensions
    {
        Role,
        Demands,
        Support,
        Relationships,
        Control,
        Change
    };

    public string[] dimensionLabels = { "ROLE", "DEMANDS", "SUPPORT", "RELATIONSHIPS", "CONTROL", "CHANGE" };

    public string GetDimensionLabel(int i)
    {
        return dimensionLabels[i];
    }

    public Dimensions GetDimensionEnum(int i)
    {
        if (i == 0) return Dimensions.Role;
        if (i == 1) return Dimensions.Demands;
        if (i == 2) return Dimensions.Support;
        if (i == 3) return Dimensions.Relationships;
        if (i == 4) return Dimensions.Control;
        if (i == 5) return Dimensions.Change;

        throw new Exception("out of range");
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

    public Dictionary<Dimensions, String[]> dimension_performance_lookup = new Dictionary<Dimensions, String[]>()
    {
        {Dimensions.Role,            new String[]{"Completely Unclear","Somewhat Clear", "Fairly Clear", "Clear", "Very Clear"} },
        {Dimensions.Demands,         new String[]{"Poor", "Demanding", "Ok","Good", "Very Good" } },
        {Dimensions.Support,         new String[]{"None", "Not enough", "Ok", "Good", "Very Good" } },
        {Dimensions.Relationships,   new String[]{"Very Poor", "Poor","Ok","Good","Very Good" } },
        {Dimensions.Control,         new String[]{"None", "Some","Ok","Good","Very Good" } },
        {Dimensions.Change,          new String[]{"No","Limited","Ok","Good","Very Good" } }
     };

    public string GetQuestionResponse(Dimensions dimensionLabel, UserResponse response)
    {
        return dimension_performance_lookup[dimensionLabel][(int)response];
    }
    public string GetQuestionResponse(UserRecord record, Dimensions dimensionLabel)
    {        
        return dimension_performance_lookup[dimensionLabel][(int)(record.responses[dimensionLabel].response)];
    }

    public string GetQuestionResponseNarrative(UserRecord record, Dimensions dimensionLabel)
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

    public KeyValuePair<UserResponse, int>[] SummariseData(List<HistoricData> historicResponses)
    {
        var lookup = new Dictionary<UserResponse, int>();

        foreach (var month in historicResponses)
        {
            foreach (var entry in month.data)
            {
                foreach (var response in entry.responses)
                {
                    if (lookup.ContainsKey(response.Value.response) == false)
                    {
                        lookup.Add(response.Value.response,1);
                    }
                    else
                    {
                        lookup[response.Value.response]++;
                    }
                }
            }
        }

        var instances = new int[5];
        var sum = 0;
        foreach (var kvp in lookup)
        {
            instances[(int) kvp.Key] = kvp.Value;

            sum += kvp.Value;
        }

        var percent = new KeyValuePair<UserResponse,int>[5];

        for (var i = 0; i < percent.Length; i++)
        {
            if (sum > 0)
            {
                percent[i] = new KeyValuePair<UserResponse, int>((UserResponse)i, (instances[i] * 100) / sum);
            }
            else
            {
                percent[i] = new KeyValuePair<UserResponse, int>((UserResponse)i, 0);
            }
        }

        Array.Sort(percent, new Comparison<KeyValuePair<UserResponse, int>>(
            (i1, i2) => i2.Value.CompareTo(i1.Value)));
        
        return percent;
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

    public void Save()
    {
        if (true)
        {
            var filename = Application.persistentDataPath + "/playerInfo.dat";
            
            if (File.Exists(filename) == true)
            {
                File.Delete(filename);
            }

            var file = File.Create(filename);
            
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file,this.magic_number);
                bf.Serialize(file,this.major);
                bf.Serialize(file,this.minor);
                bf.Serialize(file,this.video_watched);
                bf.Serialize(file, this.data);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("PersistentData - can't write to file:" + filename +"\n" +ex);
            }

            file.Close();
            Debug.LogWarning("PersistentData - written:" + filename);
        }

        if (true)
        {
            var filename = Application.persistentDataPath + "/drdata.txt";

            if (File.Exists(filename) == true)
            {
                File.Delete(filename);
            }
            var writer = File.CreateText(filename);

            try
            {
                foreach (var entry in this.data)
                {
                    writer.Write(entry.Key);
                    writer.Write(" ");
                    writer.Write(entry.Value.DumpText().Replace('\n',' '));                    
                    writer.Write(writer.NewLine);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("PersistentData - can't write to file:" + filename + " "+ ex.ToString());
            }

            writer.Close();
            Debug.LogWarning("PersistentData - written:"+filename);
        }
    }

    public void Load()
    {
        this.data = new Dictionary<DateTime, UserRecord>();

        var filename = Application.persistentDataPath + "/playerInfo.dat";

        if (File.Exists(filename) == true)
        {
            var stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            //try and read a version int
            //if we can't it's an old data format

            major = 0;
            minor = 0;

            try
            {
                var magic = (Int32) bf.Deserialize(stream);

                if (magic == this.magic_number)
                {
                    Debug.LogWarning("PersistentData - current format");
                    major = (Int32) bf.Deserialize(stream);
                    major = (Int32) bf.Deserialize(stream);
                    video_watched = (bool) bf.Deserialize(stream);

                    this.data = bf.Deserialize(stream) as Dictionary<DateTime, UserRecord>;
                    stream.Close(); 
                }
                else
                {
                    throw new System.Exception("old format");
                }
            }
            catch (Exception ex)
            {
                //old data format!
                Debug.LogWarning("PersistentData - old format");
                
                try
                {
                    this.video_watched = false;
                    stream.Seek(0, SeekOrigin.Begin);
                    this.data = bf.Deserialize(stream) as Dictionary<DateTime, UserRecord>;
                    stream.Close();
                    
                    Debug.LogWarning("PersistentData - old format LOADED");
                }
                catch (Exception e)
                {
                    this.data = new Dictionary<DateTime, UserRecord>();
                    
                    stream.Close();
                    this.video_watched = false;

                    Debug.LogWarning("PersistentData - old format FAILED TO LOAD");
                }
            }
        }

        if (this.data == null)
        {
            this.data = new Dictionary<DateTime, UserRecord>();
        }

        this.video_watched = false;
        
        Debug.LogWarning("Video watched: "+this.video_watched);
    }
};
