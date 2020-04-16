// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class NewIslandMessage : MonoBehaviour
// {
//     private IslandBehaviour islandScript;
//     private Text text;
//     private string islandName;

        
//     // RectTransform canvas;
//     // RectTransform button;
//     Vector3 startingPosition;
//     public float speed;

//     // Start is called before the first frame update
//     void Start()
//     {

//         islandScript = GameObject.Find("Water Island").GetComponent<IslandBehaviour>();
//         text = GetComponent<Text>();
//         text.text = "";

//         startPosition = transform.position;
//         endPosition = transform.position + transform.up * moveDistance;

//         // button = GetComponent<RectTransform>();
//         // canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
//         // startingPosition = transform.position;
//         // speed = -10f;
//     }

//     // Update is called once per frame
//     void Update()
//     {       

//         StartCoroutine(LerpObject());

//         islandName = islandScript.recentIsland();
//         if(islandName == "Human Island" ||islandName == "Agriculture Island" ||islandName == "Car Island" ||islandName == "House Island" ||islandName == "Animal Island" ) {
        
//         text.text = "You've unlocked the " + islandName;
//         }
//         else {
//         text.text = "";
//         }

//     }
// RectTransform rectTransform = GetComponent<RectTransform>();
//     float timeOfTravel=5; //time after object reach a target place 
//      float currentTime=0; // actual floting time 
//      float normalizedValue;
//       //getting reference to this component 

//     float moveDistance = 10f;

//     Vector3 startPosition;
//     Vector3 endPosition;
 
     
//      IEnumerator LerpObject(){ 
     
//      while (currentTime <= timeOfTravel) { 
//      currentTime += Time.deltaTime; 
//      normalizedValue=currentTime/timeOfTravel; // we normalize our time 
 
//  rectTransform.anchoredPosition=Vector3.Lerp(startPosition,endPosition, normalizedValue); 
//      yield return null; }}
// }
