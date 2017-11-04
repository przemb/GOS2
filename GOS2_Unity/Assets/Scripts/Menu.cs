using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	int n = 0;

	public AudioClip[] porady;

	public AudioSource audioSource;

	public Canvas creditsCanvas;

    // Use this for initialization
    void Start () {
		
	}
    

    public void StartPressed ()
    {
        SceneManager.LoadScene("Main");
    }

	public void CreditsPressed ()
	{
		//SceneManager.LoadScene("Credits");
		creditsCanvas.sortingOrder = 2;
	}

	public void ExitPressed ()
	{
		Application.Quit();
		//UnityEditor.EditorApplication.isPlaying = false;
	}

    public static void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

	public void HelpPressed()
	{
		audioSource.clip = porady [n];
		audioSource.Play ();

		++n;
		if (n > 1)
			n = 0;
	}

	public void BackPressed ()
	{
		creditsCanvas.sortingOrder = 0;
	}



}
