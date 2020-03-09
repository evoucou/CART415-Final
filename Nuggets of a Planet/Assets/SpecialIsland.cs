using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialIsland : MonoBehaviour
{

    private IslandBehaviour islandScript;
    // Start is called before the first frame update
    void Start()
    {
        islandScript = GameObject.Find("Water Island").GetComponent<IslandBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
            // var firstSpaceIndex = islandScript.recentIsland().IndexOf(" ");

            // string elementName = islandScript.recentIsland().Substring(0, firstSpaceIndex);
            // element = GameObject.Find(elementName);

            // if (elementName == human) {

            // }
    }
}
