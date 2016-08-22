using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float health = 150f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 50;

	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}

	void Update() {
		float probabilityOfFiring = shotsPerSecond * Time.deltaTime;

		if (Random.value < probabilityOfFiring) {
			Fire();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>(); //GameObject we're colliding with.

		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}

	void Fire() {
		Vector3 startPosition = transform.position + new Vector3(0, -1f, 0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
