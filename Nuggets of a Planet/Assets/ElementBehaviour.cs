using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour
{
    // private IslandBehaviour IslandScript;
    // private GameObject island;
    // private bool islandIsUp;
    // private MeshRenderer mesh;

    // private GameObject[] elements;

    // Start is called before the first frame update
    // void Start()
    // {
    // // Look for all active elements in the game and put in array
    // elements = GameObject.FindGameObjectsWithTag("Element");

    // mesh = this.GetComponent<MeshRenderer>();
    // mesh.enabled = false;
    // island = GameObject.Find("Fire Island");
    // IslandScript = island.GetComponent<IslandBehaviour>();

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //   islandIsUp = IslandScript.islandAppeared();
    //   Debug.Log();

    //   if (islandIsUp) StartCoroutine(ElementReveal());
    // }

        private IEnumerator ElementAppearance(GameObject element)
    {
        MeshRenderer mesh = element.GetComponent<MeshRenderer>();
        yield return new WaitForSeconds(0.5f);
        mesh.enabled = true;
    }

    public void ElementReveal(string islandName) {
        // Get the first word only of the island, which is our element name
        var firstSpaceIndex = islandName.IndexOf(" ");

        string elementName = islandName.Substring(0, firstSpaceIndex);
        GameObject element = GameObject.Find(elementName);

        StartCoroutine(ElementAppearance(element));
    }


}
