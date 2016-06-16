using UnityEngine;
using System.Collections;

public class DestroyLogic : MonoBehaviour
{
	//destroy object
	void OnCollisionEnter( Collision col )
	{
		Destroy( col.gameObject );
	}
}
