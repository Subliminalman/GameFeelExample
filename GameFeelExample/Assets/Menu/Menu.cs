using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	public Player[] players; //References to all player characters
	public GameObject menu; //Menu GUI
	public GameObject editButton; //Reference to editButton object
	public InputField runInput, jumpInput, startInput, endInput, additionalJumpSpeedInput, additionalJumpTimeInput; //Text Input for player parameters
	public Dropdown worldDropDown, playerDropDown; //Dropdown boxes to change player and world
	public Toggle jumpGraphToggle, runGraphToggle; //Toggles to turn on and off the graphs
	public GameObject jumpGraph, runGraph; //References to the line graphs in the scene
	public GameObject[] worldArray; //Reference to all the world scenes

	int currentWorld = 0; //Which world is active
	int currentPlayer = 0; //Which player is active

	//Setup scene
	void Awake () {
		ChangePlayer (0);
		ChangeWorld (0);
	}

	//Callback to change players run speed
	public void OnRunChange () {
		float run = 0f;
		if (float.TryParse(runInput.text, out run)) {
			players[currentPlayer].speed = run;
		}
	}

	//Callback to change players jumpVelocity
	public void OnJumpChange () {
		float jump = 0f;
		if (float.TryParse(jumpInput.text, out jump)) {
			players[currentPlayer].jumpVelocity = jump;
		}
	}

	//Callback to change players startGravity
	public void OnStartChange () {
		float start = 0f;
		if (float.TryParse(startInput.text, out start)) {
			players[currentPlayer].startGravity = start;
		}
	}

	//Callback to change players endGravity
	public void OnEndChange () {
		float end = 0f;
		if (float.TryParse(endInput.text, out end)) {
			players[currentPlayer].endGravity = end;
		}
	}

	//Callback to change players additionalJumpSpeed
	public void OnJumpSpeedInput () {
		float jumpSpeed = 0f;
		if (float.TryParse(additionalJumpSpeedInput.text, out jumpSpeed)) {
			players[currentPlayer].additionalJumpSpeed = jumpSpeed;
		}
	}

	//Callback to change players maxAdditionalJump
	public void OnJumpTimeInput () {
		float jumpTime = 0f;
		if (float.TryParse(additionalJumpTimeInput.text, out jumpTime)) {
			players[currentPlayer].maxAdditionalJump = jumpTime;
		}
	}

	//Callback to change world from worldDropDown menu
	public void OnWorldChange () {
		currentWorld = worldDropDown.value;
		ChangeWorld(currentWorld);
	}

	//Change the world
	void ChangeWorld (int _worldIndex) {
		if (_worldIndex < 0 || _worldIndex >= worldArray.Length || worldArray.Length == 0) {
			return;
		}

		for(int i = 0; i < worldArray.Length; i++) {
			worldArray[i].SetActive(false);
			if (_worldIndex == i) {
				worldArray[i].SetActive(true);
			}
		}
	}

	//Callback to change player from playerDropDown menu
	public void OnPlayerChange () {
		currentPlayer = Mathf.Clamp(playerDropDown.value, 0, players.Length - 1);		
		ChangePlayer (currentPlayer);
	}

	// Change the player and setup the menu to use the players values
	void ChangePlayer (int _playerIndex) {
		if (_playerIndex < 0 || _playerIndex >= players.Length || players.Length == 0) {
			return;
		}

		for (int i = 0 ; i < players.Length; i++) {
			players[i].gameObject.SetActive(false);
			if (i == _playerIndex) {
				players[i].gameObject.SetActive (true);
				players[i].Setup();
				runInput.text = "" + players[i].speed;
				jumpInput.text = "" + players[i].jumpVelocity;
				startInput.text = "" + players[i].startGravity;
				endInput.text = "" + players[i].endGravity;
				additionalJumpSpeedInput.text = "" + players[i].additionalJumpSpeed;
				additionalJumpTimeInput.text = "" + players[i].maxAdditionalJump;
				jumpGraph.GetComponent<JumpLine>().player = players[i];
				runGraph.GetComponent<RunLine>().player = players[i];
			}
		}
	}

	//Callback to turn the JumpGraph on and off
	public void OnJumpGraphChange () {
		jumpGraph.SetActive (jumpGraphToggle.isOn);
	}

	//Callback to turn the RunGraph on and off
	public void OnRunGraphChange () {
		runGraph.SetActive (runGraphToggle.isOn);
	}
}
