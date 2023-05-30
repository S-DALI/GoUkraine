using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePanel : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    private void Start()
    {
        Panel.SetActive(false);
    }
    public void ActiveRestart()
    {
        Panel.SetActive(true);
        Time.timeScale = 0;
    }

}
