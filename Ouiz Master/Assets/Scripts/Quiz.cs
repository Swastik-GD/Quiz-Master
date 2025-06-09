using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class Quiz : MonoBehaviour
{

    [Header("questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answer")]
    [SerializeField] GameObject[] answerbuttons;
    int CorrectAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button")]
    [SerializeField] Sprite defaultanswersprite;
    [SerializeField] Sprite correctanswersprite;
    [SerializeField] Sprite wronganswersprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    
    void Awake()
    {
        timer = FindAnyObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextquestion();
            timer.loadNextQuestion = false;
        }

        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion && currentQuestion != null)
        {
            if (currentQuestion != null)
            {
                DisplayAnswer(-1);
                SetButtonsState(false);
            }
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonsState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        Image wrongbuttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerbuttons[index].GetComponent<Image>();
            buttonImage.sprite = correctanswersprite;
            scoreKeeper.IncrementcorrectAnser();
        }

        else if (index < 0) // Prevents out-of-range access
        {
            questionText.text = "Time's Up!";
            CorrectAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            buttonImage = answerbuttons[CorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctanswersprite;
        }

        else
        {
            CorrectAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Wrong Answer!";
            buttonImage = answerbuttons[CorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctanswersprite;
            wrongbuttonImage = answerbuttons[index].GetComponent<Image>();
            wrongbuttonImage.sprite = wronganswersprite;
        }
    }


    void GetNextquestion()
    {
        if (questions.Count > 0)
        {
            SetButtonsState(true);
            SetDefaultSpriteButton();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion()
    {

        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerbuttons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerbuttons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonsState(bool state)
    {
        for(int i =  0; i < answerbuttons.Length; i++)
        {
            Button button = answerbuttons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultSpriteButton()
    {
        for (int i = 0; i < answerbuttons.Length; i++)
        {
            Image buttonImage = answerbuttons[i].GetComponent<Image>();
            buttonImage.sprite = defaultanswersprite;
        }
    }

}
