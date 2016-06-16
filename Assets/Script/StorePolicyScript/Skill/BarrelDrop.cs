using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarrelDrop : Skill
{
	//complex data field
	public GameObject barrel;

	//constructor - no parameter
	public BarrelDrop()
	{
		skillName = "BarrelDrop";
		skillPrice = 2;
		originCounter = 10;
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
			point.y = ray.origin.y;
		
			//active skill
			Instantiate( barrel, point, 
			            new Quaternion( Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ) ) );
			skillCounter--;

		}
	}


}

