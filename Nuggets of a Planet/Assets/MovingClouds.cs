using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds : MonoBehaviour
{
  public float speed;
 
     void Update() {
         transform.Translate(Vector3.forward * Time.deltaTime * speed);

         if(this.transform.position.z > 20) this.transform.position = new Vector3(0,0,-8.5f);
     }

        
}
