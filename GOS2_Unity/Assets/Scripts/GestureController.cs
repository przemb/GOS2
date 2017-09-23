using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PDollarGestureRecognizer;

public class GestureController : MonoBehaviour {

    public Button startButton;
    public Text textField;
    public GesturesScript gestureScript;
    public AutomaticDrawer automaticDrawer;

	// Use this for initialization
	void Start () {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(giveDrawingTask);
    }
	
	// Update is called once per frame
	void Update () {
		switch(gestureScript.getDrawingState())
        {
            case GesturesScript.DrawingState.failedDrawing:
                textField.text = "slabiak!";
                break;
            case GesturesScript.DrawingState.succededDrawing:
                textField.text = "koxik!";
                break;
        }
	}

    void giveDrawingTask()
    {
        Gesture randomGesture = gestureScript.getRandomGesture();
        textField.text = randomGesture.Name;
        gestureScript.askForGesture(randomGesture.Name);

        automaticDrawer.startMove(Instantiate(gestureScript.trailPrefab), scalePoints(randomGesture.Points));
    }

    Point[] scalePoints(Point[] points)
    {
        Point[] newPoints = new Point[points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            newPoints[i] = new Point(points[i].X, points[i].Y, 1);
        }
        foreach(Point p in newPoints)
        {
            p.X *= 5;
            p.Y *= 5;
        }
        return newPoints;
    }
}
