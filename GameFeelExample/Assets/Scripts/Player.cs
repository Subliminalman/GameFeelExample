using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public bool onGround = false;  //OnGround Sets the ability to jump so we don't get infinite jumps	
	public float speed; //Running speed	
	public float jumpVelocity; //How high to jump
	public float startGravity; //The Gravity acting on the player before their upVelocity is < 0
	public float endGravity; //The Gravity acting on the player after their upVelocity is < 0
	public float additionalJumpSpeed; //How fast do we get to the maxAdditionalJump
	public float maxAdditionalJump; //Allows for pressing and holding the jump button to jump higher or short press and shallow jumps
	public BoxCollider collider; //The Players collider
	public Animator animator; //The Players Animator State Machine
	[HideInInspector]
	public float direction; //Which way are we facing

	bool holdButton; //Is the button held down
	int floorMask = 0; //BitMask to only check for floor
	float upVelocity = 0f; //The players upVelocity. Used for jumping and falling
	float additionalJump = 0f; //Used for pressing and holding the jump button
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		//Get BoxCollider
		if (collider == null) {
			collider = GetComponent<BoxCollider>();
		}
		//Set the floor mask
		floorMask = 1 << LayerMask.NameToLayer("Floor");
	}

	//Reset position to the ground and reset the upVelocity
	public void Setup () {
		transform.position = Vector3.up * 0.5f;
		upVelocity = 0f;
		onGround = true;
	}

	void Update () {
		//Get left and right on the keyboard and controller
		direction = Input.GetAxis ("Horizontal");

		//Make character run based on direction and speed
		transform.position += Vector3.right * direction * speed * Time.deltaTime;

		//Make the character react to gravity
		if (!onGround) {
			//Check if the player is still moving up if so use startGravity if not use endGravity
			upVelocity -= (upVelocity > 0 ? startGravity : endGravity) * Time.deltaTime;
		} 
	
		//Make character jump
		if (Input.GetKeyDown (KeyCode.Space) && onGround) {
			upVelocity = jumpVelocity;
		}

		//Check to see if jump button is still pressed
		if (Input.GetKey (KeyCode.Space) && additionalJump < maxAdditionalJump) {
			holdButton = true;
			additionalJump += additionalJumpSpeed * Time.deltaTime;

		} else {
			if (holdButton) {
				upVelocity = 0f;
				holdButton = false;
			}
		}

		//Move vertically character based on gravity and upVelocity
		transform.position += Vector3.up * upVelocity * Time.deltaTime;
		
		//Check for the ground by raycasting down from the left and right of the character
		if(Physics.Raycast (transform.position + Vector3.right * 0.5f, Vector3.down, out hit, 0.7f, floorMask) || 
		   Physics.Raycast (transform.position + Vector3.left  * 0.5f, Vector3.down, out hit, 0.7f, floorMask)) {
			if (onGround == false) {
				//Set the position of the character so they aren't in the middle of a tile
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

	//Keep the player on the screen
	void KeepInBounds () {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), Mathf.Clamp (transform.position.y, 0f, 15f), 0f);
	}
	
	//Use the characters animator to animate the character. This one only accounts for direction.
	void Animate () {
		if (onGround) {
			animator.SetFloat("speed", direction);
		} 
	}
}
