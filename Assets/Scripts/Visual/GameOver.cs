using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    CanvasGroup cg;
    [SerializeField]
    TextMeshProUGUI text;
    bool gameOver = false;

    private void Start()
    {
        GameEvents.instance.OnGameOver += GameOverScreen;
    }
    void GameOverScreen(string reason)
    {
        cg.LeanAlpha(1,3);
        if(PlayerPrefs.GetInt("Highscore") < GameManager.instance.day+1)
        {
            PlayerPrefs.SetInt("Highscore", GameManager.instance.day+1);
        }
        gameOver = true;
        string highscore = PlayerPrefs.GetInt("Highscore").ToString() + " days";
        text.text = string.Format($"You died on day {GameManager.instance.day+1} from {reason} \n Press [SPACE] to restart \n Your highscore is {highscore}");
    }
    private void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
