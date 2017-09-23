using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public GestureController gestureController;

    public GameObject wizard;

    private int lessonNumber;
    private bool finishedLesson = true;


	// Use this for initialization
	void Start () {

	}
	
    
	// Update is called once per frame
	void Update () {
        if (finishedLesson)
        {
            if (lessonNumber == 0)
            {
                firstSpellLesson();
            }
            else if (lessonNumber == 1)
            {
                secondSpellLesson();
            }
        }
        if (gestureController.hasSucceeded())
        {
            finishedLesson = true;
            lessonNumber++;
        }
        if (gestureController.hasFailed())
        {
            // something
        }
    }

    private void firstSpellLesson()
    {
        finishedLesson = false;
        //play some sounds
        gestureController.giveDrawingTask("straight line");
        wizard.GetComponent<Animator>().SetTrigger("startAttack");
        gestureController.showGesture("straight line");
    }

    private void secondSpellLesson()
    {
        finishedLesson = false;
        //play some sounds
        gestureController.giveDrawingTask("l");
        wizard.GetComponent<Animator>().SetTrigger("startAttack");
        gestureController.showGesture("l");
    }
}
