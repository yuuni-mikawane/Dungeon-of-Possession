using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Image blackBG;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI gameWinText;
    private GameManager gameManager;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0.0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
    }

    public void FadeInBlackBG(float duration)
    {
        blackBG.DOFade(1f, duration);
    }

    public void FadeOutBlackBG(float duration)
    {
        blackBG.DOFade(0f, duration);
    }

    public void DisplayWinScreen()
    {
        blackBG.DOFade(1f, 0.5f).OnComplete(() => DisplayWinContent());
    }

    public void DisplayWinContent()
    {
        //win screen
        gameWinPanel.SetActive(true);
        gameWinText.text = "Difficulty: " + gameManager.currentDifficulty.name;
        gameWinText.text += "\nRooms cleared: " + gameManager.currentDifficulty.floorCount.ToString();
        gameWinText.text += "\nCasualties: " + gameManager.deathCount.ToString();
        gameWinText.text += "\nTime: " + gameManager.timer.ToString("F3") + "s";
    }
}
