using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioClip przywitanie;
	public AudioClip[] zadanie_;
	public AudioClip[] porada_;
	public AudioClip[] czar_;
	public AudioClip[] pocieszenie_;
	public AudioClip[] gratulacje_;
	public AudioClip zakonczenie;

	public AudioSource audio;

	// Use this for initialization
	void Start () { 
		 audio = GetComponent<AudioSource>();
	}
		

	public IEnumerator audio_przywitanie()
	{
		audio.clip = przywitanie;
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}

	public IEnumerator audio_zadanie_(int x)
	{
		audio.clip = zadanie_[x];
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}

	public IEnumerator audio_porada_(int x)
	{
		audio.clip = porada_[x];
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}

	public IEnumerator audio_czar_(int x)
	{
		audio.clip = czar_[x];
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}

	public IEnumerator audio_pocieszenie_(int x)
	{
		audio.clip = pocieszenie_[x];
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}

	public IEnumerator audio_gratulacje_(int x)
	{
		audio.clip = gratulacje_[x];
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}

	public IEnumerator audio_zakonczenie()
	{
		audio.clip = zakonczenie;
		audio.Play ();
		yield return new WaitForSeconds(audio.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
