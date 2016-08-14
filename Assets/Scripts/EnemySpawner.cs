using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;

	private bool movingRight = false;
	private float xMax;
	private float xMin;

	// Use this for initialization
	void Start () {
		GetFieldConstraints();
		SpawnEnemies();
	}

	// Update is called once per frame
	void Update () {
		DirectMovement();

		if (AreAllMembersDead()) {
			SpawnEnemiesUntilFull();
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3 (width, height)); //Shows us where the formation is. Draws a cube around it.
	}

	void GetFieldConstraints() {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		xMax = rightEdge.x;
		xMin = leftEdge.x;
	}

	void SpawnEnemies() {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject; //Quaternion is for rotation. identity leaves it at the default.
			enemy.transform.parent = child;
		}
	}

	void SpawnEnemiesUntilFull() {
		Transform freePosition = NextFreePosition();
		if (freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject; //Quaternion is for rotation. identity leaves it at the default.
			enemy.transform.parent = freePosition;
		}

		if (NextFreePosition ()) {
			Invoke("SpawnEnemiesUntilFull", spawnDelay);
		}
	}

	void RestrictMovement() {
		float rightEdgeOfFormation = transform.position.x + 0.5f * width;
		float leftEdgeOfFormation = transform.position.x - 0.5f * width;

		if (leftEdgeOfFormation < xMin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xMax) {
			movingRight = false;
		}
	}

	void DirectMovement() {
		if (movingRight) {
			transform.position += Vector3.right * (speed * Time.deltaTime);
		} else {
			transform.position += Vector3.left * (speed * Time.deltaTime);
		}

		RestrictMovement();
	}

	bool AreAllMembersDead() {
		foreach (Transform childPositionGameObject in transform) { //transform of the gameObject holds the children.
			if (childPositionGameObject.childCount > 0) { //If 0, then members are dead.
				return false;
			}
		}

		return true;
	}

	Transform NextFreePosition() {
		foreach (Transform childPositionGameObject in transform) { 
			if (childPositionGameObject.childCount == 0) { 
				return childPositionGameObject;
			}
		}

		return null;
	}
}
