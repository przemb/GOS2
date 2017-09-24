using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public GestureController gestureController;
    public AudioController ac;

    public GameObject wizard;

    public float chanceForPocieszenie = 1;

    private string[] gestures;

    private bool finished = false;

    enum AudioState
    {
        toIntro,
        introduction,
        spellExplanation,
        other,
        none
    }

    private AudioState audioState = AudioState.toIntro;

    private int lessonNumber;
    private bool finishedLesson = true;


	// Use this for initialization
	void Start () {
        gestures = new string[] { "straight line", "l", "b", "spiral", "star" };
    }
	
    
	// Update is called once per frame
	void Update () {
        if(!ac.audio.isPlaying && !finished && !gestureController.spellController.blockade)
        {
            switch (audioState)
            {
                case AudioState.toIntro:
                    if (lessonNumber == 0)
                    {
                        ac.audio_przywitanie();
                        lessonNumber++;
                    }
                    else if (lessonNumber >= 1 && lessonNumber <= 5)
                    {
                        ac.audio_zadanie_(lessonNumber - 1);
                    }
                    else
                    {
                        ac.audio_zakonczenie();
                        finished = true;
                    }
                    audioState = AudioState.introduction;
                    break;
                case AudioState.introduction:
                    ac.audio_czar_(lessonNumber - 1);
                    audioState = AudioState.spellExplanation;
                    break;
                case AudioState.spellExplanation:
                    startLesson();
                    audioState = AudioState.other;
                    break;
            }
        }
        
        if (gestureController.hasSucceeded())
        {
            finishedLesson = true;
            lessonNumber++;
            audioState = AudioState.toIntro;
        }
        if (gestureController.hasFailed() && !ac.audio.isPlaying)
        {
            if(Random.Range(0, 1f) <= chanceForPocieszenie)
            {
                ac.randomPocieszenie();

                wizard.GetComponent<Animator>().SetTrigger("startAttack");
                gestureController.showGesture(gestures[lessonNumber - 1]);
            }
        }
    }

    private void startLesson()
    {
        spellLesson(gestures[lessonNumber - 1]);
    }

    private void spellLesson(string gestureName)
    {
        finishedLesson = false;
        wizard.GetComponent<Animator>().SetTrigger("startAttack");
        gestureController.giveDrawingTask(gestureName);

        gestureController.showGesture(gestureName);
    }
    
}
