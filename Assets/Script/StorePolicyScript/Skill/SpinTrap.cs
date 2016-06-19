using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpinTrap : Skill
{
	//complex data field
	public GameObject spinTrapObject;

	//constructor - no parameter
	public SpinTrap ()
	{
		skillName = "SpinTrap";
		skillPrice = 40;
		originCounter = 3;
		skillCounter = originCounter;
	}

	//another method

	//skill active - use override
	public override void ActiveSkill ()
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
			
			//active skill
			Instantiate( spinTrapObject, point, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f) );
			skillCounter--;
		}
	}


}

