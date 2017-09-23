using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureController : MonoBehaviour {

    public Button startButton;
    public Text textField;
    public GesturesScript gestureScript;

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
        string randomGesture = gestureScript.getRandomGesture();
        textField.text = randomGesture;
        gestureScript.askForGesture(randomGesture);
    }
}
