using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
   
    private Image elementUI;
    public Sprite fireEl;
    public Sprite earthEl;
    public Sprite waterEl;
    public Sprite airEl;
    public Sprite empty;

    private IslandBehaviour islandScript;
    private GameObject heldObject;
    //private GameObject island;
    private string heldObjectTag;
      
    // Start is called before the first frame update
    void Start()
    {   
        islandScript = GameObject.Find("Air Island").GetComponent<IslandBehaviour>();

        elementUI = GetComponent<Image>();
        elementUI.sprite = empty;
    }

    // Update is called once per frame
    void Update()
    {

        if(islandScript.GetHeldObject() != null) heldObject = islandScript.GetHeldObject();
        if(heldObject != null) heldObjectTag = heldObject.transform.GetChild(0).gameObject.tag;

        getHeldObjName();
    }

    private void getHeldObjName()
    {
        if (heldObject != null && heldObject.gameObject.GetComponent<Renderer>().enabled)
        {
        if(heldObjectTag == "Fire") elementUI.sprite = fireEl;
        else if(heldObjectTag == "Earth") elementUI.sprite = earthEl;
        else if(heldObjectTag == "Water") elementUI.sprite = waterEl;
        else if(heldObjectTag == "Air") elementUI.sprite = airEl;
        } else elementUI.sprite = empty;
    }
}
