using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSensor : MonoBehaviour
{

	public GameObject AttackSphere;
	bool OneTime = true;
	[HideInInspector]public Animator Anim;

	// Use this for initialization
	void Start ()
	{
		Anim = this.gameObject.transform.parent.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BlastReceiver") {
			print (other.gameObject.name);
			if (OneTime) {
				StartCoroutine (AttackHim (Random.Range (1f, 5f)));
				OneTime = false;
			}
		}
	}

	IEnumerator AttackHim (float Time)
	{
		yield return new WaitForSeconds (Time);
		if (AttackSphere != null) {
			AttackSphere.SetActive (true);
			Anim.Play ("N01");
		}
		StartCoroutine (ChangeBack ());
	}

	IEnumerator ChangeBack ()
	{
		yield return new WaitForSeconds (1f);
		OneTime = true;
	}



}
