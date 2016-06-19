using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lighting : Skill
{
	//complex data field
	public GameObject lightingEffect;
	public StoreManager manager;

	//constructor - no parameter
	public Lighting ()
	{
		skillName = "Lighting";
		skillPrice = 60;
		originCounter = 2;
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
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StoreManager>();

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitinfo;
		if (Physics.Raycast( ray, out hitinfo, 100f, 1 << LayerMask.NameToLayer( "Terrain" ) )
		    || Physics.Raycast( ray, out hitinfo, 100f, 1 << LayerMask.NameToLayer( "Human" ) ))
		{
			Vector3 point = hitinfo.point;
			
			//active skill
			Instantiate( lightingEffect, point, 
				new Quaternion(Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 ), Random.Range( 0, 360 )) );
			
			//human death
			if(hitinfo.collider.gameObject.layer == LayerMask.NameToLayer("Human"))
			{
				if(hitinfo.collider.gameObject.name == "Theif")
					manager.ThiefDeath(hitinfo.collider.gameObject.GetComponent<Thief>().OnShopping, hitinfo.collider.gameObject.GetComponent<Thief>().StealPrice );
				else
					manager.CustomerDeath();
		
					}
			skillCounter--;

		}
	}


}

