using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandBehaviour : MonoBehaviour
{
 
    private PickUp pickUpScript;
    private GameObject heldObj;
    private GameObject grabPoint;
    private GameObject player;
    private GameObject objInRange;
    private GameObject island;

    // private GameObject startIsland;
    // private GameObject waterIsland;
    // private GameObject fireIsland;
    // private GameObject airIsland;
    // private GameObject earthIsland;
    // private GameObject energyIsland;
    // private GameObject swampIsland;


    private bool inRange;
    private bool islandIsMoving;
    private bool islandIsAtTop;
    private bool combinationExists;
    private bool hasAnObj;

    public float speed;
    private Rigidbody rb;

    private GameObject canvasObj;
    private GameObject myText;
    //private Text myText;
 
    void Start()
    {
        
        // Find the grabPoint associated with player
        player = GameObject.FindWithTag("Player");
        grabPoint = player.transform.Find("Bag").gameObject;
 
        canvasObj = GameObject.FindGameObjectWithTag("MainCanvas");
        myText = canvasObj.transform.Find("Mix").gameObject;
        //myText = textTr.GetComponent<Text>();

        myText.SetActive(false);

        pickUpScript = player.GetComponent<PickUp>();
        
        islandIsMoving = false;
        islandIsAtTop = false;
        combinationExists = false;
        hasAnObj = false;

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

        if (inRange && heldObj != null && heldObj.gameObject.GetComponent<Renderer>().enabled == true) myText.SetActive(true); else myText.SetActive(false);

        if (Input.GetKeyUp(KeyCode.M)) combinationExists = false;

        if (islandIsAtTop)
        {
            islandIsMoving = false;
            islandIsAtTop = false;
        }

        if (combinationExists) 
        {
            islandIsMoving = true;
            heldObj.gameObject.GetComponent<Renderer>().enabled = false;
            
        }

        if (islandIsMoving) {        
            islandRise("Energy Island", "Air", "Fire", heldObj, objInRange); 
           islandRise("Energy Island", "Fire", "Air", heldObj, objInRange); 

            islandRise("Swamp Island", "Earth", "Water", heldObj, objInRange); 
           islandRise("Swamp Island", "Water", "Earth", heldObj, objInRange);

            islandRise("Life Island", "Energy", "Swamp", heldObj, objInRange); 
            islandRise("Life Island", "Swamp", "Energy", heldObj, objInRange);

            islandRise("Lava Island", "Earth", "Fire", heldObj, objInRange); 
            islandRise("Lava Island", "Fire", "Earth", heldObj, objInRange);

            islandRise("Stone Island", "Lava", "Air", heldObj, objInRange); 
            islandRise("Stone Island", "Air", "Lava", heldObj, objInRange);

            islandRise("Sand Island", "Stone", "Water", heldObj, objInRange); 
            islandRise("Sand Island", "Water", "Stone", heldObj, objInRange);
            
            islandRise("Clay Island", "Sand", "Swamp", heldObj, objInRange); 
            islandRise("Clay Island", "Swamp", "Sand", heldObj, objInRange);

            islandRise("Golem Island", "Clay", "Life", heldObj, objInRange); 
            islandRise("Golem Island", "Life", "Clay", heldObj, objInRange);

            islandRise("Human Island", "Golem", "Life", heldObj, objInRange); 
            islandRise("Human Island", "Life", "Golem", heldObj, objInRange);

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
                        //islandIsMoving = true;                
                        
                    } else {
                        islandIsAtTop = true;
                        destroyWalls(islandName, island);
                    }

                }
            }
    }

       // Check if the tried combination exists
       private void checkIfCombinationExists() {
        
        string heldName = heldObj.transform.GetChild(0).gameObject.tag;
        string groundName = objInRange.transform.GetChild(0).gameObject.tag;
        
        //SWAMP
        if (heldName == "Earth") if (groundName == "Water") combinationExists = true;
        if (heldName == "Water") if (groundName == "Earth") combinationExists = true;

        // ENERGY
        if (heldName == "Air") if (groundName == "Fire") combinationExists = true;
        if (heldName == "Fire") if (groundName == "Air") combinationExists = true;

        // LIFE
        if (heldName == "Energy") if (groundName == "Swamp") combinationExists = true;
        if (heldName == "Swamp") if (groundName == "Energy") combinationExists = true;

        // LAVA
        if (heldName == "Fire") if (groundName == "Earth") combinationExists = true;
        if (heldName == "Earth") if (groundName == "Fire") combinationExists = true;

        // STONE
        if (heldName == "Air") if (groundName == "Lava") combinationExists = true;
        if (heldName == "Lava") if (groundName == "Air") combinationExists = true;

        // SAND
        if (heldName == "Stone") if (groundName == "Water") combinationExists = true;
        if (heldName == "Water") if (groundName == "Stone") combinationExists = true;

        // CLAY
        if (heldName == "Sand") if (groundName == "Swamp") combinationExists = true;
        if (heldName == "Swamp") if (groundName == "Sand") combinationExists = true;

         // GOLEM
        if (heldName == "Clay") if (groundName == "Life") combinationExists = true;
        if (heldName == "Life") if (groundName == "Clay") combinationExists = true;

         // HUMAN
        if (heldName == "Golem") if (groundName == "Life") combinationExists = true;
        if (heldName == "Life") if (groundName == "Golem") combinationExists = true;

    }

    // Destroy walls when new island appears
    private void destroyWalls(string name, GameObject island) 
    {

        GameObject startIsland = GameObject.Find("Start");
        GameObject waterIsland = GameObject.Find("Water Island");
        GameObject fireIsland = GameObject.Find("Fire Island");
        GameObject airIsland = GameObject.Find("Air Island");
        GameObject earthIsland = GameObject.Find("Earth Island");
        GameObject energyIsland = GameObject.Find("Energy Island");
        GameObject swampIsland = GameObject.Find("Swamp Island");
        GameObject lifeIsland = GameObject.Find("Life Island");
        GameObject lavaIsland = GameObject.Find("Lava Island");
        GameObject stoneIsland = GameObject.Find("Stone Island");
        GameObject sandIsland = GameObject.Find("Sand Island");
        GameObject clayIsland = GameObject.Find("Clay Island");
        GameObject golemIsland = GameObject.Find("Golem Island");
        GameObject humanIsland = GameObject.Find("Human Island");

        // // Destroying walls where player can walk
        // int walls = island.transform.childCount;
        if (name == "Swamp Island") 
        {
            for (int i = 2; i < 5; ++i) Destroy(island.transform.GetChild(i).gameObject.GetComponent<BoxCollider>());
            Destroy(earthIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(waterIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(startIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
        }

        if (name == "Energy Island") 
        {

            Destroy(fireIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            Destroy(airIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            Destroy(startIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            if (lavaIsland.transform.position.y > -0.16) 
            {
                Destroy(island.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
                Destroy(lavaIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            } 
        }

         if (name == "Life Island") 
        {
            Destroy(waterIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(swampIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());

            if(clayIsland.transform.position.y > -0.16) 
            {
                Destroy(clayIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
                Destroy(island.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
        }

        if (name == "Lava Island") 
        {
            Destroy(fireIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(energyIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            if(energyIsland.transform.position.y > -0.16) 
            {
                Destroy(lavaIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
                Destroy(island.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            }
        }
        
        if (name == "Stone Island") 
        {
            Destroy(airIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
        }

        if (name == "Sand Island") 
        {
            Destroy(airIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(stoneIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(waterIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
        }

        if (name == "Clay Island") 
        {
            Destroy(sandIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(waterIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());

             if(lifeIsland.transform.position.y > -0.16) 
            {
                Destroy(lifeIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
                Destroy(island.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            }
        }

        if (name == "Golem Island") 
        {
            Destroy(lifeIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(clayIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
        }

        if (name == "Human Island") 
        {
            Destroy(lifeIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(golemIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
        }

    }


}
