using UnityEngine;
using System.Collections;

public class DestroyLogic : MonoBehaviour {

	//initialize this script
	void Start () {
	
	}

	//destroy object
	void OnCollisionEnter(Collision col)
	{
		Destroy( col.gameObject );
	}
}
