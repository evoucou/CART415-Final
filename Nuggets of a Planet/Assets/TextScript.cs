using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        fullText = "Hello there! It seems like you are new here. How about I show you how to play?.";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        for(int i = 0; i < fullText.Length; i++){
            currentText = fullText.Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
