using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{

    [SerializeField] private int clicksToPop = 3;
    public int balloonSpeed = 5;

    public Material[] balloonMaterials;
    private GameManager gameManager;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private float lifetime;


    private MeshRenderer meshRenderer;
    private Rigidbody rigidbody;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        //Invoke(nameof(DestroyBallon),lifetime);
        StartCoroutine(BaloonFade());
        //StartCoroutine(IspisZadatak());
        InvokeRepeating(nameof(WriteSomething), 1f, 0.5f);
    }

    private void WriteSomething()
    {
        Debug.Log("WriteSomething called");
    }

    private IEnumerator IncreaseScale()
    {
        float originalScale = transform.localScale.x;
        // Gradually increase the scale of the balloon
        for (float scale = transform.localScale.x; scale <= originalScale + 0.2; scale += 0.1f)
        {
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator BaloonFade()
    {
        // Set the initial color of the balloon to fully opaque
        Color color = meshRenderer.materials[0].color;

        for (float alpha = 1; alpha >= 0; alpha -= 0.05f)
        {
            // Update the alpha value of the color
            color.a = alpha;
            // Apply the updated color to the material
            meshRenderer.materials[0].color = color;
            // Wait for a short duration before the next update
            yield return new WaitForSeconds(0.1f);
        }
        // After the loop, destroy the balloon object
        DestroyBallon();
    }

    private IEnumerator IspisZadatak()
    {
        Debug.Log("start");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"Ispisujem {i}");
        }
        Debug.Log("done");
    }

    private void OnMouseDown()
    {
        //povecavamo scale za 0.2f
        clicksToPop--;
        if (clicksToPop <= 0)
        {
            AudioManager.instance.PlayPopSound();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(IncreaseScale());
            AudioManager.instance.PlayFillSound();
        }
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        //transform.localScale += Vector3.one * 0.2f;
        Debug.Log($"pressed and enlarged");
    }

    public void ChangeMaterial(int index)
    {
        meshRenderer.material = balloonMaterials[index];

    }

    public void ChangeBalloonSpeed(int index)
    {
        rigidbody.linearDamping = Random.Range(0, index);
    }


    private void DestroyBallon()
    {
        Destroy(gameObject);
    }

}
