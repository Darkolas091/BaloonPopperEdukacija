using System.Collections;
using UnityEngine;

public class CorutieZadatak : MonoBehaviour
{
    //[SerializeField] private int numberofCubes;
    //[SerializeField] private GameObject prefabStairs;
    //[SerializeField] private GameObject prefabDoor;
    //[SerializeField] private GameObject[] prefabs;

    [SerializeField] private GameObject prefabCube;
    private Vector3 position;
    private int counter = 0;
    private int maxHealth = 100;
    private int health;
    private bool shouldRegenerateHealth = true;

    private void Start()
    {
        health = maxHealth - 50;
        Invoke(nameof(CheckAndStartRegen), 5f);
    }

    private void CheckAndStartRegen()
    {
        if (health < 100)
        {
            StartCoroutine(RegenerateHealth());
        }
    }


    private IEnumerator RegenerateHealth()
    {
        while (health < 100)
        {
            health += 1;
            Debug.Log("Health: " + health);
            yield return new WaitForSeconds(1f);
        }
    }

    private bool shouldHealthRegenerate()
    {
        return maxHealth < 100;
    }

    private float randomPosition()
    {
        float random = Random.Range(-5f, 5f);
        return random;
    }

    IEnumerator MoveCubePrefab()
    {
        //Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(randomPosition(), randomPosition(), randomPosition()), Quaternion.identity);
        prefabCube.transform.position = new Vector3(randomPosition(), randomPosition(), randomPosition());
        Debug.Log(" PrefabCube position: " + prefabCube.transform.position);
        yield return new WaitForSeconds(1f);
        yield return MoveCubePrefab();
    }




    //private void SpawnPrefabStairs()
    //{
    //    Debug.Log("SpawnPrefabStairs called, counter: " + counter);
    //    counter++;
    //    position += new Vector3(0, 1, 1);

    //    Instantiate(prefabStairs, position, Quaternion.identity);

    //}
    //private void SpawnPrefabDoor()
    //{
    //    position += new Vector3(0, 1 + 1f, 1 - 0.6f);

    //    Instantiate(prefabDoor, position, Quaternion.identity);

    //}






}
