using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // public float speed = 4;
    // public float rotSpeed = 80;
    // public float gravity = 8;
    // private float rot = 0f;

    // public float jumpSpeed;
    // public float jumpHeight = 5;
    // private Vector3 jump;

    // private int jumps;

    // Vector3 moveDir = Vector3.zero;

    // CharacterController controller;
    // Animation animation;
    // Rigidbody character;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     controller = GetComponent<CharacterController> ();
    //     character = GetComponent<Rigidbody> ();
    //     jump = new Vector3(0.0f, 2.0f, 0.0f);
    //     //animation = GetComponent<Animator> ();
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     Movement();
    //     GetInput();
    // }

    // void GetInput()
    // {
    //     if (controller.isGrounded)
    //     {
    //         if (Input.GetMouseButtonDown (0))
    //         {
    //             // if (animation.GetBool ("running") == true)
    //             // {
    //             //     animation.SetBool ("running", false);
    //             //     animation.SetInteger ("condition", 0);
    //             // }
    //             // else if (animation.GetBol ("running") == false)
    //             // {
    //             //     PickElement();    
    //             // }
    //         }
    //     }
    // }

    // void Movement()
    // {
    //     if (controller.isGrounded)
    //     {
    //         if (Input.GetKeyDown (KeyCode.W))
    //         {
    //             //animation.SetBool ("running", true);
    //             //animation.SetInteger ("condition", 1);
    //             moveDir = new Vector3 (0,0,1);
    //             moveDir *= speed;
    //             moveDir = transform.TransformDirection (moveDir);
    //         }
    //          if(Input.GetKeyUp (KeyCode.W))
    //         {
    //             //animation.SetBool ("running", false);
    //             //animation.SetInteger ("condition", 0);
    //             moveDir = new Vector3(0,0,0);
    //         }
    //         if (Input.GetKeyDown (KeyCode.Space))
    //         {
    //             jumpSpeed = Mathf.Sqrt (-2 * gravity * jumpHeight);
    //             moveDir.y = jumpSpeed;
    //         }
    //     }

    // rot += Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime;
    // transform.eulerAngles = new Vector3 (0, rot, 0);

    // moveDir.y -= gravity * Time.deltaTime;
    // controller.Move (moveDir * Time.deltaTime);
    // }

    // void PickElement()
    // {
    //     //animation.SetInteger ("condition", 2);
    // }

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -12;
	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	Animator animator;
	Transform cameraT;
	CharacterController controller;

	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();

	}

	void Update () {
		// input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);

		Move (inputDir, running);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
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


    // public float walkSpeed = 2;
    // public float runSpeed = 6;

    // public float turnSmoothTime = 0.1f;
    // float turnSmoothVelocity;

    // public float speedSmoothTime = 0.1f;
    // float speedSmoothVelocity;
    // float currentSpeed;

    // Animator animator;
    // //Transform cameraT;
    // CharacterController controller;

    // void Start() {
    //     //animator = GetComponent<Animator>();
    //     //cameraT = Camera.main.transform;
    //     controller = GetComponent<CharacterController>();
    // }

    // void Update() {
    //     Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
    //     Vector2 inputDir = input.normalized;

    //     if (inputDir != Vector2.zero) {
    //         float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg;
    //         transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

    //     }

    //     bool running = Input.GetKey (KeyCode.LeftShift);
    //     float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
    //     currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

    //     transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);

    //     float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
    //     //animator.SetFloat ("speedPercent", animationSpeedPercent);
    // }
}
