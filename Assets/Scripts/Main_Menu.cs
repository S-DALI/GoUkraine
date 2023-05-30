using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] private Text Coins;
    [SerializeField] private Text TopScoreText;

    private void Start()
    {
        int _coins = PlayerPrefs.GetInt("coins");
        Coins.text = _coins.ToString();
        int LastScore = PlayerPrefs.GetInt("LastScore");
        int TopScore = PlayerPrefs.GetInt("TopScore");

        if (LastScore>TopScore)
        {
            TopScore= LastScore;
            PlayerPrefs.SetInt("TopScore", TopScore);
            TopScoreText.text = TopScore.ToString();
        }
        else
        TopScoreText.text = TopScore.ToString();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartSkinMenu()
    {
        SceneManager.LoadScene(2);
    }
}
