using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] AllSkins;
    [SerializeField] private Transform ShellSkins;
    private void Awake()
    {
        for (int i = 0; i < ShellSkins.childCount; i++)
        {
            if (i == PlayerPrefs.GetInt("SelectedSkin"))
            {
                AllSkins[i].SetActive(true);
            }
            else AllSkins[i].SetActive(false);
        }
    }
}
