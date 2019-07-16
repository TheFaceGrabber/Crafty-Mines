using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour {

    public float MouseSensitivity = 2;
    public float MoveSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        float y = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;

        transform.eulerAngles += new Vector3(-y, x, 0);

        float h = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float v = Input.GetAxisRaw("Vertical") * MoveSpeed;

        transform.position += transform.right * h * Time.deltaTime;
        transform.position += transform.forward * v * Time.deltaTime;
    }
}
