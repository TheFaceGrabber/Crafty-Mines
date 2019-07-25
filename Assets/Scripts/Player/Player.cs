using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float JumpSpeed;

    public float MoveSpeed;
    public float LookSpeed;

    public float MaxLookAngle = 80;
    public float MinLookAngle = -80;

    public Transform Camera;

    public LayerMask GroundMask;

    public bool isGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, Vector3.down, 1f, GroundMask);
        }
    }

    Rigidbody rigidbody;
    float angle = 0;

    private void Start()
    {
        ResourcePack.Load();

        rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        float x = Input.GetAxisRaw("Mouse X") * LookSpeed * Time.deltaTime;
        float y = Input.GetAxisRaw("Mouse Y") * LookSpeed * Time.deltaTime;


        if (angle > MaxLookAngle)
        {
            //y = 0;
            angle = MaxLookAngle;
        }
        else if(angle < MinLookAngle)
        {
            //y = 0;
            angle = MinLookAngle;
        }
        else
        {
            angle -= y;
        }

        Camera.localEulerAngles = new Vector3(angle, 0, 0);
        transform.localEulerAngles += new Vector3(0, x, 0);

        float h = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float v = Input.GetAxisRaw("Vertical") * MoveSpeed;

        var dir = transform.TransformDirection(new Vector3(h, 0, v) * MoveSpeed);

        rigidbody.velocity = new Vector3(dir.x, rigidbody.velocity.y, dir.z);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        }
    }
}
