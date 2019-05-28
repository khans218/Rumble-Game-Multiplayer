using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;

public class SphereManager : MonoBehaviour {

    private bool OneTime = false;
    Invector.vCharacterController.AI.v_AIController HealthTemp;

    // Use this for initialization
    void Start () {
        Physics.IgnoreCollision(this.transform.GetComponentInParent<CapsuleCollider>(), GetComponent<Collider>(),true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Enemy" && OneTime == false)
    //    {
    //        HealthTemp = other.GetComponent<Invector.vCharacterController.AI.v_AIController>();
    //        //HealthTemp.CustomCurrentHealth(0f);
    //        if (HealthTemp != null)
    //        {
    //            HealthTemp.CustomCurrentHealth(0f);
    //        }
    //        this.GetComponent<SphereCollider>().isTrigger = false;
    //        OneTime = true;

    //    }
    //    else if (other.tag == "Boxes" && OneTime == false)
    //    {
    //        this.GetComponent<SphereCollider>().isTrigger = false;
    //        OneTime = true;
    //    }
    //    StartCoroutine(TurnTriggerBack());
    //}

    //IEnumerator TurnTriggerBack()
    //{
    //    yield return new WaitForSeconds(1f);
    //    this.GetComponent<SphereCollider>().isTrigger = true;
    //    OneTime = false;
    //}


}
