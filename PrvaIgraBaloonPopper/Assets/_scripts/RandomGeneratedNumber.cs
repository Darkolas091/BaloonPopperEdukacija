using UnityEngine;

public static class RandomGeneratedNumber
{
    public static int RandomNumber(int number)
    {
        int random = Random.Range(0, number);
        return random;
    }



}
