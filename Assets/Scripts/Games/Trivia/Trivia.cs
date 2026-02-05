using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;

public class Trivia : Minigame
{
    private int correctAnswer = 1;
    protected override void OnStart()
    {
        StartCoroutine(GetFacts());
    }

    public override void GameStart()
    {
        base.GameStart();
        GameController.canvas.InstantiatePopText().SetValues(string.Empty);
        GameFinish(correctAnswer);
    }

    public override void GameFinish(int winner)
    {
        base.GameFinish(winner);
    }

    IEnumerator GetFacts()
    {
        string address = "https://opentdb.com/api.php?amount=1&difficulty=easy&type=multiple";

        using (UnityWebRequest api = UnityWebRequest.Get(address))
        {
            GameController.canvas.InstantiatePopText().SetValues("loading");
            yield return api.SendWebRequest();

            if (api.result == UnityWebRequest.Result.Success)
            {
                Root newFact = JsonUtility.FromJson<Root>(api.downloadHandler.text);
                string question = HttpUtility.HtmlDecode(newFact.results[0].question);
                GameController.canvas.InstantiateQuestionText().SetValues(question);

                correctAnswer = Random.Range(0, 4);
                List<string> options = new();
                for (int i = 0; i < 4; i++)
                {
                    if (i == correctAnswer) options.Add(newFact.results[0].correct_answer);
                    else options.Add(newFact.results[0].incorrect_answers[i - (i > correctAnswer ? 1 : 0)]);
                }
                correctAnswer++; // increment by one for bet buttons start at 1 but arrays start at 0
                GameController.canvas.InstantiateButtons(4, options.ToArray(), showButtonNumber);
            }
            else GameFail();
        }

        yield break;
    }
    [System.Serializable]
    public class Result
    {
        public string type;
        public string difficulty;
        public string category;
        public string question;
        public string correct_answer;
        public List<string> incorrect_answers = new();
    }
    [System.Serializable]
    public class Root
    {
        public int response_code;
        public List<Result> results = new();
    }
}