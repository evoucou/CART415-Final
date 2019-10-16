using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    //camera switching variables
    //private Camera fpsCam;
    private GameObject player;

    //Action Scripts
    //public Spawner actionSpawn;
    // private Pickupper actionPickup;
    // private Eat actionEat;
    // private Throw actionThrow;
    // private ShieldTimer stimer;
    // public GameObject st;

    // private MeshRenderer render;
    // private Collider collider;


    void Start()
    {  

        //fpsCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
    PlayerActions();
    }

    //player actions
    private void PlayerActions()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        RigidBodyController controller = player.GetComponent<RigidBodyController>();

        controller.Locomote(new Vector3(horizontal, 0, vertical));
        controller.Rotate();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump();
        }

              // KeyCode for Marie-Eve and Audrey's milestone
//        if (Input.GetKeyDown(KeyCode.M))
//        {
//            if(actionShield.collectedShield()) {
//            if (!shieldActivated){
//            shieldActivated = true;
//            actionPickup.dropIt();          
//            } else {
//            shieldActivated = false;
//                }
//            } else return;
//
//        }


    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        if (actionPickup && actionPickup.IsHoldingObject())
    //        {
    //            Usable usable = actionPickup.HeldObject().GetComponent<Usable>();
    //            if (usable)
    //            {
    //                usable.Use();
    //            }
    //        }
    //    }

//
//                if (Input.GetKeyDown(KeyCode.F))
//        {
//
//           if(actionPickup.IsHoldingKey()) {
//               open.openDoor();
//           }
//
      //}

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     //call eat function

        //         actionEat.EatFood();

        // }
//        if (Input.GetKeyDown(KeyCode.T))
//        {
//            if (actionPickup.IsHoldingObject())
//            {
//                print("throw");
//                actionThrow.ThrowObject();
//            }
//        }
        //... more actions
    }

}
