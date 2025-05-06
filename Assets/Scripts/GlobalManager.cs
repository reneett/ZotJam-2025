using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] public GameObject[] levels;

    // public Canvas inGameCanvas;
    // public Canvas bubbleCanvas;
    // public Canvas calendarCanvas;
    // public Canvas endScreen;

    private int currentLevel = 0;
    public float celebrationTime = 0.75f;
    private bool waiting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        if (playerScript.levelClear && currentLevel < levels.Length-1 && !waiting) {
            // reset player and scene and go to next level
            waiting = true;
            StartCoroutine(respawnAfterCelebrate(celebrationTime));
        } else if (playerScript.levelClear && currentLevel >= levels.Length-1) {
            // no more levels
            SceneManager.LoadScene("Ending Scene");
        }
    }

    IEnumerator respawnAfterCelebrate(float pauseTime) {
        playerScript.simulateBody(false);
        yield return new WaitForSeconds(pauseTime);
        Time.timeScale = 0; //pause
        player.SetActive(false);
        levels[currentLevel].SetActive(false);
        currentLevel++;
        levels[currentLevel].SetActive(true);
        player.SetActive(true);
        playerScript.simulateBody(true);
        Time.timeScale = 1;
        playerScript.respawn();
        playerScript.resetLevel();
        waiting = false;
    }

}
