using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanIsland : MonoBehaviour
{
    private IslandBehaviour islandScript;
    private DialogueTrigger dialogueScript;
    private bool humanCreated;
    // Start is called before the first frame update
    void Start()
    {
        islandScript = GetComponent<IslandBehaviour>();
        dialogueScript = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        humanCreated = islandScript.isHumanUp();
        if(humanCreated) dialogueScript.TriggerDialogue();

    }

    // public bool dialogueFromBeginning() {
    //     return dialogueBeginning;
    // }
}
