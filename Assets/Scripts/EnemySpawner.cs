using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;

	private bool movingRight = false;
	private float xMax;
	private float xMin;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		xMax = rightEdge.x;
		xMin = leftEdge.x;

		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject; //Quaternion is for rotation. identity leaves it at the default.
			enemy.transform.parent = child;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3 (width, height)); //Shows us where the formation is. Draws a cube around it.
	}

	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * (speed * Time.deltaTime);
		} else {
			transform.position += Vector3.left * (speed * Time.deltaTime);
		}

		float rightEdgeOfFormation = transform.position.x + 0.5f * width;
		float leftEdgeOfFormation = transform.position.x - 0.5f * width;

		if (leftEdgeOfFormation < xMin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xMax) {
			movingRight = false;
		}
	}
}
