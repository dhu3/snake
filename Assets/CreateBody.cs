using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreateBody : MonoBehaviour {
	public Transform pointPrefab;
	[Range(10, 100)]
	public int blength = 10;
    public float scale = 0.1f;

    List<Transform> points = new List<Transform>();
    int slength = 0;
    string sdirection = "R";
    Queue<string> directions = new Queue<string>();

    void Start () {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 2;

  //      float step = 2f / resolution;
		//Vector3 scale = Vector3.one * step;
		Vector3 position;
		position.z = 0f;
        position.y = 0f;

		int i;
        for (i=0; i < blength; i++) {
			Transform point = Instantiate(pointPrefab);
            //position.x = (i + 0.5f) * step - 1f;
            position.x = i*scale*1f;
            point.localPosition = position;
			point.localScale = Vector3.one * scale;
            points.Add(point);
            directions.Enqueue("R");
		}

        slength = points.Count;
	}

    void Update()
    {
        directions.Enqueue(sdirection);
        if (directions.Count > slength)
        {
            directions.Dequeue();
        }

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

		int i;
        for(i=0;i<slength;i++)
        {
            Vector3 position;
            //position.x = i.localPosition.x + 0.01f;
            //position.y = i.localPosition.y;
            //position.z = i.localPosition.z;
            position = points[i].localPosition;

			int j;
            for (j=0; j < slength; j++)
            {
                string direction = directions.ElementAt(j);
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
            }


            points[i].localPosition = position;
        }
    }
}
