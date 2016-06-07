using UnityEngine;
using System.Collections;

public class GenerateClouds : MonoBehaviour
{
	//simple data field
	public GameObject cloud1;
	public GameObject cloud2;
	[Range( 0f, 10f )]public float cycle = 3f;


	//initialize this script
	void Start( )
	{
		StartCoroutine( GenerateCloud() );	
	}


	//IEnumerator -> make cloud
	IEnumerator GenerateCloud( )
	{
		//loop
		while (true)
		{
			//select cloud type
			int type = Random.Range( 0, 1 );

			//select create position
			float randomX = Random.Range( -70f, -30f );
			float randomY = Random.Range( 30f, 40f );
			float randomZ = Random.Range( -80f, -10f );
			Vector3 randomPosition = new Vector3( randomX, randomY, randomZ );

			//create cloud
			if (type == 0)
			{
				Instantiate( cloud1, randomPosition, new Quaternion( 0f, 0f, 0f, 0f ) );
				yield return new WaitForSeconds( cycle );
			}
			else
			{
				Instantiate( cloud2, randomPosition, new Quaternion( 0f, 0f, 0f, 0f ) );
				yield return new WaitForSeconds( cycle );
			}
		}

	}

}