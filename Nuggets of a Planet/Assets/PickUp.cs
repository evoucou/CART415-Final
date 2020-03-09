using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform grabPoint;

    private bool inRange;
    private bool buttonDown;
    private bool isHolding;
    private float dist;

    private GameObject element;
    private GameObject elementIcon;
    private GameObject[] elements;

    public GameObject myText;
    


    void Start()
    {
        
        inRange = false;
        buttonDown = false;
        isHolding = false;
        elementIcon = null;
        myText.SetActive(false);

        // Look for all active elements in the game and put in array
        elements = GameObject.FindGameObjectsWithTag("Element");

    }

    void FixedUpdate()
    {

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
                myText.SetActive(true);
                break;
                } else myText.SetActive(false);
             }


        if (Input.GetKeyDown (KeyCode.P)) {
            PickUpObj();
            buttonDown = true;
        }   
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

    public GameObject GetElementInRange()
    {
        return element;
    }

}
