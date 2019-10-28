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
    

    
    void Start()
    {
        
        inRange = false;
        buttonDown = false;
        isHolding = false;
        elementIcon = null;

        elements = GameObject.FindGameObjectsWithTag("Element");

    }

    void Update()
    {

    Debug.Log(inRange);
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

             foreach (GameObject obj in elements) {
                float dist = Vector3.Distance(obj.transform.position, this.transform.position);
                float minDist = 0.8f;

                if (dist < minDist) {
                inRange = true;
                element = obj;
                Debug.Log(element);
                break;
                } //else inRange = false;
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
            //element = elements[0];
            //If player already holds an item, delete if first
            if (elementIcon != null) Destroy(elementIcon);
            elementIcon = Instantiate(element, new Vector3(grabPoint.transform.position.x + 5.0f, grabPoint.transform.position.y, 0), grabPoint.transform.rotation);
            Destroy(elementIcon.GetComponent<Rigidbody>());
            inRange = false;
            isHolding = true;   
        }
    }

    public bool IsHoldingObject()
    {
        return isHolding;
    }

    public GameObject HeldObject()
    {
        return elementIcon;
    }

}
