using UnityEngine;
using System.Collections;

public class WarriorHealth : MonoBehaviour {

	public float MaxHealth = 100f;
	public float CurrHealth = 0f;
	public GameObject healthBar;
	FinishGame game;
	bool end = false;

	// Use this for initialization
	void Start () {
		CurrHealth = MaxHealth;
		game = new FinishGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(float amount){
		CurrHealth -= amount;
		float calc_health = CurrHealth / MaxHealth;
		SetHealthBar (calc_health);

		if (CurrHealth == 0 && end==false) {
			game.endGame ("Warrior");
			end = true;
		}
	}

	void SetHealthBar(float myHealth){
		//myhealth 0-1, 
		healthBar.transform.localScale = new Vector3(myHealth,healthBar.transform.localScale.y,healthBar.transform.localScale.z);
	}
}
