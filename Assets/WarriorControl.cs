using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class WarriorControl : MonoBehaviour {

	private GameObject samurai;
	private GameObject warrior;

	float damage = 5f;
	float maxHealth = 100f;
	float currHealth = 0f;

	float speed = 30.0f;
	float MinDist = 60.0f;
	static int count = 0;

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
			StateManager sm = TrackerManager.Instance.GetStateManager ();
			IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

			foreach (TrackableBehaviour tb1 in activeTrackables) {
				if (tb1.TrackableName == "King") {
				

					if (Vector3.Distance (transform.position, samurai.transform.position) > MinDist) {

						warrior.GetComponent<Animation> ().CrossFade ("Walk");
						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (samurai.transform.position - transform.position), 3 * Time.deltaTime);
						transform.position = Vector3.MoveTowards (transform.position, samurai.transform.position, speed * Time.deltaTime);

					} else if ((Vector3.Distance (transform.position, samurai.transform.position) <= MinDist)) {
						foreach (TrackableBehaviour tb in activeTrackables) {
							if (tb.TrackableName == "Queen" && attack == true) {
								StartCoroutine (checkplay (warrior.GetComponent<Animation> ()));
								break;
							}
						}
					} else {
						warrior.GetComponent<Animation> ().CrossFade ("Idle_01");
					}
					//Debug.Log ("warrior: " + currHealth);

					break;
				}
			}
		} else {
			//warrior.GetComponent<Animation> ().CrossFade ("Idle_01");
		}
	}

	public IEnumerator checkplay(Animation animation){
		attack = false;
		warrior.GetComponent<Animation> ().Play ("Attack_01");
		yield return new WaitForSeconds (animation["Attack_01"].length/2);
		samurai.GetComponent<SamuraiHealth> ().TakeDamage (damage);
		yield return new WaitForSeconds (animation["Attack_01"].length/2);
		attack = true;
	}

	public void TakeDamage(float amount){
		currHealth -= amount;
	}

	public void finishGame(){
		finish = true;
	}

}
