using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    MasterController MasterTemp;

	// Use this for initialization
	void Start () {
        MasterTemp = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CallRebirth(float x)
    {
        Invoke("Rebirth", x);
        if (this.gameObject.tag == "MyEnemyTag")
        {
            MasterTemp.ScorePlusPlus();
        }
    }


    void Rebirth()
    {
        //if (this.gameObject.tag == "MyEnemyTag")
        //{
            Destroy(this.gameObject.transform.parent.gameObject);
            MasterTemp.CallNewEnemyCreation();
        //}
        //if (this.gameObject.tag == "OtherEnemyTag")
        //{
        //    Destroy(this.gameObject.transform.parent.gameObject);
        //    MasterTemp.CallNewEnemyCreation();
        //}
    }

}
