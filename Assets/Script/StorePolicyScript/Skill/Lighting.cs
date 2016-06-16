using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lighting : Skill
{
	//complex data field
	public GameObject lightingEffect;

	//constructor - no parameter
	public Lighting()
	{
		skillName = "Lighting";
		skillPrice = 30;
		originCounter = 2;
		skillCounter = originCounter;
	}

	//another method

	//skill active - use override
	public override void ActiveSkill( )
	{
		//if counter no exist -> Do not active skill
		if (skillCounter < 0)
		{
			Debug.Log( "Counter off" );
			return;
		}


		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitinfo;
		if (Physics.Raycast( ray, out hitinfo, 100f, 1 << LayerMask.NameToLayer( "Terrain" ) ))
		{
			Vector3 point = hitinfo.point;
			point.y = 35f;

			//active skill
			Instantiate( lightingEffect, point, 
			            new Quaternion( Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ) ) );
			skillCounter--;

		}
	}


}

