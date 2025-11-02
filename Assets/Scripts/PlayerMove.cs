using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public float momentumDamping = 5f;

    private CharacterController controller;
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();

        camAnim.SetBool("isWalking", isWalking);
    }

    private void GetInput()
    {
        // If holding down wasd, then give us -1, 0, 1
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            // If not, then give us whatever inputVector was at when it was last checked and lerp it towards zero
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);

            isWalking= false;
        }

        movementVector = (inputVector * speed) + (Vector3.up * gravity);
    }

    private void MovePlayer()
    {
        controller.Move(movementVector * Time.deltaTime);
    }
}
