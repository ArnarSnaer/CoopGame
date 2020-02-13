using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextLevel;
    public GameObject door;
    public bool player1ThroughDoor, player2ThroughDoor = false;
    public AudioSource clearSound;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(WaitThenQuit());
        }
    }
    IEnumerator WaitThenQuit()
    {
        // Quit Game After Timer to allow for Fade Out Effect
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void LevelClear()
    {
        //Play Lift Door Animation
        //Temporary Fix
        door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 1, door.transform.position.z);
        clearSound.Play();
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RedPlayer")
        {
            player1ThroughDoor = true;
        }
        else if (other.tag == "BluePlayer")
        {
            player2ThroughDoor = true;
        }
        if (player1ThroughDoor && player2ThroughDoor)
        {
            //Play Walkout Animation then Change Level Animation
            //Temporary Fix
            ChangeLevel();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "RedPlayer")
        {
            player1ThroughDoor = false;
        }
        else if (other.tag == "BluePlayer")
        {
            player2ThroughDoor = false;
        }
    }
}
