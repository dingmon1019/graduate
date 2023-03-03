using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float speed = 16f;
    public float jumpForce = 300f;
    public int jumpLimit = 2;
    int jumpCount = 0;
    float jumpTimer = 0;

    public float mouseSensitivity;
    public Camera fpsCamera;
    float xRotation = 0f;

    public float stepRate = 3f;
    float nextTimeToStep;

    Vector3 prevPosition;

    void Start()
    {
        prevPosition = transform.position;
    }

    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        float inputMouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float inputMouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= inputMouseVertical; // x축의 움직임이 회전의 방향에서 반대로 작용하는 원리
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //회전 한계 설정

        fpsCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //위아래 카메라 이동(x축 자체를 움직임)
        transform.Rotate(0f, inputMouseHorizontal, 0f);


        Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical);

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(moveDirection*speed*Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        jumpTimer += Time.deltaTime;

        if(CheckOnGround())
        {
            jumpTimer = 0;
            jumpCount = 0;
        }

        //키 코드 사용
        //if(Input.GetKeyDown(KeyCode.Space))
        //    playerRigidbody.AddForce(Vector3.up*jumpForce);

        // InputManger 사용
        if(Input.GetButtonDown("Jump") && CheckJumpable())
        {
            jumpTimer = 0;
            jumpCount++;
            playerRigidbody.AddForce(Vector3.up*jumpForce);
        }

        if (Time.time >= nextTimeToStep && CheckOnGround() && CheckMoving())
        {
            nextTimeToStep = Time.time + 1f / stepRate;

        }

        prevPosition =  transform.position;
        
    }

    bool CheckMoving()
    {
        float distance = Vector3.Distance(transform.position, prevPosition);

        return distance > 0.01f;
    }

    bool CheckJumpable()
    {
        return (jumpCount < jumpLimit) && (jumpTimer == 0 || jumpTimer > 0.3f);
    }

    bool CheckOnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
