using UnityEngine;
using System.Collections;

public class MoveCloud : MonoBehaviour
{

	// Use this for initialization
	void Start( )
	{
	
	}
	
	// Update is called once per frame
	void Update( )
	{
		//set speed
		float randomX = Random.Range( 0.0f, 2f );
		float randomZ = Random.Range( 0.0f, 2f );

		//move cloud
		Vector3 moveDirection = new Vector3( randomX, 0f, randomZ );
		transform.Translate( moveDirection * Time.deltaTime );
	}
}
