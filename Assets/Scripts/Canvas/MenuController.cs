using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Image blackBG;
    public GameObject difficultyPanel;
    public GameObject mainMenu;
    public GameObject credit;
    public GameObject help;

    public void DisplayDifficulties()
    {
        mainMenu.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void HideDifficulties()
    {
        mainMenu.SetActive(true);
        difficultyPanel.SetActive(false);
    }

    public void StartNewGame(int difficulty)
    {
        PlayerPrefs.SetInt("dif", difficulty);
        mainMenu.SetActive(false);
        difficultyPanel.SetActive(false);
        blackBG.DOFade(1f, 1f).OnComplete(() => LoadGameplayScene());
    }

    private void LoadGameplayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowCredits()
    {
        credit.SetActive(true);
    }

    public void HideCredits()
    {
        credit.SetActive(false);
    }

    public void ShowHelp()
    {
        help.SetActive(true);
    }

    public void HideHelp()
    {
        help.SetActive(false);
    }
}
