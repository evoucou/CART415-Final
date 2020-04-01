using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	//public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
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

		lookAroundActivated = false;

		// 		if (camTransform == null)
		// {
		// 	camTransform = GetComponent(typeof(Transform)) as Transform;
		// }

		IslandScript = island.GetComponent<IslandBehaviour>();
	}

	void FixedUpdate () {
		islandIsMoving = IslandScript.islandMoving();

        if (Input.GetKeyDown (KeyCode.C)) {
			if(!lookAroundActivated) lookAroundActivated = true;
			else lookAroundActivated = false;
        }

		if(lookAroundActivated) {
		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * dstFromTarget;

		handleZoom();

		} else {
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target);
		}


		if (islandIsMoving) StartCoroutine("Shake");
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

	 dstFromTarget = Mathf.Clamp(dstFromTarget, 4f, 8f);

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
