using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] public Text ScoreText;

    private void Update()
    {
        ScoreText.text = ((int)(Player.transform.position.z/3)).ToString();
    }
}
