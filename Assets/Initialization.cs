using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Initializaztion : MonoBehaviour {
	public GameObject SectionPrefab;
	[Range(10, 100)]
    public static float scale = 0.1f;

	List<GameObject> Snake = new List<GameObject>();
    int slength = 0;
    string sdirection = "R";
    Queue<string> directions = new Queue<string>();

    void Awake () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 2;

		CreateSnake (10);
        slength = Snake.Count;

		Vector3 vectorNW, vectorNE, vectorSW, vectorSE;
		vectorNW.y = 50 * scale; vectorNW.x = -50 * scale;
		vectorNE.y = 50 * scale; vectorNE.x = 50 * scale;
		vectorSW.y = -50 * scale; vectorSW.x = -50 * scale;
		vectorSE.y = -50 * scale; vectorSE.x = 50 * scale;

		CreateWall (vectorNW, vectorNE);
		CreateWall (vectorSW, vectorNW);
		CreateWall (vectorSE, vectorNE);
		CreateWall (vectorSW, vectorSE);
	}

	void CreateSnake(int blength){
		Vector3 position;
		position.z = 0f;
		position.y = 0f;

		for (int i=0; i < blength; i++) {
			GameObject Section = Instantiate(SectionPrefab);
			position.x = -i*scale*1f;

			Transform TSection = Section.GetComponent (typeof(Transform)) as Transform;
			TSection.localPosition = position;
			TSection.localScale = Vector3.one * scale;
			if (i == 0) {
				Section.AddComponent <SnakeHead>();
			}
			Snake.Add(Section);
			directions.Enqueue("R");
		}
	}

	void CreateWall(Vector3 sPosition, Vector3 ePosition){
		Vector3 position;
		position.z = 0f;

		for (int i=sPosition.x; i <= ePosition.x; i=i+scale*1f) {
			position.x = sPosition.x + i * scale * 1f;
			for (int j = sPosition.y; j <= ePosition.y; j = j + scale * 1f) {
				GameObject Section = Instantiate(SectionPrefab);
				position.y = sPosition.y + i * scale * 1f;

				Transform TSection = Section.GetComponent (typeof(Transform)) as Transform;
				TSection.localPosition = position;
				TSection.localScale = Vector3.one * scale;
			}
		}
	}

    void Update()
    {
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
