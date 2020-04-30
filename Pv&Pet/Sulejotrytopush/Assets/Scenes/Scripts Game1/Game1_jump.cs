using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1_jump : MonoBehaviour
{
	public float speed = 1;
	private Rigidbody rb;
	public LayerMask groundLayers;
	public float jumpForce = 10;
	public SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
		
        float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal,0,moveVertical);
		rb.AddForce(movement * speed);
		
		if(IsGrounded()&& Input.GetKeyDown(KeyCode.Space) || (IsGrounded()&& Input.touchCount>0)) //alt+124
		{
			rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
			rb.velocity=rb.velocity*3.5f;
		}

    }
	private bool IsGrounded()
	{
		return Physics.CheckCapsule(col.bounds.center,new Vector3(col.bounds.center.x,col.bounds.min.y,col.bounds.center.z),
		col.radius*.9f,groundLayers);
	}
}
