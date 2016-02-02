using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (BoxCollider))]
[RequireComponent (typeof (SpriteRenderer))]
public class Coin : MonoBehaviour {
	public AudioClip pickupClip;
	BoxCollider collider;
	AudioSource audioSource;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider>();
		audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	IEnumerator WaitAndRespawn () {

		yield return new WaitForSeconds (10f);

		spriteRenderer.enabled = true;
		transform.localScale = Vector3.zero;

		float time = 0f;
		while (time < 1f) {
			time += Time.deltaTime;
			transform.localScale = Vector3.one * (time / 1f);
			yield return null;
		}

		collider.enabled = true;

	}

	void OnCollisionEnter (Collision _col) {
		if (_col.collider.CompareTag("Player")) {
			if(pickupClip) {
				audioSource.PlayOneShot(pickupClip);
			}

			collider.enabled = false;
			spriteRenderer.enabled = false;
			StartCoroutine (WaitAndRespawn());
		}
	}
}
