using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PDollarGestureRecognizer;

public class GestureController : MonoBehaviour {

    //public Button startButton;
    //public Text textField;
    public GesturesScript gestureScript;
    public AutomaticDrawer automaticDrawer;
    public SpellsController spellController;
    private bool success = false;
    private bool failure = false;

	// Use this for initialization
	void Start () {
        //Button btn = startButton.GetComponent<Button>();
        //btn.onClick.AddListener(giveRandomDrawingTask);
    }
	
	// Update is called once per frame
	void Update () {
		switch(gestureScript.getDrawingState())
        {
            case GesturesScript.DrawingState.failedDrawing:
                failure = true;
                break;
            case GesturesScript.DrawingState.succededDrawing:
                spellController.createSpellEffect(gestureScript.wantedGesture);
                success = true;
                break;
        }
	}

    void giveRandomDrawingTask()
    {
        Gesture randomGesture = gestureScript.getRandomGesture();
        //textField.text = randomGesture.Name;
        gestureScript.askForGesture(randomGesture.Name);

        automaticDrawer.startMove(Instantiate(gestureScript.trailPrefab), scalePoints(randomGesture.Points));
    }

    public void showGesture(string gestureName)
    {
        automaticDrawer.startMove(Instantiate(gestureScript.wizardTrail), scalePoints(gestureScript.getGesture(gestureName).Points));
    }

    public void giveDrawingTask(string gestureName)
    {
        gestureScript.askForGesture(gestureName);
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

    public bool hasSucceeded()
    {
        if (success)
        {
            success = false;
            return true;
        }
        else return false;
    }

    public bool hasFailed()
    {
        if (failure)
        {
            failure = false;
            return true;
        }
        else return false;
    }
}
