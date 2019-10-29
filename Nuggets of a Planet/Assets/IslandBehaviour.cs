using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehaviour : MonoBehaviour
{
 
    private PickUp pickUpScript;
    private GameObject heldObj;
    private GameObject grabPoint;
    private GameObject player;
    private GameObject objInRange;
    private GameObject island;

    private bool inRange;
    private bool islandIsMoving;
    private bool islandIsAtTop;
    private bool keyPressed;
    private bool combinationExists;

    public float speed;
    private Rigidbody rb;
 
    void Start()
    {
        // Find the grabPoint associated with player
        player = GameObject.FindWithTag("Player");
        grabPoint = player.transform.Find("Bag").gameObject;

        pickUpScript = player.GetComponent<PickUp>();
        
        islandIsMoving = false;
        islandIsAtTop = false;
        keyPressed = false;
        combinationExists = false;


    }

    void FixedUpdate()
    {

        // Find the element that is currently held
        if (grabPoint.transform.childCount > 0) heldObj = grabPoint.transform.GetChild(0).gameObject;
        
        // Find element in front of player
        objInRange = pickUpScript.GetElementInRange();

        inRange = pickUpScript.PlayerIsInRange();
        
        if (Input.GetKeyDown(KeyCode.M)) {
            if(heldObj != null && inRange) checkIfCombinationExists(); else return;
        }
        if (Input.GetKeyUp(KeyCode.M)) combinationExists = false;

       //if(keyPressed && inRange) checkIfCombinationExists(combinationExists, heldObj, objInRange);

        if (islandIsAtTop)
        {
            islandIsMoving = false;
            islandIsAtTop = false;
        }

        if (combinationExists) islandIsMoving = true;

        if (islandIsMoving) {        
            islandRise("Energy Island", "Air", "Fire", heldObj, objInRange); 
           islandRise("Energy Island", "Fire", "Air", heldObj, objInRange); 

            islandRise("Swamp Island", "Earth", "Water", heldObj, objInRange); 
           islandRise("Swamp Island", "Water", "Earth", heldObj, objInRange);

        }

        //  if (islandIsMoving) if (island.transform.position.y < 0) island.transform.Translate(Vector3.up * Time.deltaTime, Space.World); else islandIsAtTop = true;
    }

    public void islandRise(string islandName, string desiredHeldElementName, string desiredFixedElementName, GameObject heldElement, GameObject fixedElement)
    {
        
        GameObject island = GameObject.Find(islandName);
        rb = island.GetComponent<Rigidbody>();

        string heldElementName = heldElement.transform.GetChild(0).gameObject.tag;
        string fixedElementName = fixedElement.transform.GetChild(0).gameObject.tag;

        // Debug.Log("Held Element Actual Name : " + heldElementName);
        // Debug.Log("Held Element Desired Name : " + desiredHeldElementName);

        // Debug.Log("Fixed Element Actual Name : " + fixedElementName);
        // Debug.Log("Fixed Element Desired Name : " + desiredFixedElementName);

            if (heldElementName == desiredHeldElementName)
            {
                if(fixedElementName == desiredFixedElementName)
                {
                    if (island.transform.position.y < 0)
                    {
                        // island.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
                        Vector3 dir = new Vector3(0, 1, 0);
                        dir = dir.normalized * speed * Time.deltaTime;
                        rb.MovePosition(island.transform.position + dir);
                        islandIsMoving = true;
                    } else {
                        islandIsAtTop = true;
                    }

                }
            }
    }

    // public void checkIfCombinationExists(bool exists, GameObject isHeld, GameObject onGround) {
        
    //     string heldName = isHeld.transform.GetChild(0).gameObject.tag;
    //     string groundName = onGround.transform.GetChild(0).gameObject.tag;
        
    //     if (heldName == "Earth") if (groundName == "Water") exists = true;
    //     if (heldName == "Water") if (groundName == "Earth") exists = true;

    //     if (heldName == "Air") if (groundName == "Fire") exists = true;
    //     if (heldName == "Fire") if (groundName == "Air") exists = true;

    //     Debug.Log("inside: " + exists);
    // }

       // Check if the tried combination exists
       private void checkIfCombinationExists() {
        
        string heldName = heldObj.transform.GetChild(0).gameObject.tag;
        string groundName = objInRange.transform.GetChild(0).gameObject.tag;
        
        if (heldName == "Earth") if (groundName == "Water") combinationExists = true;
        if (heldName == "Water") if (groundName == "Earth") combinationExists = true;

        if (heldName == "Air") if (groundName == "Fire") combinationExists = true;
        if (heldName == "Fire") if (groundName == "Air") combinationExists = true;

    }

}
