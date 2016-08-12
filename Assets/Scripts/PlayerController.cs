using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10f;
	public float padding = 1f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate;

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
		MoveSpaceshipIfClicked();
		PlayerFireIfClicked();
	}

	void PlayerFireIfClicked() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);//First parameter is method name. Second is the initial delay before first run if you put in zero, then it bugs. Third is repeat time.
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke();
		}
	}

	void Fire() {
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
	}

	void MoveSpaceshipIfClicked() {
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
