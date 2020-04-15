using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandBehaviour : MonoBehaviour
{
    // public EndDialogue endDialogue;
 
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
    private bool metalUp = false;
    private bool toolsUp = false;
    private bool bricksUp = false;
    private bool cementUp = false;
    private bool seedsUp = false;
    private bool eggUp = false;
    private bool turtleUp = false;
    private bool lizardUp = false;
    private bool treeUp = false;
    private bool forestUp = false;
    private bool carbonUp = false;
    private bool oilUp = false;
    private bool fieldsUp = false;
    private bool woodUp = false;
    private bool wheelUp = false;
    private bool cartUp = false;
    private bool beastUp = false;
    private bool horseUp = false;

    private bool inRange;
    private bool islandIsMoving;
    private bool islandIsAtTop;
    private bool combinationExists;

    public float speed;
    private Rigidbody rb;

    public Sprite activeM;
    public Sprite inactiveM;
    private Image stateM;

    private GameObject DialogueManager;
	private DialogueManager DialogueScript;
	private int sentence;
	private Animator dialogueState;
    //private Button button;

    // private GameObject myText;
    private Text inRangeElement;

    private ElementBehaviour elementScript;
    private string mostRecentIsland;

    // private GameObject[] highlights;
    //bool buttonOk = true;

    private bool rightObjToMixTut;
 
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

        GameObject dialogueBox = canvas.transform.Find("DialogueBox").gameObject;

        dialogueState = dialogueBox.GetComponent<Animator>();
        // //button = dialogueBox.transform.GetChild(5).gameObject.GetComponent<Button>();


        DialogueManager = GameObject.Find("DialogueManager");
        DialogueScript = DialogueManager.GetComponent<DialogueManager>();

        // myText.SetActive(false);
        //inRangeElement.SetActive(false);

        pickUpScript = player.GetComponent<PickUp>();
        
        islandIsMoving = false;
        islandIsAtTop = false;
        combinationExists = false;

        rightObjToMixTut = false;
    }

    void FixedUpdate()
    {

        sentence = DialogueScript.dialogueIndex();

        // Find the element that is currently held
        if (grabPoint.transform.childCount > 0) heldObj = grabPoint.transform.GetChild(0).gameObject;
        
        // Find element in front of player
        objInRange = pickUpScript.GetElementInRange();

        inRange = pickUpScript.PlayerIsInRange();
        
        // Check if this is a valid combination if in range and player has obj

        // if (dialogueState.GetBool("isOpen")) {
        //     //Debug.Log("Dialogue is open");
        //     if(sentence == 5) {
        // if (Input.GetKeyDown(KeyCode.M)) {
        //     string name;
        //     if(heldObj != null && inRange) { 
        //          Debug.Log("in range");
        //     name = objInRange.transform.GetChild(0).gameObject.tag;
        //     if (name == "Earth") {
        //         Debug.Log("in last one");
        //         buttonOk = true;
        //         checkIfCombinationExists();
        //                 }
        //             }
        //         }
        //     } else if (sentence == 0) {
        //     foreach (GameObject highlight in highlights) {
        //     Transform pulse = highlight.transform.Find("Pulse");
        //     pulse.GetComponent<SpriteRenderer>().enabled = true;
        //     Transform particle = highlight.transform.Find("Streaks");
        //     particle.GetComponent<ParticleSystem>().Play();
        //  }
        //     }
        // } else {
            if (!dialogueState.GetBool("isOpen")) 
            {
                if (Input.GetKeyDown(KeyCode.M)) if(heldObj != null && inRange) checkIfCombinationExists(); else return;
            } else {
                if(sentence == 5) if (Input.GetKeyDown(KeyCode.M)) if(inRange) {
                    string name = objInRange.transform.GetChild(0).gameObject.tag;
                    if (name == "Earth") {
                        // Debug.Log("held: " +heldObj);
                        // Debug.Log(objInRange);
                        checkIfCombinationExists();
                        // IslandUpTutorial();
                        // if (heldObj != null) heldObj.gameObject.GetComponent<Renderer>().enabled = false;
                        rightObjToMixTut = true;
                    } // if earth
                }
            
         }
            
        
    

        //          if (dialogueState.GetBool("isOpen")) {
        //     string name;
        //      if(sentence == 6) {
        //     button.interactable = false;
        //     if(buttonDown && inRange) {
        //     name = element.transform.GetChild(0).gameObject.tag;
        //      if (name == "Fire") button.interactable = true;
        //         }
        //     }  
        // } 

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
    
            islandRise("Egg Island", "Stone", "Sand", heldObj, objInRange); 
            islandRise("Egg Island", "Sand", "Stone", heldObj, objInRange);
            
            islandRise("Clay Island", "Sand", "Swamp", heldObj, objInRange); 
            islandRise("Clay Island", "Swamp", "Sand", heldObj, objInRange);

            islandRise("Golem Island", "Clay", "Life", heldObj, objInRange); 
            islandRise("Golem Island", "Life", "Clay", heldObj, objInRange);

            islandRise("Metal Island", "Stone", "Fire", heldObj, objInRange); 
            islandRise("Metal Island", "Fire", "Stone", heldObj, objInRange);
            
            islandRise("Tools Island", "Metal", "Human", heldObj, objInRange); 
            islandRise("Tools Island", "Human", "Metal", heldObj, objInRange);

            islandRise("Bricks Island", "Clay", "Stone", heldObj, objInRange); 
            islandRise("Bricks Island", "Stone", "Clay", heldObj, objInRange);

            islandRise("Cement Island", "Clay", "Sand", heldObj, objInRange); 
            islandRise("Cement Island", "Sand", "Clay", heldObj, objInRange);

            islandRise("Seeds Island", "Sand", "Earth", heldObj, objInRange); 
            islandRise("Seeds Island", "Earth", "Sand", heldObj, objInRange);
            
            islandRise("Tree Island", "Seeds", "Earth", heldObj, objInRange); 
            islandRise("Tree Island", "Earth", "Seeds", heldObj, objInRange);

            islandRise("Forest Island", "Tree", "Tree", heldObj, objInRange); 
    
            islandRise("Carbon Island", "Forest", "Fire", heldObj, objInRange); 
            islandRise("Carbon Island", "Fire", "Forest", heldObj, objInRange);
        
            islandRise("Oil Island", "Carbon", "Fire", heldObj, objInRange); 
            islandRise("Oil Island", "Fire", "Carbon", heldObj, objInRange);
            
            islandRise("Turtle Island", "Sand", "Egg", heldObj, objInRange); 
            islandRise("Turtle Island", "Egg", "Sand", heldObj, objInRange);

            islandRise("Lizard Island", "Turtle", "Earth", heldObj, objInRange); 
            islandRise("Lizard Island", "Earth", "Turtle", heldObj, objInRange);

            islandRise("Fields Island", "Tools", "Earth", heldObj, objInRange); 
            islandRise("Fields Island", "Earth", "Tools", heldObj, objInRange);

            islandRise("Wood Island", "Tools", "Tree", heldObj, objInRange); 
            islandRise("Wood Island", "Tree", "Tools", heldObj, objInRange);

            islandRise("Wheel Island", "Tools", "Wood", heldObj, objInRange); 
            islandRise("Wheel Island", "Wood", "Tools", heldObj, objInRange);

            islandRise("Cart Island", "Wheel", "Wood", heldObj, objInRange); 
            islandRise("Cart Island", "Wood", "Wheel", heldObj, objInRange);

            islandRise("Beast Island", "Lizard", "Forest", heldObj, objInRange); 
            islandRise("Beast Island", "Forest", "Lizard", heldObj, objInRange);

            islandRise("Horse Island", "Beast", "Cart", heldObj, objInRange); 
            islandRise("Horse Island", "Cart", "Beast", heldObj, objInRange);

            specialIsland("Human Island", "Golem", "Life", heldObj, objInRange); 
            specialIsland("Human Island", "Life", "Golem", heldObj, objInRange);

            // Obj in your bag disappears (reset)
            heldObj.gameObject.GetComponent<Renderer>().enabled = false;

        }

        //  if (islandIsMoving) if (island.transform.position.y < 0) island.transform.Translate(Vector3.up * Time.deltaTime, Space.World); else islandIsAtTop = true;
    }

    // private void IslandUpTutorial() {

    //     //string islandName = "Lava Island";
    //     GameObject island = GameObject.Find("Lava Island");
    //     rb = island.GetComponent<Rigidbody>();

    //                 if (island.transform.position.y < 0.5)
    //                 {
    //                     // island.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    //                     Vector3 dir = new Vector3(0, 1, 0);
    //                     dir = dir.normalized * speed * Time.deltaTime;
    //                     rb.MovePosition(island.transform.position + dir);
    //                     islandIsMoving = true;                
                        
    //                 } else {
    //                     elementScript.ElementReveal("Lava Island");
    //                     destroyWalls("Lava Island", island);

    //                     islandIsAtTop = true;
    //                     islandIsMoving = false;
    //                     mostRecentIsland = "Lava Island";
    //                 }
    // }

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
                elementScript.ElementReveal(islandName);      

                islandIsAtTop = true;
                islandIsMoving = false;
                mostRecentIsland = islandName;
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

        //LAVA
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

        // METAL
        if (heldName == "Fire") if (groundName == "Stone") if(!metalUp) combinationExists = true;
        if (heldName == "Stone") if (groundName == "Fire") if(!metalUp) combinationExists = true;

        // TOOLS
        if (heldName == "Human") if (groundName == "Metal") if(!toolsUp) combinationExists = true;
        if (heldName == "Metal") if (groundName == "Human") if(!toolsUp) combinationExists = true;

        // BRICKS
        if (heldName == "Clay") if (groundName == "Stone") if(!bricksUp) combinationExists = true;
        if (heldName == "Stone") if (groundName == "Clay") if(!bricksUp) combinationExists = true;
        
        // CEMENT
        if (heldName == "Clay") if (groundName == "Sand") if(!cementUp) combinationExists = true;
        if (heldName == "Sand") if (groundName == "Clay") if(!cementUp) combinationExists = true;

        // SEEDS
        if (heldName == "Sand") if (groundName == "Earth") if(!seedsUp) combinationExists = true;
        if (heldName == "Earth") if (groundName == "Sand") if(!seedsUp) combinationExists = true;

        // TREE
        if (heldName == "Seeds") if (groundName == "Earth") if(!treeUp) combinationExists = true;
        if (heldName == "Earth") if (groundName == "Seeds") if(!treeUp) combinationExists = true;

        // FOREST
        if (heldName == "Tree") if (groundName == "Tree") if(!forestUp) combinationExists = true;

        // CARBON
        if (heldName == "Forest") if (groundName == "Fire") if(!carbonUp) combinationExists = true;
        if (heldName == "Fire") if (groundName == "Forest") if(!carbonUp) combinationExists = true;

        // OIL
        if (heldName == "Carbon") if (groundName == "Fire") if(!oilUp) combinationExists = true;
        if (heldName == "Fire") if (groundName == "Carbon") if(!oilUp) combinationExists = true;

        // EGG
        if (heldName == "Stone") if (groundName == "Sand") if(!eggUp) combinationExists = true;
        if (heldName == "Sand") if (groundName == "Stone") if(!eggUp) combinationExists = true;

        // TURTLE
        if (heldName == "Egg") if (groundName == "Sand") if(!turtleUp) combinationExists = true;
        if (heldName == "Sand") if (groundName == "Egg") if(!turtleUp) combinationExists = true;

        // LIZARD
        if (heldName == "Earth") if (groundName == "Turtle") if(!lizardUp) combinationExists = true;
        if (heldName == "Turtle") if (groundName == "Earth") if(!lizardUp) combinationExists = true;

        // BEAST
        if (heldName == "Forest") if (groundName == "Lizard") if(!beastUp) combinationExists = true;
        if (heldName == "Lizard") if (groundName == "Forest") if(!beastUp) combinationExists = true;

       // HORSE
        if (heldName == "Beast") if (groundName == "Cart") if(!horseUp) combinationExists = true;
        if (heldName == "Cart") if (groundName == "Beast") if(!horseUp) combinationExists = true;

        // FIELDS
        if (heldName == "Earth") if (groundName == "Tools") if(!fieldsUp) combinationExists = true;
        if (heldName == "Tools") if (groundName == "Earth") if(!fieldsUp) combinationExists = true;

        // WOOD
        if (heldName == "Tree") if (groundName == "Tools") if(!woodUp) combinationExists = true;
        if (heldName == "Tools") if (groundName == "Tree") if(!woodUp) combinationExists = true;

        // WHEEL
        if (heldName == "Wood") if (groundName == "Tools") if(!wheelUp) combinationExists = true;
        if (heldName == "Tools") if (groundName == "Wood") if(!wheelUp) combinationExists = true;

        // CART
        if (heldName == "Wood") if (groundName == "Wheel") if(!cartUp) combinationExists = true;
        if (heldName == "Wheel") if (groundName == "Wood") if(!cartUp) combinationExists = true;
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
        GameObject metalIsland = GameObject.Find("Metal Island");
        GameObject toolsIsland = GameObject.Find("Tools Island");
        GameObject bricksIsland = GameObject.Find("Bricks Island");
        GameObject cementIsland = GameObject.Find("Cement Island");
        GameObject seedsIsland = GameObject.Find("Seeds Island");
        GameObject eggIsland = GameObject.Find("Egg Island");
        GameObject turtleIsland = GameObject.Find("Turtle Island");
        GameObject lizardIsland = GameObject.Find("Lizard Island");
        GameObject beastIsland = GameObject.Find("Beast Island");
        GameObject treeIsland = GameObject.Find("Tree Island");
        GameObject forestIsland = GameObject.Find("Forest Island");
        GameObject carbonIsland = GameObject.Find("Carbon Island");
        GameObject oilIsland = GameObject.Find("Oil Island");
        GameObject fieldsIsland = GameObject.Find("Fields Island");
        GameObject woodIsland = GameObject.Find("Wood Island");
        GameObject wheelIsland = GameObject.Find("Wheel Island");
        GameObject cartIsland = GameObject.Find("Cart Island");
        GameObject horseIsland = GameObject.Find("Horse Island");


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
            if (metalIsland.transform.position.y > -0.16) 
            {
                Destroy(island.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
                Destroy(metalIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
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

        
        if (name == "Egg Island") 
        {
            Destroy(stoneIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
         if(bricksIsland.transform.position.y > -0.16) 
            {
            Destroy(bricksIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            } 
            eggUp = true;
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

        
        if (name == "Metal Island") 
        {
            Destroy(stoneIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            Destroy(airIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());

            if(energyIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(energyIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
            if(turtleIsland.transform.position.y > -0.16) 
            {
            Destroy(turtleIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }

             if(lizardIsland.transform.position.y > -0.16) 
            {
            Destroy(lizardIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }

            if(beastIsland.transform.position.y > -0.16) 
            {
            Destroy(beastIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            }
            metalUp = true;
        }

               if (name == "Tools Island") 
        {
            Destroy(swampIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(lifeIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            toolsUp = true;
        }

                if (name == "Bricks Island") 
        {
            Destroy(stoneIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(sandIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
                if(cementIsland.transform.position.y > -0.16) 
            {
            Destroy(cementIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            }  
            if(eggIsland.transform.position.y > -0.16) 
            {
            Destroy(eggIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }
            bricksUp = true;
        }
        
                if (name == "Cement Island") 
        {
            Destroy(sandIsland.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(clayIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
             if(bricksIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(bricksIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            }
            cementUp = true;
        }

                if (name == "Seeds Island") 
        {   
            Destroy(earthIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
                 if(swampIsland.transform.position.y > -0.16) 
            {
            Destroy(swampIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
            if(toolsIsland.transform.position.y > -0.16) 
            {
            Destroy(toolsIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            }

             if(fieldsIsland.transform.position.y > -0.16) 
            {
            Destroy(fieldsIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            }
            seedsUp = true;
        }

            if (name == "Tree Island") 
        {   
            Destroy(earthIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(seedsIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            treeUp = true;
        }

            if (name == "Forest Island") 
        {   
            Destroy(treeIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            forestUp = true;
        }

            if (name == "Carbon Island") 
        {   
            Destroy(forestIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            Destroy(treeIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(earthIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            Destroy(fireIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());

             if(woodIsland.transform.position.y > -0.16) 
            {
            Destroy(woodIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }
            carbonUp = true;
        }


            if (name == "Oil Island") 
        {   
            Destroy(forestIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(carbonIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());

           if(woodIsland.transform.position.y > -0.16) 
            {
            Destroy(woodIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
            if(cartIsland.transform.position.y > -0.16) 
            {
            Destroy(cartIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(island.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }
            oilUp = true;
        }
        
           if (name == "Turtle Island") 
        {
            Destroy(stoneIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(eggIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
             if(metalIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(metalIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
            turtleUp = true;
        }
        
           if (name == "Lizard Island") 
        {
            Destroy(turtleIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
             if(metalIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(metalIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }
            turtleUp = true;
        }

           if (name == "Beast Island") 
        {
            Destroy(lizardIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(energyIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(lavaIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());

             if(metalIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(5).gameObject.GetComponent<BoxCollider>());
            Destroy(metalIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            }
            beastUp = true;
        }

           if (name == "Fields Island") 
        {
            Destroy(toolsIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());

             if(seedsIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(seedsIsland.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            }
            fieldsUp = true;
        }

          if (name == "Wood Island") 
        {
            Destroy(fireIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            Destroy(lavaIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());

             if(carbonIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(carbonIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }
             if(oilIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(oilIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            }
            woodUp = true;
        }
           if (name == "Wheel Island") 
        {
            Destroy(lavaIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            Destroy(woodIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            wheelUp = true;
        }
           if (name == "Cart Island") 
        {
            Destroy(wheelIsland.transform.GetChild(1).gameObject.GetComponent<BoxCollider>());
            Destroy(woodIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            if(oilIsland.transform.position.y > -0.16) 
            {
            Destroy(island.transform.GetChild(0).gameObject.GetComponent<BoxCollider>());
            Destroy(oilIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            }
            cartUp = true;
        }
           if (name == "Horse Island") 
        {
            Destroy(wheelIsland.transform.GetChild(4).gameObject.GetComponent<BoxCollider>());
            Destroy(lavaIsland.transform.GetChild(3).gameObject.GetComponent<BoxCollider>());
            Destroy(beastIsland.transform.GetChild(2).gameObject.GetComponent<BoxCollider>());
            horseUp = true;
        }

        checkIfTutorialHighlight(name);

    }

         private void checkIfTutorialHighlight(string islandName) {

            var firstSpaceIndex = islandName.IndexOf(" ");
            string elementName = islandName.Substring(0, firstSpaceIndex);

            GameObject highlight;

            // string name = element.transform.GetChild(0).gameObject.tag;

            if (elementName == "Swamp" | elementName == "Stone" | elementName == "Sand" | elementName == "Energy" | elementName == "Clay" | elementName == "Life" | elementName == "Clay" | elementName == "Golem") {
                
                string highlightName = elementName + "Pulse";
               highlight = GameObject.Find(highlightName);
            //    Debug.Log("highlight has been instantiated as " + highlight);
            } else highlight = null;

            if(highlight!=null) {
            // Debug.Log("highlight exists, starting streaks system");
    
            Transform pulse = highlight.transform.Find("Pulse");
            pulse.GetComponent<SpriteRenderer>().enabled = false;
            Transform particle = highlight.transform.Find("Streaks");
            particle.GetComponent<ParticleSystem>().Stop();
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

    public bool rightElementToMix() {
        return rightObjToMixTut;
    }

        public bool isHumanUp() {
        return humanUp;
    }
        
}
