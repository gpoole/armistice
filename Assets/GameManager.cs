using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public SalvageShip activeShip;

	protected Text damage;

	protected Text fuel;

	// Use this for initialization
	void Start () {
		damage = GameObject.Find ("/UI/Damage").GetComponent<Text>();
		fuel = GameObject.Find ("/UI/Fuel").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		damage.text = "Damage: " + Mathf.RoundToInt(activeShip.damage / activeShip.maxDamage * 100) + "%";
		fuel.text = "Fuel: " + Mathf.RoundToInt (activeShip.fuel / activeShip.maxFuel * 100) + "%";
	}
}
