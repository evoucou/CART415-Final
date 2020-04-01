using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObj : MonoBehaviour
{

    public Animator minimapAnimator;
    public Animator dialogueAnimator;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dialogueAnimator.GetBool("isOpen")) minimapAnimator.SetBool("isDisplayed", false);
        else minimapAnimator.SetBool("isDisplayed", true);
    }
}
