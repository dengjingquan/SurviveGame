using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*********************************
 @Description   :   玩家移动控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.11.30
********************************/

public class PlayerMoveController : MonoBehaviour
{

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes;
    public float speed = 6f;            
    Vector3 movement;                   
    Animator anim;                      
    Rigidbody playerRigidbody;          
    public float gravity = 10;
    private CharacterController charController;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minimumVert = -15.0f;
    public float maximumVert = 45.0f;
    private float rotationX = 0;
    private Vector3 moveDirection = Vector3.zero;


    void Awake()
    {

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        
    
    }

    private void Update()
    {
        if (axes == RotationAxes.MouseX)
        {   // 仅水平转向
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // 仅垂直转向            
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
        else
        {
            // 垂直水平转向          
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }

    void FixedUpdate()
    {
        if (charController.isGrounded)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed); //**** 将对角移动的速度限制为和沿着轴移动的速度一样
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);  //**** 把movement向量从本地变换为全局坐标
            charController.Move(movement);                      //**** 告知CharacterController通过movement向量移动
            // Animate the player.
            Animating(deltaX, deltaZ);
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * Time.deltaTime);
        }    
    }


    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}


