using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinShop : MonoBehaviour
{
    public SkinData[] SkinsList;
    private bool[] PurchasedSkins;

    [SerializeField] private Text AmountCoins;
    [SerializeField] private Text SkinPrice;
    [SerializeField] private Text ActivePanelText;
    [SerializeField] private Transform ShellSkins;
    [SerializeField] private Button ButtonBuy;
    [SerializeField] private Button ButtonActive;
    [SerializeField] private GameObject Buy;
    [SerializeField] private GameObject ActivePanel;

    public int SkinIndex;
    public int Coins;

    private void Awake()
    {
        SkinIndex = PlayerPrefs.GetInt("SelectedSkin");
        Coins = PlayerPrefs.GetInt("coins");
        AmountCoins.text = Coins.ToString();
        PurchasedSkins = new bool[SkinsList.Length];
        if (PlayerPrefs.HasKey("PurchasedArr"))
        {
            PurchasedSkins = PlayerPrefsX.GetBoolArray("PurchasedArr");
        }
        else
        {
            PurchasedSkins[0] = true;
        }

        SkinsList[SkinIndex].Selected = true;
        
        for(int i =0;i < SkinsList.Length;i++)
        {
            SkinsList[i].Bought = PurchasedSkins[i];
            if(i == SkinIndex)
            {
                ShellSkins.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                ShellSkins.GetChild(i).gameObject.SetActive(false);
            }
        }

        Buy.SetActive(false);
        ActivePanel.SetActive(true);
        ActivePanelText.text = "Activated";
        ButtonBuy.interactable = false;
        ButtonActive.interactable = false;
    }

    public void Save()
    {
        PlayerPrefsX.SetBoolArray("PurchasedArr", PurchasedSkins);
    }

    public void RightScroll()
    {
       if(SkinIndex<ShellSkins.childCount-1)
       {
            SkinIndex++;
            ScrollSkins();
       }
    }
    public void LeftScroll()
    {
        if(SkinIndex>0)
        {
            SkinIndex--;
            ScrollSkins();
        }
    }

    public void BuySkinsButton()
    {
        if(ButtonBuy.interactable && !SkinsList[SkinIndex].Bought)
        {
            if (Coins > int.Parse(SkinPrice.text))
            {
                Coins -= int.Parse(SkinPrice.text);
                AmountCoins.text = Coins.ToString();
                PlayerPrefs.SetInt("coins", Coins);
                PurchasedSkins[SkinIndex] = true;
                SkinsList[SkinIndex].Bought = true;
                Buy.SetActive(false);
                ActivePanel.SetActive(true);
                ActivePanelText.text = "Activate";
                ButtonActive.interactable = true;
                ButtonBuy.interactable = false;
                Save();
            }
        }

        if(ButtonActive.interactable&& !SkinsList[SkinIndex].Selected && SkinsList[SkinIndex].Bought)
        {
            PlayerPrefs.SetInt("SelectedSkin",SkinIndex);
            ButtonActive.interactable = false;
            SkinsList[SkinIndex].Selected= true;
            ActivePanelText.text = "Activated";
            for(int i = 0; i < ShellSkins.childCount - 1; i++)
            {
                if (SkinsList[SkinIndex].Selected && i!=SkinIndex)
                {
                    SkinsList[i].Selected = false;
                }
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private void ScrollSkins()
    {
        if (SkinsList[SkinIndex].Bought && SkinsList[SkinIndex].Selected)
        {
            Buy.SetActive(false);
            ActivePanel.SetActive(true);
            ActivePanelText.text = "Activated";
            ButtonBuy.interactable = false;
            ButtonActive.interactable = false;
        }
        else if (!SkinsList[SkinIndex].Bought)
        {
            Buy.SetActive(true);
            ActivePanel.SetActive(false);
            ButtonBuy.interactable = true;
            ButtonActive.interactable = false;
            SkinPrice.text = SkinsList[SkinIndex].Price.ToString();
        }
        else if (SkinsList[SkinIndex].Bought && !SkinsList[SkinIndex].Selected)
        {
            Buy.SetActive(false);
            ActivePanel.SetActive(true);
            ActivePanelText.text = "Activate";
            ButtonActive.interactable = true;
            ButtonBuy.interactable = false;
        }
        for (int i = 0; i < ShellSkins.childCount; i++)
        {
            ShellSkins.GetChild(i).gameObject.SetActive(false);
        }

        ShellSkins.GetChild(SkinIndex).gameObject.SetActive(true);
    }

}
 [System.Serializable] public class SkinData
 {
    public string Name;
    public int Price;
    public bool Bought;
    public bool Selected;
 }