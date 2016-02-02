using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	public Player[] players;
	public GameObject menu;
	public GameObject editButton;
	public InputField runInput, jumpInput, startInput, endInput, additionalJumpSpeedInput, additionalJumpTimeInput;
	public Dropdown worldDropDown, playerDropDown;
	public Toggle jumpGraphToggle, runGraphToggle;
	public GameObject jumpGraph, runGraph;
	public GameObject[] worldArray;

	int currentWorld = 0;
	int currentPlayer = 0;

	void Awake () {
		ChangePlayer (0);
		ChangeWorld (0);
	}

	public void OnRunChange () {
		float run = 0f;
		if (float.TryParse(runInput.text, out run)) {
			players[currentPlayer].speed = run;
		}
	}

	public void OnJumpChange () {
		float jump = 0f;
		if (float.TryParse(jumpInput.text, out jump)) {
			players[currentPlayer].jumpVelocity = jump;
		}
	}

	public void OnStartChange () {
		float start = 0f;
		if (float.TryParse(startInput.text, out start)) {
			players[currentPlayer].startGravity = start;
		}
	}

	public void OnEndChange () {
		float end = 0f;
		if (float.TryParse(endInput.text, out end)) {
			players[currentPlayer].endGravity = end;
		}
	}

	public void OnJumpSpeedInput () {
		float jumpSpeed = 0f;
		if (float.TryParse(additionalJumpSpeedInput.text, out jumpSpeed)) {
			players[currentPlayer].additionalJumpSpeed = jumpSpeed;
		}
	}

	public void OnJumpTimeInput () {
		float jumpTime = 0f;
		if (float.TryParse(additionalJumpTimeInput.text, out jumpTime)) {
			players[currentPlayer].maxAdditionalJump = jumpTime;
		}
	}

	public void OnWorldChange () {
		currentWorld = worldDropDown.value;
		ChangeWorld(currentWorld);
	}

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

	public void OnPlayerChange () {
		currentPlayer = Mathf.Clamp(playerDropDown.value, 0, players.Length - 1);		
		ChangePlayer (currentPlayer);
	}

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

	public void OnJumpGraphChange () {
		jumpGraph.SetActive (jumpGraphToggle.isOn);
	}

	public void OnRunGraphChange () {
		runGraph.SetActive (runGraphToggle.isOn);
	}
}
