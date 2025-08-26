using System.Collections;
using UnityEngine;

public class SlinkoManager : MonoBehaviour
{
    [SerializeField] private GameObject snail;

    private void Start()
    {
        StartCoroutine(cinematicStart());
    }
    private void RandomizeSpawn()
    {

        int spawnX = Random.Range(-6, 6);
        snail.transform.position = new Vector2(spawnX, 5);
        snail.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeSpawn();
           
        }
    }

    //Write function to detirmine winning number

    //Absoulute cinema
    private IEnumerator cinematicStart()
    {
        RandomizeSpawn();
        yield return new WaitForSeconds(0.3f);

        RandomizeSpawn();
        yield return new WaitForSeconds(0.3f);


        RandomizeSpawn();
        yield return new WaitForSeconds(0.3f);


        RandomizeSpawn();
        yield return new WaitForSeconds(0.3f);

        RandomizeSpawn();
        yield break;


    }
}
