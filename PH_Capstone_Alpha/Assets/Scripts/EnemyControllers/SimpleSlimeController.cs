using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlimeController : BaseEnemyController
{

	// Use this for initialization
	void Start () {
		MoveComplete = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void PlayerOn()
	{

	}

	public override void GetMove() { }

	public override void LightEffect(LightStatus n_lightColor)
	{
		switch (n_lightColor)
		{
			case LightStatus.White:
				
				break;
			case LightStatus.Infrd:

				break;
			case LightStatus.Ulvlt:

				break;
			case LightStatus.Nopwr:
				
				break;
		}
	}
}
