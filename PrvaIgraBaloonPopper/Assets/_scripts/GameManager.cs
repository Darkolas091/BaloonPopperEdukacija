using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private Balloon[] balloonPrefabs;
    [SerializeField] private Balloon balloonPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private Vector3 spawnArea;
    private int spawnAreaX;
    private int spawnAreaZ;
    
    [SerializeField] private float timeInterval = 0f;

     private float timeCounter = 2f;

    private void Update()
    {
        timeCounter -= Time.deltaTime;
        if(timeCounter <= 0)
        {
            timeCounter = timeInterval;
            BalloonSpawnPoint();
        }
    }

    //private Balloon RandomBalloon()
    //{
    //    int random = RandomNumber(balloonPrefabs.Length);
    //    return balloonPrefabs[random];
    //}

    private void BalloonSpawnPoint()
    {
        //int random = RandomNumber(spawnPoints.Length);
        //Balloon balloonClone = Instantiate(balloonPrefab, new Vector3(Random.Range(-10,11),0, Random.Range(0, 6)) , Quaternion.identity);
        //balloonClone.ChangeMaterial(RandomNumber(balloonClone.balloonMaterials.Length));
        //balloonClone.ChangeBalloonSpeed(balloonClone.balloonSpeed);

        int random = RandomNumber(spawnPoints.Length);
        Balloon KirbyClone = Instantiate(balloonPrefab, new Vector3(Random.Range(-10, 11), 0, Random.Range(0, 6)), Quaternion.identity);
        KirbyClone.ChangeMaterial(RandomNumber(KirbyClone.balloonMaterials.Length));
        KirbyClone.ChangeBalloonSpeed(KirbyClone.balloonSpeed);
    }

    public int RandomNumber(int index)
    {

        return Random.Range(0, index);
    }

    //private int RandomVelocity(int index)
    //{
    //    return Random.Range(0, index);
    //}
}
