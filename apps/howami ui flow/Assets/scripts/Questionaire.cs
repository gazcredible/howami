using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Question
{
    public string question;
    public string green;
    public string amber;
    public string red;

    public Question(String question, String green, String amber, String red)
    {
        this.question = question;
        this.green = green;
        this.amber = amber;
        this.red = red;
    }
}

public class Response
{
    public enum Value { NoResponse, Red, Amber, Green };
    public Value value;
    public String response;

    public Response()
    {
        value = Value.NoResponse;
        response = "";
    }
}

public class Questionaire : Library.Singleton<Questionaire>
{
    public Question[] questions;
    public Response[] responses;

    public int currentQuestion;

    public void OnStartUp()
    {
        questions = new Question[6];

        questions[0] = new Question("Role<br> In the last month, have you been clear about your role in the job?",
                                    "Very clear",
                                    "Quite clear",
                                    "Not at all clear");

        questions[1] = new Question("Demand<br> In the last month, in your work and personal life, how demanding has your situation been?",
                                    "Comfortable demands",
                                    "Moderately demanding",
                                    "Excessively demanding");

        questions[2] = new Question("Support<br> In the last month, how much support have you received in your work and personal life?",
                                     "Good support",
                                     "Okay support",
                                     "Poor support");

        questions[3] = new Question("Relationships<br> Have the relationships you have at work been positive?",
                                    "Very good",
                                    "Quite good",
                                    "Poor");

        questions[4] = new Question("Control<br> In the last month, do you feel you have had enough say in how you do your work?",
                                    "A good amount of say",
                                    "A fair amount of say",
                                    "Not enough say");

        questions[5] = new Question("Change<br> In the last month, have any changes to your work been well communicated to you?",
                                    "Good communication of any change",
                                    "Okay communication of any change",
                                    "Poor communication of change");

        responses = new Response[6];
        for (var i = 0; i < responses.Length; i++)
        {
            responses[i] = new Response();
        }
    }

    public void OnEnterQuestionaire()
    {
        currentQuestion = 0;
    }
}