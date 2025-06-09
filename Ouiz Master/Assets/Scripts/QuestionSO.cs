using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{

    [TextArea(2, 6)] 
    [SerializeField] string question = "Enter your Question here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int CorrectAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int Index)
    {
        return answers[Index];
    }

    public int GetCorrectAnswerIndex()
    {
        return CorrectAnswerIndex;
    }
}