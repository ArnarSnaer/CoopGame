using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Collider2D canBeInteractedBy;
    public GameObject otherInteractable;
    public Sprite interactedSprite;
    public Sprite deInteractedSprite;
    public bool interactedWith = false;
    public bool freezeInteractedWith = false;
    public GameObject gameManager;
    public AudioSource buttonPress;
    private bool playSound = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        // If other is player X then true
        if (other == canBeInteractedBy)
        {
            interactedWith = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = interactedSprite;
            
            if(playSound == true)
            buttonPress.Play();

            if (otherInteractable.GetComponent<Interactable>().interactedWith == true)
            {
                ClearLevel();
                otherInteractable.GetComponent<Interactable>().ClearLevel();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // if other is player x and the other player has not cleared the other interactable object then false
        if (other == canBeInteractedBy && freezeInteractedWith == false)
        {
            interactedWith = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = deInteractedSprite;
        }
    }

    public void ClearLevel()
    {
        // If both players are on their interactable objects then the level is clear and they can move on
        playSound = false;
        interactedWith = true;
        freezeInteractedWith = true;
        gameManager.GetComponent<GameManager>().LevelClear();
    }
}