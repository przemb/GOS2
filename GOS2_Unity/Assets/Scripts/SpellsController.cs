using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour {

    public GameObject explosionPrefab;

    public GameObject rabbit1;
    public GameObject rabbit2;
    public GameObject rabbit3;

    public GameObject poof;
    public GameObject sweets;
    public GameObject snow;
    public GameObject strongSnow;
    public Sprite nightBackground;
    public GameObject background;
    public GameObject wizard;
    public AudioSource musicPlayer;
    public AudioClip[] summerAndWinterMusic; 

    public float speed;

    private bool movingWizard = false;
    private bool moving2 = false;
    public bool blockade = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(movingWizard)
        {
            wizard.transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (wizard.transform.position.x > 12)
            {
                wizard.transform.position = new Vector3(-12, wizard.transform.position.y, 0);
                moving2 = true;
            }
            if(wizard.transform.position.x > -6 && moving2)
            {
                movingWizard = false;
                moving2 = false;
                wizard.GetComponent<Animator>().SetTrigger("transformation");
                wizard.GetComponent<AudioSource>().Stop();
                Instantiate(poof, wizard.transform.position, Quaternion.identity);
                blockade = false;
            }
        }
	}

    public void createSpellEffect(string gesture)
    {
        switch(gesture)
        {
            case "straight line":
                //explosion(5, 30);
                summonRabbit();
                break;
            case "l":
                summonSweets();
                break;
            case "b":
                transformation();
                break;
            case "spiral":
                summonWinter();
                break;
            case "star":
                explosion(5, 30);
                break;
        }
    }

    private void explosion(float maxDelay, int numberOfWaves)
    {
        blockade = true;
        for(int i = 0; i < numberOfWaves; i++)
        {
            float delay = Random.Range(0, maxDelay);
            Invoke("explode", delay);
        }
        Invoke("liftBlockade", maxDelay);
    }

    private void explode()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab, getRandomWorldPosition(), Quaternion.identity);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetime.constant);
        }
    }

    private void summonRabbit()
    {
        blockade = true;
        rabbit1.GetComponent<Animator>().SetTrigger("start");
        rabbit2.GetComponent<Animator>().SetTrigger("start");
        rabbit3.GetComponent<Animator>().SetTrigger("start");

        //Instantiate(poof, new Vector3(4.5f, -3, 0), Quaternion.identity);
        Invoke("liftBlockade", 2);
    }

    private void summonSweets()
    {
        blockade = true;
        sweets.SetActive(true);
        Instantiate(poof, new Vector3(-5.5f, -3.5f, 0), Quaternion.identity);
        Instantiate(poof, new Vector3(-1f, -3, 0), Quaternion.identity);
        Invoke("liftBlockade", 2);
    }

    private void summonWinter()
    {
        blockade = true;
        Instantiate(snow);
        Instantiate(strongSnow);
        Invoke("changeBackground", 1.5f);
        Invoke("liftBlockade", 3);
    }

    private void changeBackground()
    {
        background.GetComponent<SpriteRenderer>().sprite = nightBackground;
        musicPlayer.clip = summerAndWinterMusic[1];
        musicPlayer.Play();
    }

    private void liftBlockade()
    {
        blockade = false;
    }

    private void transformation()
    {
        blockade = true;
        movingWizard = true;
        wizard.GetComponent<Animator>().SetTrigger("transformation");
        wizard.GetComponent<AudioSource>().Play();
        Instantiate(poof, wizard.transform.position, Quaternion.identity);
    }

    private Vector3 getRandomWorldPosition()
    {
        float randomX = Random.Range(0, 1f);
        float randomY = Random.Range(0, 1f);

        return Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0));
    }
}
