using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.Xml;
using System.IO;

public class GesturesScript : MonoBehaviour {

    public GameObject trailPrefab;
    public GameObject wizardTrail;

    public string wantedGesture;

    public AudioSource trailSoundSource;

    private GameObject trail;
    private bool allowDrawing = false;

    public RuntimePlatform platform;

    public enum DrawingState
    {
        notDrawing,
        drawing,
        failedDrawing,
        succededDrawing
    }

    private DrawingState drawingState = DrawingState.notDrawing;

    Dictionary<string, Gesture> templateGestures;

    List<Gesture> trainingGestures;
    List<Point> currentTrailPoints;

	// Use this for initialization
	void Awake () {
        trainingGestures = new List<Gesture>();
        currentTrailPoints = new List<Point>();
        templateGestures = new Dictionary<string, Gesture>();
        loadGestureTrainingSet();
        loadTemplateGestures();

#if UNITY_ANDROID
        platform = RuntimePlatform.Android;
#elif UNITY_STANDALONE_WIN
        platform = RuntimePlatform.WindowsPlayer;
#endif
    }

    // Update is called once per frame
    void Update() {
        // -- Drag
        // ------------------------------------------------

        if (platform == RuntimePlatform.WindowsPlayer)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                if (allowDrawing)
                {
                    startDrawing();
                    trailSoundSource.Play();
                }
            }
            else if (trail != null && Input.GetMouseButton(0))
            {
                if (drawingState == DrawingState.drawing)
                {
                    continueDrawing();
                }
            }
            else if (trail != null && Input.GetMouseButtonUp(0))
            {
                if (drawingState == DrawingState.drawing)
                {
                    finishDrawing();
                    trailSoundSource.Stop();
                }
            }
        }
        else if(platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (allowDrawing)
                {
                    startDrawing();
                    trailSoundSource.Play();
                }
            }
            else if (trail != null && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (drawingState == DrawingState.drawing)
                {
                    continueDrawing();
                }
            }
            else if (trail != null && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (drawingState == DrawingState.drawing)
                {
                    finishDrawing();
                    trailSoundSource.Stop();
                }
            }
        }

    }

    private void startDrawing()
    {

        currentTrailPoints = new List<Point>(1);

        Vector3 mousePos;
        if(platform == RuntimePlatform.WindowsPlayer)
        {
            mousePos = Input.mousePosition;
        }
        else
        {
            mousePos = Input.GetTouch(0).position;
        }

        mousePos.z = 10;
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);

        trail = CreateTrail(position);
        savePoint(position.x, position.y);

        drawingState = DrawingState.drawing;
    }

    private void continueDrawing()
    {
        Vector3 mousePos;
        if (platform == RuntimePlatform.WindowsPlayer)
        {
            mousePos = Input.mousePosition;
        }
        else
        {
            mousePos = Input.GetTouch(0).position;
        }
        mousePos.z = 10;
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);
     
        trail.transform.position = position;
        savePoint(position.x, position.y);
    }

    private void finishDrawing()
    {
        Destroy(trail, trail.GetComponent<TrailRenderer>().time);
        trail = null;

        if (classifyDrawnShape(new Gesture(currentTrailPoints.ToArray())))
        {
            drawingState = DrawingState.succededDrawing;
        }
        else
        {
            drawingState = DrawingState.failedDrawing;
        }
    }

    public void askForGesture(string gesture)
    {
        allowDrawing = true;
        wantedGesture = gesture;
        drawingState = DrawingState.notDrawing;
    }

    public DrawingState getDrawingState()
    {
        DrawingState currentState = drawingState;

        if (drawingState == DrawingState.succededDrawing)
        {
            allowDrawing = false;
            drawingState = DrawingState.notDrawing;
        }
        else if(drawingState == DrawingState.failedDrawing)
        {
            drawingState = DrawingState.drawing;
        }

        return currentState;
}

    void loadTemplateGestures()
    {
        foreach (string filename in Directory.GetFiles(Application.streamingAssetsPath + "/GestureTraining/Templates", "*.xml"))
        {
            Gesture loadedGesture = ReadGesture(filename);
            templateGestures.Add(loadedGesture.Name, loadedGesture);
        }
    }

    void savePoint(float x, float y)
    {
        currentTrailPoints.Add(new Point(x, y, 1));
    }

    GameObject CreateTrail(Vector3 position)
    {
        GameObject trail = Instantiate(trailPrefab) as GameObject;
        trail.transform.position = position;

        return trail;
    }

    bool classifyDrawnShape(Gesture candidate)
    {
        if(candidate.Points.Length <= 1 || candidate.Points[1] == null)
        {
            return false;
        }
        StringFloatPair classificationResult = PointCloudRecognizer.Classify(candidate, trainingGestures.ToArray());
        if (classificationResult.f < 0.2) //to be set experimentally
        {
            return wantedGesture == classificationResult.s;
        }
        else return false;
    }

    void loadGestureTrainingSet()
    {
        foreach(string filename in Directory.GetFiles(Application.streamingAssetsPath + "/GestureTraining/", "*.xml"))
        {
            trainingGestures.Add(ReadGesture(filename));
        }
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

    public Gesture getRandomGesture()
    {
        List<string> keyList = new List<string>(templateGestures.Keys);

        Random rand = new Random();
        Gesture randomKey = templateGestures[keyList[Random.Range(0, keyList.Count)]];
        return randomKey;
    }

    public Gesture getGesture(string name)
    {
        return templateGestures[name];
    }
}
