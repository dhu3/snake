using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Initialization : MonoBehaviour {
	public GameObject BodyPrefab;
	public GameObject WallPrefab;
	public GameObject HeadPrefab;
	public GameObject FoodPrefab;

	[Range(10, 100)]
    public static float scale = 0.1f;

	List<GameObject> Snake = new List<GameObject>();
    string sdirection = "R";
    Queue<string> directions = new Queue<string>();

    void Awake () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 2;

		CreateSnake (10);

		GameObject WallN = Instantiate (WallPrefab);
		Transform TWallN = WallN.GetComponent (typeof(Transform)) as Transform;
		TWallN.localPosition = new Vector3 (0, 50*scale, 0);
		TWallN.localScale = new Vector3 (100*scale, 1*scale, 1*scale);

		GameObject WallS = Instantiate (WallPrefab);
		Transform TWallS = WallS.GetComponent (typeof(Transform)) as Transform;
		TWallS.localPosition = new Vector3 (0, -50*scale, 0);
		TWallS.localScale = new Vector3 (100*scale, 1*scale, 1*scale);

		GameObject WallW = Instantiate (WallPrefab);
		Transform TWallW = WallW.GetComponent (typeof(Transform)) as Transform;
		TWallW.localPosition = new Vector3 (-50*scale, 0, 0);
		TWallW.localScale = new Vector3 (1*scale, 100*scale, 1*scale);

		GameObject WallE = Instantiate (WallPrefab);
		Transform TWallE = WallE.GetComponent (typeof(Transform)) as Transform;
		TWallE.localPosition = new Vector3 (10*scale, 0, 0);
		TWallE.localScale = new Vector3 (1*scale, 100*scale, 1*scale);

//		CreateWall (vectorNW, vectorNE);
//		CreateWall (vectorSW, vectorNW);
//		CreateWall (vectorSE, vectorNE);
//		CreateWall (vectorSW, vectorSE);
	}

	void CreateSnake(int blength){
		Vector3 position;
		position.z = 0f;
		position.y = 0f;

		for (int i=0; i < blength; i++) {
			GameObject Section;
			if (i < 2) {
				Section = Instantiate (HeadPrefab);
				if (i == 0) {
					Section.AddComponent <SnakeHead> ();
				}
			} else {
				Section = Instantiate(BodyPrefab);
			}
			position.x = -i*scale*1f;

			Transform TSection = Section.GetComponent (typeof(Transform)) as Transform;
			TSection.localPosition = position;
			TSection.localScale = Vector3.one * scale;

			Snake.Add(Section);
			directions.Enqueue("R");
		}
	}

	void CreateWall(Vector3 sPosition, Vector3 ePosition){
		Vector3 position=sPosition;

		for (float i=sPosition.x; i <= ePosition.x; i=i+scale*1f) {
			if (i < ePosition.x) {
				position.x = position.x + scale;
			}
			for (float j = sPosition.y; j <= ePosition.y; j = j + scale * 1f) {
				GameObject Section = Instantiate(WallPrefab);
				if (j < ePosition.y) {
					position.y = position.y + scale;
				}
				
				Transform TSection = Section.GetComponent (typeof(Transform)) as Transform;
				TSection.localPosition = position;
				TSection.localScale = Vector3.one * scale;
			}
		}
	}

	public void CreateFood(){
		System.Random rng =new System.Random();
		GameObject Food = Instantiate (FoodPrefab);
		Transform TFood = Food.GetComponent (typeof(Transform)) as Transform;
		TFood.localPosition = new Vector3 (rng.Next(-48, 48),rng.Next(-48, 48),0)*scale;
		TFood.localScale = new Vector3(1,1,1)*scale;
	}

	public void SnakeGrow(){
		GameObject Section = Instantiate(BodyPrefab);
		Transform TSection = Section.GetComponent (typeof(Transform)) as Transform;
		Transform TLastSection = Snake[Snake.Count-1].GetComponent (typeof(Transform)) as Transform;
		Vector3 lastVector = TLastSection.localPosition;
		string direction = directions.ToArray () [0];

		if (direction == "L") {
			TSection.localPosition = new Vector3 (lastVector.x + scale, lastVector.y, 0);
		}
		else if (direction == "R") {
			TSection.localPosition = new Vector3 (lastVector.x - scale, lastVector.y, 0);
		}
		else if (direction == "U") {
			TSection.localPosition = new Vector3 (lastVector.x, lastVector.y - scale, 0);
		}
		else if (direction == "D") {
			TSection.localPosition = new Vector3 (lastVector.x, lastVector.y + scale, 0);
		}
		TSection.localScale = new Vector3(1,1,1)*scale;
	}

    void Update()
    {
		int slength= Snake.Count;

		if (Input.GetKeyDown (KeyCode.A)) {
			if (sdirection != "L" && sdirection != "R") {
				sdirection = "L";
			}
		} else if (Input.GetKeyDown (KeyCode.S)) {
			if (sdirection != "U" && sdirection != "D") {
				sdirection = "D";
			}
		} else if (Input.GetKeyDown (KeyCode.D)) {
			if (sdirection != "L" && sdirection != "R") {
				sdirection = "R";
			}
		} else if (Input.GetKeyDown (KeyCode.W)) {
			if (sdirection != "U" && sdirection != "D") {
				sdirection = "U";
			}
		} else {
		}

		directions.Enqueue(sdirection);
		if (directions.Count > slength)
		{
			directions.Dequeue();
		}

        for(int i=0;i<slength;i++)
        {
			Transform TSection = Snake[i].GetComponent (typeof(Transform)) as Transform;

            Vector3 position;
			position = TSection.localPosition;

			string[] da = directions.ToArray ();
			string direction = directions.ToArray()[slength-1-i];
            if (direction == "L")
            {
                position.x = position.x - 1f*scale;
            }
            else if(direction == "R")
            {
                position.x = position.x + 1f*scale;
            }
            else if (direction == "U")
            {
                position.y = position.y + 1f*scale;
            }
            else if(direction == "D")
            {
                position.y = position.y - 1f*scale;
            }

			TSection.localPosition = position;
        }
	}
}
