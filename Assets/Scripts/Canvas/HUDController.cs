using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image sanityFill;
    [SerializeField] private Image hpFill;
    [SerializeField] private TextMeshProUGUI roomIndicatorText;
    [SerializeField] private TextMeshProUGUI instruction;
    [SerializeField] private GameObject instructionObject;

    [SerializeField] private LevelManager levelManager;
    private GameManager gameManager;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.Instance;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSanityUI();
        UpdateHPUI();
        UpdateRoomUI();
    }

    private void UpdateSanityUI()
    {
        sanityFill.fillAmount = playerStats.sanity / playerStats.maxSanity;
    }

    private void UpdateHPUI()
    {
        hpFill.fillAmount = (float)playerStats.hp / (float)playerStats.maxHp;
    }

    private void UpdateRoomUI()
    {
        roomIndicatorText.text = "Room " + (levelManager.currentRoomIndex + 1) + " of " + levelManager.roomsSpawned.Count;
        if (gameManager.currentGameState == GameState.Battling ||
            gameManager.currentGameState == GameState.Transitioning ||
            gameManager.currentGameState == GameState.Killed ||
            gameManager.currentGameState == GameState.Unconcious ||
            gameManager.currentGameState == GameState.Win ||
            gameManager.currentGameState == GameState.Pausing)
        {
            instructionObject.SetActive(false);
        }
        else
        {
            if (levelManager.currentRoom.cleared)
            {
                instruction.text = "Room cleared.\n";
            }
            else if (levelManager.currentRoom.isCorrupted)
            {
                instruction.text = "You feel an eerie presence...\n";
            }
            else
            {
                instruction.text = "Battle is coming.\n";
            }
            instruction.text += "Press SPACE to proceed";
            instructionObject.SetActive(true);
        }
    }
}
