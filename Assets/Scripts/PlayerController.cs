using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10f;
	public float padding = 1f;

	private float xMin;
	private float xMax;

	void Start() {
		float distance = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)); //Relative coordinates here. 1,1 would mean at the top right. 0,0 would be at the bottom left.
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}

	void Update () {
		MoveSpaceship();
	}

	void MoveSpaceship() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;//Default function to move to the left
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		RestrictMovement();
	}

	void RestrictMovement() {
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
