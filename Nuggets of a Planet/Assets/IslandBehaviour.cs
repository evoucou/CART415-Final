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

    private bool energyUp = false;
    private bool swampUp = false;
    private bool lifeUp = false;
    private bool lavaUp = false;
    private bool stoneUp = false;
    private bool sandUp = false;
    private bool clayUp = false;
    private bool golemUp = false;
    private bool humanUp = false;

    private bool inRange;
    private bool islandIsMoving;
    private bool islandIsAtTop;
    private bool combinationExists;

    public float speed;
    private Rigidbody rb;

    public Sprite activeM;
    public Sprite inactiveM;
    private Image stateM;

    // private GameObject myText;
    private Text inRangeElement;

    private ElementBehaviour elementScript;
    private string mostRecentIsland;
 
    void Start()
    {
        mostRecentIsland = null;
        elementScript = GameObject.Find("Energy").GetComponent<ElementBehaviour>();

        // Find the grabPoint associated with player
        player = GameObject.FindWithTag("Player");
        grabPoint = player.transform.Find("Bag").gameObject;
 
        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        stateM = canvas.transform.Find("MKey").gameObject.GetComponent<Image>();
        
        inRangeElement = GameObject.Find("ElementDisplay").GetComponent<Text>(); 
        

        // myText.SetActive(false);
        //inRangeElement.SetActive(false);

        pickUpScript = player.GetComponent<PickUp>();
        
        islandIsMoving = false;
        islandIsAtTop = false;
        combinationExists = false;

    }

    void FixedUpdate()
    {

        // Find the element that is currently held
        if (grabPoint.transform.childCount > 0) heldObj = grabPoint.transform.GetChild(0).gameObject;
        
        // Find element in front of player
        objInRange = pickUpScript.GetElementInRange();

        inRange = pickUpScript.PlayerIsInRange();
        
        // Check if this is a valid combination if in range and player has obj
        if (Input.GetKeyDown(KeyCode.M)) {
            if(heldObj != null && inRange) checkIfCombinationExists(); else return;
        } 

        // Text trigger (press 'm")
        if (inRange && heldObj != null && heldObj.gameObject.GetComponent<Renderer>().enabled == true) stateM.sprite = activeM; else stateM.sprite = inactiveM;
        
        if (inRange) inRangeElement.text = objInRange.ToString();
        else inRangeElement.text = " ";

       // if (Input.GetKeyUp(KeyCode.M)) combinationExists = false;

        if (islandIsAtTop)
        {
            combinationExists = false;
            islandIsAtTop = false;
        }

        // if (combinationExists) 
        // {
        //     islandIsMoving = true;
        //     heldObj.gameObject.GetComponent<Renderer>().enabled = false;
          
        // }

        if (combinationExists) {        
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

            specialIsland("Human Island", "Golem", "Life", heldObj, objInRange); 
            specialIsland("Human Island", "Life", "Golem", heldObj, objInRange);

            // Obj in your bag disappears (reset)
            heldObj.gameObject.GetComponent<Renderer>().enabled = false;
            heldObj = null;

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
                        elementScript.ElementReveal(islandName);
                        destroyWalls(islandName, island);
                                                // .material getter clones the material, 
            // // so cache this copy in a member variable so we can dispose of it when we're done.
            // Material _myMaterial = island.GetComponent<Renderer>().material;

            // // Start a coroutine to fade the material to zero alpha over 3 seconds.
            // // Caching the reference to the coroutine lets us stop it mid-way if needed.
            // StartCoroutine(FadeTo(_myMaterial, 1f, 1.5f));

                        islandIsAtTop = true;
                        islandIsMoving = false;
                        mostRecentIsland = islandName;
                    }

                }
            }
    }


           IEnumerator FadeTo(Material material, float targetOpacity, float duration) {

        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while(t < duration) {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            material.color = color;

            // Wait one frame, and repeat.
            yield return null;
            }
        }

       public void specialIsland(string islandName, string desiredHeldElementName, string desiredFixedElementName, GameObject heldElement, GameObject fixedElement)
    {

        GameObject island = GameObject.Find(islandName);
        //rb = island.GetComponent<Rigidbody>();
        
        Material _myMaterial = island.GetComponent<Renderer>().material;


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
                    // if (_myMaterial.color.a < 1f)
                    // {
                // Start a coroutine to fade the material to zero alpha over 3 seconds.
                // Caching the reference to the coroutine lets us stop it mid-way if needed.
                //elementScript.ElementReveal(islandName);
                destroyWalls(islandName, island);   
                mostRecentIsland = islandName;  
                StartCoroutine(FadeIn(_myMaterial, 1f, 1.5f));          
                        
                    // } else {
                    //     destroyWalls(islandName, island);
                    //     islandIsAtTop = true;
                    //     islandIsMoving = false;
                    //     mostRecentIsland = islandName;
                    // }

                }
            }
    }

       // Check if the tried combination exists
       private void checkIfCombinationExists() {
        
        string heldName = heldObj.transform.GetChild(0).gameObject.tag;
        string groundName = objInRange.transform.GetChild(0).gameObject.tag;
        
        //SWAMP
        if (heldName == "Earth") if (groundName == "Water") if (!swampUp) combinationExists = true;
        if (heldName == "Water") if (groundName == "Earth") if (!swampUp) combinationExists = true;

        // ENERGY
        if (heldName == "Air") if (groundName == "Fire") if (!energyUp) combinationExists = true;
        if (heldName == "Fire") if (groundName == "Air") if (!energyUp) combinationExists = true;

        // LIFE
        if (heldName == "Energy") if (groundName == "Swamp") if(!lifeUp) combinationExists = true;
        if (heldName == "Swamp") if (groundName == "Energy") if(!lifeUp) combinationExists = true;

        // LAVA
        if (heldName == "Fire") if (groundName == "Earth") if(!lavaUp) combinationExists = true;
        if (heldName == "Earth") if (groundName == "Fire") if(!lavaUp) combinationExists = true;

        // STONE
        if (heldName == "Air") if (groundName == "Lava") if(!stoneUp) combinationExists = true;
        if (heldName == "Lava") if (groundName == "Air") if(!stoneUp) combinationExists = true;

        // SAND
        if (heldName == "Stone") if (groundName == "Water") if(!sandUp) combinationExists = true;
        if (heldName == "Water") if (groundName == "Stone") if(!sandUp) combinationExists = true;

        // CLAY
        if (heldName == "Sand") if (groundName == "Swamp") if(!clayUp) combinationExists = true;
        if (heldName == "Swamp") if (groundName == "Sand") if(!clayUp) combinationExists = true;

         // GOLEM
        if (heldName == "Clay") if (groundName == "Life") if(!golemUp) combinationExists = true;
        if (heldName == "Life") if (groundName == "Clay") if(!golemUp) combinationExists = true;

         // HUMAN
        if (heldName == "Golem") if (groundName == "Life") if(!humanUp) combinationExists = true;
        if (heldName == "Life") if (groundName == "Golem") if(!humanUp) combinationExists = true;

    }

    // Destroy walls when new island appears (also determines if island is up or not)
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

            swampUp = true;
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
            energyUp = true;
        }

         if (name == "Life Island") 
        {
            Destroy(waterIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(swampIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());

            if(clayIsland.transform.position.y > -0.2) 
            {
                Destroy(clayIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
                Destroy(island.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
            lifeUp = true;
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
            lavaUp = true;
        }
        
        if (name == "Stone Island") 
        {
            Destroy(airIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            stoneUp = true;
        }

        if (name == "Sand Island") 
        {
            Destroy(airIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(stoneIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(waterIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            sandUp = true;
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
            clayUp = true;
        }

        if (name == "Golem Island") 
        {
            Destroy(lifeIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(clayIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            golemUp = true;
        }

        if (name == "Human Island") 
        {
            Destroy(lifeIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(golemIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            humanUp = true;
        }

    }


        public bool islandMoving() {
            return islandIsMoving;
        }

        public bool islandAppeared() {
            return islandIsAtTop;
        }

        public string recentIsland() {
            return mostRecentIsland;
        }

                IEnumerator FadeIn(Material material, float targetOpacity, float duration) {

        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while(t < duration) {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            material.color = color;

                // islandIsAtTop = true;
                // islandIsMoving = false;
                // mostRecentIsland = islandName;

            // Wait one frame, and repeat.
            yield return null;
            }
        }

            public GameObject GetHeldObject()
    {
        return heldObj;
    }
        
}
