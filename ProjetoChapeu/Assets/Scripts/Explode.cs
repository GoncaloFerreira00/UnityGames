using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	public GameObject Explosion;
	public ParticleSystem[] effects; 

	void OnCollisionEnter2D (Collision2D collision){
		GameObject gb=Instantiate (Explosion, transform.position, transform.rotation);
		gb.transform.parent = null;
		Destroy(gb, 5.0f);
		foreach (var effect in effects) {
			effect.transform.parent = null;
			effect.Stop ();
			Destroy (effect.gameObject, 2.0f);
		}
		Destroy (gameObject);

	}



}
