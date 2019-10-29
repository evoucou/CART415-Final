using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemy : MonoBehaviour
{

    //private PickUp pickUpScript;
    private GameObject currObj;
    private GameObject grabPoint;
    private GameObject player;
 
    void Start()
    {
        // Find the grabPoint associated with player
        player = GameObject.FindWithTag("Player");
        grabPoint = player.transform.Find("Bag").gameObject;

        // pickUpScript = player.GetComponent<PickUp>();
        // currObj = pickUpScript.HeldObject();
    }

    void Update()
    {
        // Find the bag's child to know which element they have
        if (grabPoint.transform.childCount > 0) currObj = grabPoint.transform.GetChild(0).gameObject;



    }
}
