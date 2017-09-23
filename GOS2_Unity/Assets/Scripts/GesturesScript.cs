using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.Xml;
using System.IO;

public class GesturesScript : MonoBehaviour {

    public GameObject trailPrefab;

    private GameObject trail;

    List<Gesture> trainingGestures;

	// Use this for initialization
	void Start () {
        trainingGestures = new List<Gesture>();
        loadGestureTrainingSet();
	}
	
	// Update is called once per frame
	void Update () {
        // -- Drag
        // ------------------------------------------------
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);
           // position.z = 0; // Make sure the trail is visible

            trail = CreateTrail(position);
        }
        else if (trail != null && Input.GetMouseButton(0))
        {
            // Move the trail
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);
            //position.z = 0; // Make sure the trail is visible

            trail.transform.position = position;
        }
        else if (trail != null && Input.GetMouseButtonUp(0))
        {
                // Let the trail fade out
                Destroy(trail, trail.GetComponent<TrailRenderer>().time);
                trail = null;
        }
    }

    GameObject CreateTrail(Vector3 position)
    {
        GameObject trail = Instantiate(trailPrefab) as GameObject;
        trail.transform.position = position;

        return trail;
    }

    void loadGestureTrainingSet()
    {
        foreach(string filename in getAllFilesFromFolder())
        {
            trainingGestures.Add(ReadGesture(filename));
        }
    }

    string[] getAllFilesFromFolder()
    {
        string test = Application.dataPath + "/GestureTraining";
        return Directory.GetFiles(Application.dataPath + "/GestureTraining/", "*.xml");
    }

    public static Gesture ReadGesture(string fileName)
    {
        List<Point> points = new List<Point>();
        XmlTextReader xmlReader = null;
        int currentStrokeIndex = -1;
        string gestureName = "";
        try
        {
            xmlReader = new XmlTextReader(File.OpenText(fileName));
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType != XmlNodeType.Element) continue;
                switch (xmlReader.Name)
                {
                    case "Gesture":
                        gestureName = xmlReader["Name"];
                        if (gestureName.Contains("~")) // '~' character is specific to the naming convention of the MMG set
                            gestureName = gestureName.Substring(0, gestureName.LastIndexOf('~'));
                        if (gestureName.Contains("_")) // '_' character is specific to the naming convention of the MMG set
                            gestureName = gestureName.Replace('_', ' ');
                        break;
                    case "Stroke":
                        currentStrokeIndex++;
                        break;
                    case "Point":
                        points.Add(new Point(
                            float.Parse(xmlReader["X"]),
                            float.Parse(xmlReader["Y"]),
                            currentStrokeIndex
                        ));
                        break;
                }
            }
        }
        finally
        {
            if (xmlReader != null)
                xmlReader.Close();
        }
        return new Gesture(points.ToArray(), gestureName);
    }
}
