using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject rabbit;
    public GameObject poof;
    public GameObject sweets;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createSpellEffect(string gesture)
    {
        switch(gesture)
        {
            case "straight line":
                explosion(5);
                break;
            case "l":
                summonRabbit();
                break;
            case "b":
                summonSweets();
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

    private Vector3 getRandomWorldPosition()
    {
        float randomX = Random.Range(0, 1f);
        float randomY = Random.Range(0, 1f);

        return Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0));
    }
}
