﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class AutomaticDrawer : MonoBehaviour {

    private GameObject movedObject;
    private Queue<Point> path;

    public float speed = 1;

    //private 

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(movedObject != null && path.Count > 0)
        {
            moveObject();
        }
	}

    public void startMove(GameObject obj, Point[] path)
    {
        movedObject = obj;
        this.path = new Queue<Point>(path);
    }

    private void moveObject()
    {
        Point nextPoint = path.Dequeue();
        Vector3 newPos = new Vector3(nextPoint.X, nextPoint.Y, 0);

        movedObject.transform.position = newPos;
    }
}
