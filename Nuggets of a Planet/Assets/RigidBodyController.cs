using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
   //tutorial reference -- https://medium.com/ironequal/unity-characteracter-controller-vs-rigidbody-a1e243591483 and https://docs.huihoo.com/unity/3.3/Documentation/ScriptReference/Input.GetAxis.html and https://www.noob-programmer.com/unity3d/how-to-make-player-object-jump-in-unity-3d/

    public Rigidbody character;
    public float speed = 2f;
    public float jumpSpeed = 5f;
    public Vector3 jump;

    private float rotateXAxis = 0.0f;
    private float rotateYAxis = 0.0f;
    
    //private Eat eat;

    public bool isGrounded;
 

    
    // Start is called before the first frame update
    void Start()
    {
       // if(this.gameObject.name == "Player")
        // eat = gameObject.GetComponent<Eat>();
        // else eat = null;
        
        character = GetComponent<Rigidbody>();
        //character.constraints = RigidbodyConstraints.FreezePositionY;
        rotateXAxis = character.transform.eulerAngles.x;
        rotateYAxis = character.transform.eulerAngles.y;
        jump = new Vector3(0.0f, 2.0f, 0.0f);

    }
    
    void Update() {
    }

    public void Locomote(Vector3 direction)
    {
        direction.y = 0;

        direction = direction.normalized;
      
        direction.z *= speed * Time.deltaTime;
        direction.x *= speed * Time.deltaTime;

        character.transform.position += transform.right * direction.x;
        character.transform.position += transform.forward * direction.z;
    
    }

    
    public void Rotate()
    {
            rotateYAxis += 3 * Input.GetAxis("Mouse X");
            rotateXAxis += 3 * Input.GetAxis("Mouse Y");
        
        character.transform.rotation = Quaternion.Euler(0, rotateYAxis, 0);

        // Become cam = GameObject.Find("Camera_Become").GetComponent<Become>();

        // if (cam.GetCamMode() == 1)
        // {
        //     cam.gameObject.transform.rotation = Quaternion.Euler(rotateXAxis, rotateYAxis, 0);
        // }
        // else
        // {
        //     cam.gameObject.transform.rotation = Quaternion.Euler(33+ rotateXAxis, rotateYAxis, 0);
        //     }
        }

    public void Jump()
    {
//        character.constraints = RigidbodyConstraints.None;
        if (isGrounded)
        {
            //character.constraints = RigidbodyConstraints.None;
            character.AddForce(jump * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
       
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Island") && isGrounded == false)
        {
            isGrounded = true;
        }
    }
    
}
