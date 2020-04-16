using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTuto : MonoBehaviour
{
    public GameObject trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
