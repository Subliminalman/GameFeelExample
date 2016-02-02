using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool onGround = false;
	public float speed;
	public float jumpVelocity;
	public float startGravity;
	public float endGravity;
	public float additionalJumpSpeed;
	public float maxAdditionalJump;
	public BoxCollider collider;
	public Animator animator;
	[HideInInspector]
	public float direction;

	bool holdButton;
	int floorMask = 0;
	float upVelocity = 0f;
	float additionalJump = 0f;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		if (collider == null) {
			collider = GetComponent<BoxCollider>();
		}
		floorMask = 1 << LayerMask.NameToLayer("Floor");
	}

	public void Setup () {
		transform.position = Vector3.up * 0.5f;
		upVelocity = 0f;
		onGround = true;
	}

	void Update () {
		direction = Input.GetAxis ("Horizontal");

		transform.position += Vector3.right * direction * speed * Time.deltaTime;

		if (!onGround) {
			upVelocity -= (upVelocity > 0 ? startGravity : endGravity) * Time.deltaTime;
		} 
	
		if (Input.GetKeyDown (KeyCode.Space) && onGround) {
			upVelocity = jumpVelocity;
		}

		if (Input.GetKey (KeyCode.Space) && additionalJump < maxAdditionalJump) {
			holdButton = true;
			additionalJump += additionalJumpSpeed * Time.deltaTime;

		} else {
			if (holdButton) {
				upVelocity = 0f;
				holdButton = false;
			}
		}

		transform.position += Vector3.up * upVelocity * Time.deltaTime;

		if (transform.position.y < 0f) {
			transform.position = new Vector3(transform.position.x, 0f, 0f);
		}

		if(Physics.Raycast (transform.position + Vector3.right * 0.5f, Vector3.down, out hit, 0.7f, floorMask) || 
		   Physics.Raycast (transform.position + Vector3.left  * 0.5f, Vector3.down, out hit, 0.7f, floorMask)) {
			if (onGround == false) {
				transform.position = new Vector3 ( transform.position.x, hit.collider.bounds.max.y + collider.bounds.extents.y, transform.position.z);
			}
			upVelocity = 0f;
			onGround = true;
		} else {
			onGround = false;
		}

		Animate ();

		KeepInBounds ();
	}

	void KeepInBounds () {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), Mathf.Clamp (transform.position.y, 0f, 15f), 0f);
	}

	void Animate () {
		if (onGround) {
			animator.SetFloat("speed", direction);
		} 
	}
}
