using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;

public class GarbageCollector : MonoBehaviour {

    MasterController MasterTemp;


	// Use this for initialization
	void Start () {
        MasterTemp = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject par = null;
        if (other.tag == "MyEnemyTag" || other.tag == "OtherEnemyTag" || other.tag == "Player")
        {
            if (other.transform.parent == null)
            {
                par = other.gameObject;
            } else
            {
                par = other.transform.parent.gameObject;
            }
        }
        if (par == null || !par.GetComponent<vHealthController>()) return;
        par.GetComponent<vHealthController>().CustomCurrentHealth(0f);
        /*
        if (other.tag == "MyEnemyTag")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            //MasterTemp.CallNewEnemyCreation();
        }
        if (other.tag == "OtherEnemyTag")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            //MasterTemp.CallNewEnemyCreation();
        }

        if (other.tag == "Player")
        {
            if (MasterTemp.CurrentPlayer != other.transform)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            } else
            {
                MasterTemp.CallGameOver(2);
            }
        }
        */

    }

}
