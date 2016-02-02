using UnityEngine;
using System.Collections;

public class RunLine : MonoBehaviour {
	public LineRenderer accelerationLine;
	
	Vector3 [] accelerationVectors; //Points on the runLine
	
	public Player player; //Reference to player
	
	// Setup the runLine's width, each point on the line, and the amount of points
	void Start () {
		accelerationLine.SetWidth (0.1f, 0.1f);
		accelerationVectors = new Vector3[100];
		
		for (int i = 0; i < 100; i++) {
			accelerationVectors[i] = new Vector3((float) 100 - i / 100f, 0f, 0f);
		}

		accelerationLine.SetVertexCount(100);
	}
	
	//Every frame set the array of points based on time and the player direction of the player
	void Update () {	

		if (player == null) {
			return;
		}

		accelerationVectors[accelerationVectors.Length - 1] = new Vector3 (0f, player.direction, 0f);
		
		for (int i = 0; i < 99; i++) {
			accelerationVectors[i] = new Vector3 ((((float) i/ 99f) * 10f) - 10f, accelerationVectors[i + 1].y, 0f);
		}
		
		for (int i = 0; i < accelerationVectors.Length; i++) {
			accelerationLine.SetPosition(i, accelerationVectors[i]);
		}
	}

}
