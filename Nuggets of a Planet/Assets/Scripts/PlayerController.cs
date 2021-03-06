﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -12;
	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;
	public Animator dialogueState;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	Animator animator;
	Transform cameraT;
	CharacterController controller;

	public GameObject DialogueManager;
	private DialogueManager DialogueScript;
	private int sentence;

	private bool dialogueEnd;
	private IslandBehaviour islandScript;

	public GameObject button;

	//public Button button;
	// public Sprite able;
	// public Sprite disable;

	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();

		DialogueScript = DialogueManager.GetComponent<DialogueManager>();
		//button = GetComponent<UnityEngine.UI.Button>();
		islandScript = GameObject.Find("Water Island").GetComponent<IslandBehaviour>();
		dialogueEnd = islandScript.isHumanUp();
		button.GetComponent<DestroyButton>().beginningTrue();
	
	}

	void FixedUpdate() {

		// Know which sentence is displayed
		sentence = DialogueScript.dialogueIndex();

		// input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);

		// if tutorial is open, can't move, unless it's the 'use arrows to move' dialogue
		if (!dialogueState.GetBool("isOpen")) {if(!button.GetComponent<DestroyButton>().isItBeginning()) Move (inputDir, running);}
		else if (dialogueState.GetBool("isOpen") && !dialogueEnd) {
			// if (sentence == 9) {
			// 	Move (inputDir, running);
			// 	if (!timeElapsed) button.interactable = false;
			// 	else button.interactable = true;

			// 	StartCoroutine(delayNextSentence());

			// }
			if(sentence == 9 || sentence == 7 || sentence == 5 ) Move (inputDir, running);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			//Jump ();
		}
		// animator
		float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

	}

	void Move(Vector2 inputDir, bool running) {

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}
			
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded) {
			velocityY = 0;
		}

	}

	void Jump() {
		if (controller.isGrounded) {
			float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
		}
	}

	float GetModifiedSmoothTime(float smoothTime) {
		if (controller.isGrounded) {
			return smoothTime;
		}

		if (airControlPercent == 0) {
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}

}
