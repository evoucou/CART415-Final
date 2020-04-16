using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandCounter : MonoBehaviour
{
    private IslandBehaviour islandScript;
    private int counter = 0 ;
    private Text countText;

    // Start is called before the first frame update
    void Start()
    {
        islandScript = GameObject.Find("Water Island").GetComponent<IslandBehaviour>();
        countText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        counter = islandScript.trackIslandCount();
        countText.text = counter.ToString() + "/5";
        // Debug.Log(counter);

    }
}
