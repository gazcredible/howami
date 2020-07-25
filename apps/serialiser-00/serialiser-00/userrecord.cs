using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public enum UserResponse { worst=0, med_worst=1,med=2, med_good=3, good=4,unselected=-1};

public class UserData
{
    public enum Dimensions
    {
        Role,
        Demands,
        Support,
        Relationships,
        Control,
        Change
    };
}

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
            response = UserResponse.med;

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