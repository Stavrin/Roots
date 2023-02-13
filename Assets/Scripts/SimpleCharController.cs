using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using UnityEngine.XR;
///using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.InputSystem;
using System.ComponentModel;

public class SimpleCharController : MonoBehaviour
{
    public static SimpleCharController singleton; //static is a singleton, one instance only

    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    public float sensitivity = 5f;
    private float rotY = 0;

    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] CharacterController plController;
    [SerializeField] CharacterController vrController;

    public CinemachineVirtualCamera virtualCamera;
    public OVRCameraRig ovrCameraRig;

    public Animator animator;

    [SerializeField] PlayerAnimations playerAnim;

    //[SerializeField] public bool usingVR; //old way

    [SerializeField] OVRHeadsetEmulator headsetEmulator;

    [SerializeField] GameManager game;


    void Awake()
    {
        singleton = this;
        //DontDestroyOnLoad(this);
    }

    public static SimpleCharController GetInstance()
    {
        return singleton;
    }

    void Start()
    {
        game = GameManager.GetInstance();

        if (game.spinningMode)
            GetComponent<CapsuleCollider>().enabled = true;
        

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        //rotY = transform.localEulerAngles.y;

        //virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0; //stops character looking down at start

        if (game.usingVR)
            virtualCamera.enabled = false; //turn off virtual cam if using VR
        else
            virtualCamera.enabled = true;


 

    }

    void Update()
    {
        if (plController.isGrounded)
        {
            if (virtualCamera.enabled) //if virtual camera is active accept keyboard input
                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

            if (game.usingVR) //if the VRcam is being used, move the character in relation to that
            {
                moveDirection = (transform.forward * OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y) + (transform.right * OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x);

                //headset emulator
                moveDirection = (headsetEmulator.transform.forward * Input.GetAxis("Vertical")) + (headsetEmulator.transform.right * Input.GetAxis("Horizontal"));
            }

            moveDirection = moveDirection.normalized * speed; //transform forward and right are needed to make character move in direction they are facing


            //moveDirection.y = virtualCamera.transform.localPosition.y; //move in the direction the camera is facing, doesn't work

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (!game.usingVR)
        {
            float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity; //local needed to look l/r
            rotY += Input.GetAxis("Mouse Y") * sensitivity; //needs to not be local variable to be able to look u/d

            rotY = Mathf.Clamp(rotY, -90f, 90f); //important to limit how far you can look
            //transform.localEulerAngles = new Vector3(-rotY, rotX, 0); //causes the looking down immediately bug

            transform.localEulerAngles = new Vector3(0, rotX, 0); //move the character just horizontally

            if(game.jankyMode)
                transform.localEulerAngles = new Vector3(-rotY, rotX, 0);

            virtualCamera.transform.localEulerAngles = new Vector3(-rotY, rotX, 0); //move the virtual camera
        }
        else
        {
            Quaternion gaze = OVRInput.GetLocalControllerRotation(OVRInput.Controller.Touch);

            float rotX = transform.localEulerAngles.y + gaze.x + Input.GetAxis("Mouse X") * sensitivity;
            rotY += (gaze.y + Input.GetAxis("Mouse Y")) * sensitivity;
            rotY = Mathf.Clamp(rotY, -90f, 90f);

            transform.localEulerAngles = new Vector3(0, rotX, 0); //move the character just horizontally

            //emulated vr camera movement, can cause the spinning
            //headsetEmulator.transform.localEulerAngles = new Vector3(-rotY, rotX, 0);

            if(game.spinningMode)
                headsetEmulator.transform.localEulerAngles = new Vector3(-5f, rotX, 0);

        }

            

        moveDirection.y -= gravity * Time.deltaTime;

        if (Input.GetAxis("Horizontal") != 0) //if there is horizontal input
        {
            //moveDirection = transform.right * moveDirection.x + transform.forward * moveDirection.y; //to make movement always in the right direction, doesn't work
        }

        plController.Move(moveDirection.normalized * Time.deltaTime);

        //move the vr camera in relation to the player.
        //vrController.Move(moveDirection.normalized * Time.deltaTime);


        playerAnim.UpdateAnimation(plController.velocity.sqrMagnitude * 5);




    }
}
