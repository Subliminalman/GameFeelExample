using UnityEngine;
using System.Collections;

public class JumpLine : MonoBehaviour {
	public LineRenderer jumpLine;

	Vector3 [] jumpVectors; //Points on the jumpLine
	
	public Player player; //Reference to the player

	//Setup the jumpLine's width, each point on the line, and the amount of points
	void Start () {
		
		jumpLine.SetWidth (0.1f, 0.1f);
		jumpVectors = new Vector3[100];


		for (int i = 0; i < 100; i++) {
			jumpVectors[i] = new Vector3((float)100 - i / 100f, 0f, 0f);		
		}

		jumpLine.SetVertexCount(100);
	}
	
	//Every frame set the array of points based on time and y position of the player
	void Update () {
		jumpVectors[jumpVectors.Length - 1] = new Vector3 (0f, player.transform.position.y, 0f);

		for (int i = 0; i < 99; i++) {
			jumpVectors[i] = new Vector3 ((((float) i/ 99f) * 10f) - 10f, jumpVectors[i + 1].y, 0f);
		}

		for (int i = 0; i < jumpVectors.Length; i++) {
			jumpLine.SetPosition(i, jumpVectors[i]);
		}
	}
}
