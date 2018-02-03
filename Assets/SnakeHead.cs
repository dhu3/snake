using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeHead : MonoBehaviour {
	public GameObject FoodPrefrab;
	public Initialization ScriptInit;

	// Use this for initialization
	void Awake () {
		ScriptInit.CreateFood ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c){
		if (c.collider.name.Contains ("Wall")) {
			SceneManager.LoadScene ("snake");
		} else if (c.collider.name.Contains ("Body")) {
			SceneManager.LoadScene ("snake");
		} else if (c.collider.name.Contains ("Food")) {
			ScriptInit.CreateFood ();
			ScriptInit.SnakeGrow ();
		}
	}
}
