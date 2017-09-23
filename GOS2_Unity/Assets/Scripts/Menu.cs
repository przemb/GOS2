using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
    

    public void StartPressed ()
    {
        SceneManager.LoadScene("Main");
    }

	public void ExitPressed ()
	{
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

}
