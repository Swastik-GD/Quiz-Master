using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int correctAnswer = 0;
    int questionSeen = 0;

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public void IncrementcorrectAnser()
    {
        correctAnswer++;
    }

    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void IncrementQuestionSeen()
    {
        questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswer / (float)questionSeen * 100);
    }
}
