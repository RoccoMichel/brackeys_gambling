using System.Collections;
using UnityEngine;

public class SlinkoManager : Minigame
{
    [SerializeField] private GameObject snail;

    protected override void OnStart()
    {
        snail.SetActive(false);
        base.OnStart();
    }

    public override void GameStart()
    {
        base.GameStart();
        snail.SetActive(true);
        StartCoroutine(CinematicStart());
    }
    //Väljer slumpmässig plats för snigeln att spawna - Felix
    private void RandomizeSpawn()
    {
        int spawnX = Random.Range(-6, 6);
        snail.transform.position = new Vector2(spawnX, 5);
        snail.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    
    private void Update()
    {
        //For testing - Felix
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeSpawn();           
        }
    }

    // vÄRY COOL start so it like studsa runt - Felix
    private IEnumerator CinematicStart()
    {
        RandomizeSpawn();
        yield return new WaitForSeconds(0.2f);

        RandomizeSpawn();
        yield return new WaitForSeconds(0.3f);


        RandomizeSpawn();
        yield return new WaitForSeconds(0.4f);


        RandomizeSpawn();
        yield return new WaitForSeconds(0.5f);

        RandomizeSpawn();
        yield break;
    }
}
