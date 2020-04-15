using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform grabPoint;
    public GameObject island;
    private IslandBehaviour islandScript;

    private bool inRange;
    private bool buttonDown;
    private bool isHolding;
    private float dist;

    private GameObject element;
    private GameObject elementIcon;
    private GameObject[] elements;

    // public GameObject myText;

    public Sprite activeP;
    public Sprite inactiveP;
    private Image stateP;

    public GameObject DialogueManager;
	private DialogueManager DialogueScript;
	private int sentence;
	public Animator dialogueState;
    //public Button button;

    private bool cannotPickup;
    private bool rightObjPickedUpTut;
    private bool islandMoving;

    private string name;


    void Start()
    {
        islandScript = island.GetComponent<IslandBehaviour>();

        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        stateP = canvas.transform.Find("PKey").gameObject.GetComponent<Image>();
        
        inRange = false;
        buttonDown = false;
        isHolding = false;
        elementIcon = null;

        // Look for all active elements in the game and put in array
        elements = GameObject.FindGameObjectsWithTag("Element");
        DialogueScript = DialogueManager.GetComponent<DialogueManager>();
        cannotPickup = false;

        rightObjPickedUpTut = false;

    }

    void FixedUpdate()
    {

        islandMoving = islandScript.islandMoving();

        if (islandMoving) cannotPickup = true;
        else cannotPickup = false;

        sentence = DialogueScript.dialogueIndex();
        inRange = false;

        //If someone presses the button, this parents the element to the selected empty.
        if (buttonDown)
        {

            // If an element is picked up, its icon appears in Onye's bag
            if (isHolding && elementIcon.gameObject != null)
            {
                elementIcon.transform.position = grabPoint.transform.position;
                elementIcon.transform.parent = grabPoint.transform;
            } 
        } 

            // Check if player is within range of one element
             foreach (GameObject obj in elements) {
                float dist = Vector3.Distance(obj.transform.position, this.transform.position);
                float minDist = 0.8f;

                if (dist < minDist) {
                inRange = true;
                element = obj;
                // myText.SetActive(true);
                if (!cannotPickup) stateP.sprite = activeP;
                break;
                } else stateP.sprite = inactiveP;
             }


        if (Input.GetKeyDown (KeyCode.P)) {
            if (!cannotPickup) PickUpObj();
            buttonDown = true;        
        } 

        //  if (dialogueState.GetBool("isOpen")) {
        //     //string name;
        //      if(sentence == 7) {
        //          cannotPickup = false;
        //     //button.interactable = false;
        //     // if(buttonDown && inRange) name = element.transform.GetChild(0).gameObject.tag;
        //     //  if (name == "Fire") button.interactable = true;
        //     //     }
        //     } else cannotPickup = true;
        // } else cannotPickup = false;

               if (dialogueState.GetBool("isOpen")) {
               if(sentence == 7) {
              if(inRange) { 
                        name = element.transform.GetChild(0).gameObject.tag;
                    if (buttonDown) {
                    if(name == "Fire") rightObjPickedUpTut = true;
                    else buttonDown = false;
                    }
              }        
                 if(rightObjPickedUpTut) cannotPickup = true;
                 else cannotPickup = false;

               } else cannotPickup = true;
               } else cannotPickup = false;

            
    }  

    

    public void PickUpObj()
    {
        if(inRange)
        {
            // If player already holds an item, delete if first
            if (elementIcon != null) Destroy(elementIcon);

            // Instantiate a little element icon in Onye's bag
            elementIcon = Instantiate(element, new Vector3(grabPoint.transform.position.x + 5.0f, grabPoint.transform.position.y, 0), grabPoint.transform.rotation);
            Destroy(elementIcon.GetComponent<Float>());
            elementIcon.transform.localScale += new Vector3(-0.05f, -0.05f, -0.05f);

            inRange = false;
            isHolding = true;   
        }
    }

    public bool PlayerIsInRange()
    {
        return inRange;
    }

    public bool rightElementPickedUp() {
        return rightObjPickedUpTut;
    }

    public GameObject GetElementInRange()
    {
        return element;
    }

}
