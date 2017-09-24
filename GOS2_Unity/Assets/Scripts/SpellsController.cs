using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject rabbit;
    public GameObject poof;
    public GameObject sweets;
    public GameObject snow;
    public GameObject strongSnow;
    public Sprite nightBackground;
    public GameObject background;
    public GameObject wizard;

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
                transformation();
                //explosion(5);
                break;
            case "l":
                summonRabbit();
                break;
            case "b":
                summonSweets();
                break;
            case "spiral":
                summonWinter();
                break;
        }
    }

    private void explosion(int explosionCount)
    {
        for(int i = 0; i < explosionCount; i++)
        {
            Instantiate(explosionPrefab, getRandomWorldPosition(), Quaternion.identity);
        }
    }

    private void summonRabbit()
    {
        Instantiate(rabbit, new Vector3(4.5f, -3, 0), Quaternion.identity);
        Instantiate(poof, new Vector3(4.5f, -3, 0), Quaternion.identity);
    }

    private void summonSweets()
    {
        sweets.SetActive(true);
        Instantiate(poof, new Vector3(-5.5f, -3.5f, 0), Quaternion.identity);
        Instantiate(poof, new Vector3(-1f, -3, 0), Quaternion.identity);
    }

    private void summonWinter()
    {
        Instantiate(snow);
        Instantiate(strongSnow);
        Invoke("changeBackground", 1.5f);
    }

    private void changeBackground()
    {
        background.GetComponent<SpriteRenderer>().sprite = nightBackground;
    }

    private void transformation()
    {
        blockade = true;
        movingWizard = true;
        wizard.GetComponent<Animator>().SetTrigger("transformation");
        Instantiate(poof, wizard.transform.position, Quaternion.identity);
    }

    private Vector3 getRandomWorldPosition()
    {
        float randomX = Random.Range(0, 1f);
        float randomY = Random.Range(0, 1f);

        return Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0));
    }
}
