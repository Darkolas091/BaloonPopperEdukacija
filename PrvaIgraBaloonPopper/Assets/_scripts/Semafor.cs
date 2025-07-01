using System.Collections;
using UnityEngine;

public class Semafor : MonoBehaviour
{
    [SerializeField] private GameObject[] semafor;
    bool isRedNext = false;
    bool firstTime = true;

    private void Start()
    {
        StartCoroutine(RedSemafor());
    }

    private IEnumerator RedSemafor()
    {
        isRedNext = false;

        semafor[1].SetActive(false);
        semafor[0].SetActive(true);
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(YellowSemafor());
    }
    private IEnumerator YellowSemafor()
    {
        if (isRedNext)
        {
            semafor[2].SetActive(false);
        }
        else if (!isRedNext)
        {
            semafor[0].SetActive(false);
        }

        semafor[1].SetActive(true);
        yield return new WaitForSeconds(2f);

        if (isRedNext)
        {
            yield return StartCoroutine(RedSemafor());
        }
        else if (!isRedNext)
        {
            yield return StartCoroutine(GreenSemafor());
        }

    }
    private IEnumerator GreenSemafor()
    {
        semafor[1].SetActive(false);
        isRedNext = true;
        semafor[2].SetActive(true);
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(YellowSemafor());
    }
}
