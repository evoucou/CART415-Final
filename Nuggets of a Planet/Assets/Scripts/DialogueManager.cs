using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    private Queue<string> sentences;
    private int index = 0;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {

        animator.SetBool("isOpen", true);

        // Debug.Log("Starting Conversation");

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
  
        // Displaying the elements in array 
        // foreach(string str in arr) 
        // { 
        //     Debug.Log(str.Length); 
        // } 
        }
        DisplayNextSentence();
    }


    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        // var firstSpaceIndex = sentence.IndexOf(" ");
        // string firstWord = sentence.Substring(0, firstSpaceIndex);

        index = sentences.Count;
        Debug.Log(index);
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        //Debug.Log("\nContents of the first copy:");
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        // Debug.Log(sentence);
        foreach (char letter in sentence.ToCharArray())
        {

            // Debug.Log(dialogueText.text);

            dialogueText.text += letter;
            yield return null;
        }
    }
    
    void EndDialogue() {
        //Debug.Log("End of conversation");
         animator.SetBool("isOpen", false);

    }

    public int dialogueIndex() {
        return index;
    }

}
