using UnityEngine;
using System.Collections;

public class FinishGame : MonoBehaviour {

	private GameObject samurai;
	private GameObject warrior;
	bool end = false;

	// Use this for initialization
	void Start () {
		samurai = GameObject.FindGameObjectWithTag ("Samurai");
		warrior = GameObject.FindGameObjectWithTag ("Warrior");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void endGame(string Deadplayer){
		if(end==false){
		samurai = GameObject.FindGameObjectWithTag ("Samurai");
		warrior = GameObject.FindGameObjectWithTag ("Warrior");

		samurai.GetComponent<Samurai_Control> ().finishGame ();
		warrior.GetComponent<WarriorControl> ().finishGame ();
		if (Deadplayer == "Samurai") {
		} else {
			//StartCoroutine (KillWarrior (warrior.GetComponent<Animation> ()));
			warrior.GetComponent<Animation> ().Play ("Die");
				end = true;
				Debug.Log (end);
		}
		//Destroy (samurai);
		//Destroy (warrior);
		}
	}

	public IEnumerator KillWarrior(Animation animation){
		warrior.GetComponent<Animation> ().Play ("Die");
		yield return new WaitForSeconds (animation["Die"].length);
		Destroy (warrior);
	}
}
