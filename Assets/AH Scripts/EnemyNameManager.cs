using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNameManager : MonoBehaviour {

    public Text NameText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        NameText.text = "PLAYER " + Random.Range(0, 9).ToString() + Random.Range(0, 9).ToString();
    }


}
