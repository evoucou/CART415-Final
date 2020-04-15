using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shake : MonoBehaviour
{
    // public GameObject enemy;
     private Image bagRenderer;
 
     public float damageTime = 0.1f; // duration of shake and red color
 
     public float shakeRange = 20f; // shake range change be changed from inspector,
                                    //keep it mind that max it can go is half in either direction

    private IslandBehaviour islandScript;
    private bool combinationExists;
    private bool inRange;
    private GameObject player;

	private Animator dialogueState;
    private PickUp pickUpScript;
 
     // Use this for initialization
     void Start () {

         player = GameObject.FindWithTag("Player");
         pickUpScript = player.GetComponent<PickUp>();
         //by saving the renderer you will not need to use GetComponent<> again and again
         bagRenderer = GetComponent<Image>();
         islandScript = GameObject.Find("Water Island").GetComponent<IslandBehaviour>();

        GameObject canvas = transform.parent.gameObject;
        GameObject dialogueBox = canvas.transform.Find("DialogueBox").gameObject;

        dialogueState = dialogueBox.GetComponent<Animator>();
        // //button = dialogueBox.transform.GetChild(5).gameObject.GetComponent<Button>();

        // shakeScript = canvas.transform.Find("BagImg").GetComponent<Shake>();
     }
     
     // Update is called once per frame
     void Update() {
        inRange = pickUpScript.PlayerIsInRange();

         combinationExists = islandScript.doesComboExist();
        // if (!dialogueState.GetBool("isOpen")) {
         if (Input.GetKeyDown(KeyCode.M) && inRange)
         { 
             if(!combinationExists) {
             StartCoroutine(Damage());
             StartCoroutine(EnemyShake());
         }
         }
        // }
     
     }
 
     private IEnumerator Damage()
     {
         Color originalColor = bagRenderer.color;
         WaitForSeconds wait = new WaitForSeconds(damageTime);
         bagRenderer.color = new Color32(255, 0, 0, 255); //adjust color to your needs
         yield return wait;
         bagRenderer.color = originalColor;
     }
 
     private IEnumerator EnemyShake()
     {
 
         float elapsed = 0.0f;
         Quaternion originalRotation = transform.rotation;
 
         while (elapsed < damageTime)
         {
 
             elapsed += Time.deltaTime;
             float z = Random.value * shakeRange - (shakeRange /2);
             transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + z); 
             yield return null;
         }
 
        transform.rotation = originalRotation;
     }

	// private Vector3 originPosition;
	// private Quaternion originRotation;
	// public float shake_decay = 0.002f;
	// public float shake_intensity = .3f;

	// private float temp_shake_intensity = 0;
	
	// void Start (){
    //     Shaking();
	// }
	
	// void Update (){
	// 	if (temp_shake_intensity > 0){
	// 		transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
	// 		transform.rotation = new Quaternion(
	// 			originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
	// 			originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
	// 			originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
	// 			originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
	// 		temp_shake_intensity -= shake_decay;
	// 	}
	// }
	
	// void Shaking(){
	// 	originPosition = transform.position;
	// 	originRotation = transform.rotation;
	// 	temp_shake_intensity = shake_intensity;

	// }

}
