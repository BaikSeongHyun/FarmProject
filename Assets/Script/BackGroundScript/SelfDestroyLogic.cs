using UnityEngine;
using System.Collections;

public class SelfDestroyLogic : MonoBehaviour
{
	//complex data field
	public GameObject boomEffect;

	//destroy object
	void OnCollisionEnter( Collision col )
	{
		if (col.gameObject.layer.Equals( LayerMask.NameToLayer( "Terrain" ) )
		    || col.gameObject.layer.Equals( LayerMask.NameToLayer( "Human" ) ))
		{
			GameObject effect = Instantiate( boomEffect, transform.position, Quaternion.identity ) as GameObject;
			Destroy( this.gameObject );
			Destroy( effect, 2f );
		}
	}
}
	