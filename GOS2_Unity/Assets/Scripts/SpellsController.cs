using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour {

    public GameObject explosionPrefab;

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

    }

    private Vector3 getRandomWorldPosition()
    {
        float randomX = Random.Range(0, 1f);
        float randomY = Random.Range(0, 1f);

        return Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0));
    }
}
