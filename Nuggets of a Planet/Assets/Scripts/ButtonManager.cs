using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private bool buttonDeactivated;
    private GameObject dialogueBox;
    private Animator animator;
    private DialogueManager dialogueManager;
    private int sentence;

    private bool dialogueEnd;

    public GameObject bag;

    // private GameObject button;

    float timeLeft = 5f;

    public GameObject dialogueManagerObj;
    private PickUp pickup;
    public GameObject player;
    public GameObject island;
    private IslandBehaviour islandBehaviour;

    private KeyCode CKey = KeyCode.C;
    private KeyCode PKey = KeyCode.P;
    //private KeyCode MKey = KeyCode.M;

    bool keyPressed = false;
    private GameObject[] highlights;


    // Start is called before the first frame update
    void Start()
    {
        buttonDeactivated = false;
        dialogueBox = this.transform.parent.gameObject;
        animator = dialogueBox.GetComponent<Animator>();
        dialogueManager = dialogueManagerObj.GetComponent<DialogueManager>();

        pickup = player.GetComponent<PickUp>();
        islandBehaviour = island.GetComponent<IslandBehaviour>();
        highlights = GameObject.FindGameObjectsWithTag("Highlight");
        dialogueEnd = islandBehaviour.isHumanUp();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        if(animator.GetBool("isOpen") && !dialogueEnd) {
        sentence = dialogueManager.dialogueIndex();
        //if(buttonDeactivated) deactivateButton();

        if (sentence == 9) {
    
            timeLeft -= Time.deltaTime;
            if ( timeLeft < 0 ) {
                buttonDeactivated = false;
                timeLeft = 0;
            }
            else buttonDeactivated = true;
        }
        else if(sentence == 8) {
            buttonDeactivated = true;
            reactivateButton(CKey);
        }  else if(sentence == 7) {
            buttonDeactivated = true;
            if(pickup.rightElementPickedUp()) {
                reactivateButton(PKey);
                dialogueManager.DisplayNextSentence();
                buttonDeactivated = false;
            }
        } else if (sentence == 6) {
            bag.GetComponent<Pulse>().enabled = true;
        }
        else if(sentence == 5) {
            bag.GetComponent<Pulse>().enabled = false;
            buttonDeactivated = true;
            // GameObject.SetActive(false);
            if(islandBehaviour.rightElementToMix()) {
                dialogueManager.DisplayNextSentence();
                buttonDeactivated = false;
            }
        }  else if(sentence == 0) {
            foreach (GameObject highlight in highlights) {
            Transform pulse = highlight.transform.Find("Pulse");
            pulse.GetComponent<SpriteRenderer>().enabled = true;
            Transform particle = highlight.transform.Find("Streaks");
            particle.GetComponent<ParticleSystem>().Play();
         }
        }



          //     foreach (GameObject highlight in highlights) {
        //     Transform pulse = highlight.transform.Find("Pulse");
        //     pulse.GetComponent<SpriteRenderer>().enabled = true;
        //     Transform particle = highlight.transform.Find("Streaks");
        //     particle.GetComponent<ParticleSystem>().Play();
        //  }
        //Debug.Log(buttonDeactivated);

        if (buttonDeactivated) GetComponent<UnityEngine.UI.Button>().interactable = false;
        else GetComponent<UnityEngine.UI.Button>().interactable = true;
        } else if(animator.GetBool("isOpen") && dialogueEnd) {
            buttonDeactivated = false;
        }
    }

    // 	   private IEnumerator delayNextSentence()
    // {   
    //     yield return new WaitForSeconds(5);
	// 	timeElapsed = true;
        
    // }
    
    private void reactivateButton(KeyCode key) {

       if (Input.GetKeyUp(key)) {
        //    Debug.Log(buttonDeactivated);
         keyPressed = true; 
       }

       if(keyPressed) buttonDeactivated = false;
    }

    public bool isButtonDeactivated() {
        return buttonDeactivated;
    }
}