using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    EndScreen endscreen;

    void Awake()
    {
        quiz = FindAnyObjectByType<Quiz>();
        endscreen = FindAnyObjectByType<EndScreen>();
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endscreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endscreen.gameObject.SetActive(true);
            endscreen.ShowFinalScore();
        }
    }

    public void OnReplaylevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
