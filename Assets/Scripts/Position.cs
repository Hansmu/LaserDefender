﻿using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	// Gizmos are things that aren't shown in the game, but shown to you as a developer.
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, 1); //Draw a sphere around our GameObject with a radius of 1.
	}
}
