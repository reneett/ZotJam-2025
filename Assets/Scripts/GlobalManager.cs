using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{

    [SerializeField] private PlayerMovement player;
    [SerializeField] public GameObject[] levels;

    // public Canvas inGameCanvas;
    // public Canvas bubbleCanvas;
    // public Canvas calendarCanvas;
    // public Canvas endScreen;

    private int currentLevel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if (player.levelClear && currentLevel < levels.Length-1) {
            // reset player and scene and go to next level
            Time.timeScale = 0; //pause
            levels[currentLevel].SetActive(false);
            currentLevel++;
            levels[currentLevel].SetActive(true);
            Time.timeScale = 1;
            player.respawn();
            player.resetLevel();
        } else if (player.levelClear && currentLevel == levels.Length-1) {
            // no more levels
            SceneManager.LoadScene(2);
        }
    }

}
