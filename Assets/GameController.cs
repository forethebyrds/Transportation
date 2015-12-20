using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public int score = 0;
	public Airplane airplane;
	public float turnLength = 1.5f;
	public float timeToAct = 0;
	private GameObject[,] allCubes;
	public GameObject aCube;
	int gridWidth = 16;
	int gridHeight = 9;
	int depotX = 15;
	int depotY = 0;
	int airplaneStartX = 0;
	int airplaneStartY = 8;

	public void ProcessClickedCube (GameObject clickedCube, int x, int y)
	{
		// If the player clicks an inactive airplane, it should highlight
		if (x == airplane.x && y == airplane.y && airplane.active == false) {
			airplane.active = true;
			clickedCube.GetComponent<Renderer>().material.color = Color.yellow;
		}  
		// If the player clicks an active airplane, it should unhighlight
		else if (x == airplane.x && y == airplane.y && airplane.active) {
			airplane.active = false;
			clickedCube.GetComponent<Renderer>().material.color = Color.red;
		}

		airplane.targetX = x;
		airplane.targetY = y;
		
	}
	public void MoveAirplane(){
		
		int nextX = airplane.x;
		int nextY = airplane.y;
		
		// Use this for initialization
		if (airplane.targetX > airplane.x) {

			nextX++;

		} else if (airplane.targetX < airplane.x) {

			nextX--;

		}
		
		if (airplane.targetY > airplane.y) {

			nextY++;

		} else if (airplane.targetY < airplane.y) {

			nextY--;

		}
		// Set the old cube to black if it's the depot
		if (airplane.x == depotX && airplane.y == depotY) {

			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.black;
		
		}

		// otherwise, set the old cube to white
		else {

			allCubes[nextX, nextY].GetComponent<Renderer>().material.color = Color.white;
		
		}
		
		// Set the new cube to yellow if the airplane is still active
		if (airplane.active) {
			allCubes[nextX, nextY].GetComponent<Renderer>().material.color = Color.yellow;
		}
		// otherwise the airplane is deactive and red
		else {
			allCubes[nextX, nextY].GetComponent<Renderer>().material.color = Color.red;
		}
		
		// Update the airplane to be in the new location
		airplane.x = nextX;
		airplane.y = nextY;

	}
		
		void Start (){
			
			timeToAct = turnLength;

			airplane = new Airplane();

			allCubes = new GameObject[gridWidth, gridHeight];

			allCubes[depotX,depotY].GetComponent<Renderer>().material.color = Color.black;	

			airplane.targetX = airplaneStartX;
			airplane.targetY = airplaneStartY;
			
			
		for (int x = 0; x < gridWidth; x++) {
				for (int y = 0; y < gridHeight; y++) {
					
				allCubes[x,y] = (GameObject) Instantiate(aCube,new Vector3(x*2 - 14, y*2 - 8, 10), Quaternion.identity);

			}

		}

			airplane.x = airplaneStartX;
			airplane.y = airplaneStartY;
			allCubes[airplaneStartX, airplaneStartY].GetComponent<Renderer>().material.color = Color.red;
			
		}
		
		// Update is called once per frame
		void Update ()
		{
			
			if (Time.time > timeToAct) {
					
				MoveAirplane();
				timeToAct += turnLength;
				//check if airplane is in it's start location 
				
				if (airplane.x == airplaneStartX && airplane.y == airplaneStartY) {
					airplane.cargo = Mathf.Min(airplane.cargo + 10, airplane.cargoCapacity);
				}
				
				if (airplane.cargo > airplane.cargoCapacity) {
					
					airplane.cargo = airplane.cargoCapacity;
				}
			}
			if (airplane.x == depotX && airplane.y == depotY) {
				score += airplane.cargo;
				airplane.cargo = 0;
			}
			
			print ("Airplane Cargo: " + airplane.cargo);
			
			if (airplane.x == depotX && airplane.y == depotY) {
				score += airplane.cargo;
				airplane.cargo = 0;
			}
			
			
			print ("Score: " + score);
			
			// prints the current cargo value 
			print ("Cargo: " + airplane.cargo);
			
			
			//calls the keyboard movement
//			KeyboardMovement();
			
		}

}