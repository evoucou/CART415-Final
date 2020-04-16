using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyButton : MonoBehaviour
{
    private GameObject menu;
    private bool beginning;

    // void start() {
    //     beginning = true;
    // }

    public void beginningTrue() {
        beginning = true;
    }
    
    public void Destroy() {
        //GetComponent<Image>().enabled = false;
        // menu = this.transform.parent.gameObject;
        beginning = false;
       transform.parent.gameObject.SetActive(false);
    }

    public bool isItBeginning() {
        return beginning;
    }
}
