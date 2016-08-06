using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10f;

	void Update () {
		MoveSpaceship();
	}

	void MoveSpaceship() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		}
	}
}
