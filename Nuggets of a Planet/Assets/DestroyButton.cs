using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Destroy() {
        GetComponent<Image>().enabled = false;
    }
}
