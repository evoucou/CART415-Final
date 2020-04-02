using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonCamera : MonoBehaviour
{
	//public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
	public Transform targetFire;
	public Transform targetEarth;
	public Transform targetLava;
	public Transform targetHuman;
	//private Transform currTarget;
	public float dstFromTarget = 4;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Vector2 pitchMinMax = new Vector2 (-40, 85);

	private bool lookAroundActivated;

	public float rotationSmoothTime = .12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

	private IslandBehaviour IslandScript;
	private bool islandIsMoving;
	public GameObject island;

	//For camera Shake
	public float duration = 0.5f;
	public float magnitude = 0.03f;

	// Icon
	public Sprite lockedCam;
    public Sprite unlockedCam;
    private Image camIcon;

	public GameObject DialogueManager;
	private DialogueManager DialogueScript;
	private int sentence;
	public Animator dialogueState;

	public Button button;
	private bool cPressed = false;

	// 	// Transform of the camera to shake. Grabs the gameObject's transform
	// // if null.
	// public Transform camTransform;
	
	// // How long the object should shake for.
	// public float shakeDuration = 0f;
	
	// // Amplitude of the shake. A larger value shakes the camera harder.
	// public float shakeAmount = 0.7f;
	// public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;

	void Start() {
		// if (lockCursor) {
		// 	Cursor.lockState = CursorLockMode.Locked;
		// 	Cursor.visible = false;
		// }

		GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        camIcon = canvas.transform.Find("CameraIcon").gameObject.GetComponent<Image>();
		camIcon.sprite = lockedCam;
		lookAroundActivated = false;

		DialogueScript = DialogueManager.GetComponent<DialogueManager>();

		// 		if (camTransform == null)
		// {
		// 	camTransform = GetComponent(typeof(Transform)) as Transform;
		// }

		IslandScript = island.GetComponent<IslandBehaviour>();
	}

	void FixedUpdate () {

		sentence = DialogueScript.dialogueIndex();
		islandIsMoving = IslandScript.islandMoving();
					//Debug.Log(lookAroundActivated);

    //     if (Input.GetKeyDown (KeyCode.C)) {
	// 		if (!dialogueState.GetBool("isOpen")) {
	// 		if(!lookAroundActivated) lookAroundActivated = true;
	// 		else lookAroundActivated = false;
	// 		} // if dialogue not open
	// 		else {
	// 			if(sentence == 7) {
	// 		if(!lookAroundActivated) lookAroundActivated = true;
	// 		else lookAroundActivated = false;
	// 			}
	// 		} // if dialogue open but 3rd dialogue
    //     }	// C key

	// 	if (dialogueState.GetBool("isOpen"))
	// {
	// 	if(sentence != 7) lookAroundActivated = false;
	// 	else {
	// 		button.interactable = false;
	// 	}
	// }

	if (dialogueState.GetBool("isOpen")) {
		if(sentence == 7) {
		button.interactable = false;
		if (Input.GetKeyDown (KeyCode.C)) 
			{
			cPressed = true;
			if(!lookAroundActivated) lookAroundActivated = true;
			else lookAroundActivated = false;
			}
		} else lookAroundActivated = false;
	} else {
		if (Input.GetKeyDown (KeyCode.C)) {
			if(!lookAroundActivated) lookAroundActivated = true;
			else lookAroundActivated = false;
		}
	}

	if (cPressed) button.interactable = true;


		// focus on fire element
		if(sentence == 6 || sentence == 5) cameraChange(targetFire);
		else if(sentence == 4) cameraChange(targetEarth);
		else if(sentence == 3) cameraChange(targetLava);
		else if(sentence == 1) cameraChange(targetHuman);


		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		if(lookAroundActivated) {
		camIcon.sprite = unlockedCam;

		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * dstFromTarget;

		handleZoom();

		} else {
		camIcon.sprite = lockedCam;
		dstFromTarget = 4;
		if (sentence > 6 || sentence < 1) transform.LookAt(target);
		}

		if (islandIsMoving) StartCoroutine("Shake");
	}

	private void cameraChange(Transform target) {
		float speed = 1.0f;
		float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		transform.LookAt(target);
	}

	private void handleZoom() {

				//control dist from target with scroll
		float zoomChangeAmount = 80f;
		 if (Input.GetAxis("Mouse ScrollWheel") < 0) // back zoom out
        {
			//Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize-1, 1);
			//dstFromTarget -= Mathf.Max(Camera.main.orthographicSize-1, 1);
			dstFromTarget -= zoomChangeAmount * Time.deltaTime;
     	}
     if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward zoom in
     {
        	//Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize-1, 6);
			dstFromTarget += zoomChangeAmount * Time.deltaTime;
     }

	 dstFromTarget = Mathf.Clamp(dstFromTarget, 3.7f, 8.3f);

	}

    public IEnumerator Shake()
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            // float x = Random.Range(this.transform.position.x, this.transform.position.x+1f) * magnitude;
            // float y = Random.Range(this.transform.position.y, this.transform.position.y+1f) * magnitude;

            // transform.position = new Vector3(x, y, -10f);
            // elapsed += Time.deltaTime;
            // yield return 0;

		elapsed += Time.deltaTime;          
        
        float percentComplete = elapsed / duration;         
        //float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

		float x = Random.Range(orignalPosition.x - magnitude, orignalPosition.x + magnitude);
        float y = Random.Range(orignalPosition.y - magnitude, orignalPosition.y + magnitude);
        // x *= magnitude * damper;
        // y *= magnitude * damper;
        
        Camera.main.transform.position = new Vector3(x, y, orignalPosition.z);
            
        yield return 0;
        }
        transform.position = orignalPosition;
    }


	// void OnEnable()
	// {
	// 	originalPos = camTransform.localPosition;
	// }

	// private void cameraShake() {
	// 			if (shakeDuration > 0)
	// 	{
	// 		camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
	// 		shakeDuration -= Time.deltaTime * decreaseFactor;
	// 	}
	// 	else
	// 	{
	// 		shakeDuration = 0f;
	// 		camTransform.localPosition = originalPos;
	// 	}
	// }
}
