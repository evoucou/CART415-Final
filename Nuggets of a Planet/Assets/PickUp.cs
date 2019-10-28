using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   /*These are the requirements for this script to work:
     * 1 - You must create a trigger collider designating the reach of the grab action.
     * 2 - You must have an empty as a child to place the picked up object.
     * 3 - ButtonCheck() must be called to fire.
     * 4 - Element object must have a rigidbody.
     * 5 - Element object must have the tag "Element".
     */

    public Transform grabPoint;

    private GameObject element;
    private List<GameObject> elements = new List<GameObject>();
    private bool inRange;
    private bool buttonDown;
    private bool isHolding;

    private GameObject elementIcon;


    
    void Start()
    {
        
        inRange = false;
        buttonDown = false;
        isHolding = false;
        elementIcon = null;
    }

    void Update()
    {
        Debug.Log(inRange);
        Debug.Log("Counts: " + elements.Count);

        //If someone presses the button, this parents the element to the selected empty.
        if (buttonDown)
        {

            // If an element is picked up, its icon appears in Onye's bag
            if (inRange && elementIcon.gameObject != null)
            {
                elementIcon.transform.position = grabPoint.transform.position;
                elementIcon.transform.parent = grabPoint.transform;

            }
        }  

        if (Input.GetKeyDown (KeyCode.P)) {
            PickUpObj();
        }  
  
        
    }
    
    
    //Passively checks for objects within the trigger's range.
    //NOTE: This is the trigger you should have set up.
    private void OnCollisionEnter(Collision other)
    {

        
        if (other.transform.tag == "Element")
        {
            elements.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
       if (other.transform.tag == "Element")
        {
            elements.Remove(other.gameObject);
           
            if (elements.Count == 0)
            {
                inRange = false;
            }    
        }
    }

    // public void dropIt() {
    //     if (buttonDown) {    
    //         buttonDown = false;
    //         isHolding = false;
    //         Destroy(elementIcon);
    //         return;
    //     }
    // }

     //This function will activate an element of the nearest object within range.   
    public void ButtonCheck()
    {
        if (buttonDown)
        {
        
            buttonDown = false;
            isHolding = false;
            if (elementIcon.gameObject != null) Destroy(elementIcon);
            return;
        }

        if(elements.Count == 1 && buttonDown == false)
        {
        
            element = elements[0];
            elementIcon = Instantiate(element, grabPoint.transform.position, grabPoint.transform.rotation);
            Destroy(elementIcon.GetComponent<Rigidbody>());
            buttonDown = true;
            inRange = true;
            isHolding = true;
            
        }
     }

    public void PickUpObj()
    {
        ButtonCheck();
    }

    public bool IsHoldingObject()
    {
        return isHolding;
    }

    public GameObject HeldObject()
    {
        return element;
    }

}
