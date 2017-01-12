using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class Samurai_Control : MonoBehaviour {

	private GameObject samurai;
	private GameObject warrior;

	float speed = 30.0f;
	float MinDist = 60.0f;

	float damage = 10f;
	float maxHealth = 100f;
	float currHealth = 0f;

	bool attack = true;
	bool finish = false;

	// Use this for initialization
	void Start () {
		currHealth = maxHealth;
		samurai = GameObject.FindGameObjectWithTag ("Samurai");
		warrior = GameObject.FindGameObjectWithTag ("Warrior");
	}
	
	// Update is called once per frame
	void Update () {

		if (finish == false) {
			// Get the Vuforia StateManager
			StateManager sm = TrackerManager.Instance.GetStateManager ();

			// Query the StateManager to retrieve the list of
			// currently 'active' trackables 
			//(i.e. the ones currently being tracked by Vuforia)
			IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

			foreach (TrackableBehaviour tb1 in activeTrackables) {
				if (tb1.TrackableName == "Queen") {

					if (Vector3.Distance (transform.position, warrior.transform.position) > MinDist) {

						samurai.GetComponent<Animation> ().CrossFade ("Walk");
						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (warrior.transform.position - transform.position), 3 * Time.deltaTime);
						transform.position = Vector3.MoveTowards (transform.position, warrior.transform.position, speed * Time.deltaTime);

					} else if ((Vector3.Distance (transform.position, warrior.transform.position) <= MinDist)) {
						foreach (TrackableBehaviour tb in activeTrackables) {
							if (tb.TrackableName == "King" && attack == true) {
								StartCoroutine (checkplay (samurai.GetComponent<Animation> ()));
								break;
							}
						}
					} else {
						samurai.GetComponent<Animation> ().CrossFade ("idle");
					}
					//Debug.Log ("samurai: " + currHealth);
					break;
				}
			}
		} else {
			samurai.GetComponent<Animation> ().CrossFade ("idle");
		}
	}

	public IEnumerator checkplay(Animation animation){
		attack = false;
		samurai.GetComponent<Animation> ().Play ("Attack");
		yield return new WaitForSeconds (animation["Attack"].length/2);
		warrior.GetComponent<WarriorHealth> ().TakeDamage (damage);
		yield return new WaitForSeconds (animation["Attack"].length/2);
		attack = true;
	}

	public void TakeDamage(float amount){
		currHealth -= amount;
	}

	public void finishGame(){
		finish = true;
	}
}
