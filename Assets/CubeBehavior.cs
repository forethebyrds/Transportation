using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour 
{

	public int x, y;

	GameController aGameController; 

	// Use this for initialization
	public void Start () 
	{
		aGameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();
	}

	public void OnMouseDown(){

		aGameController.ProcessClickedCube(this.gameObject, x, y);

	}
	
	// Update is called once per frame
	public void Update () {

	}
}
