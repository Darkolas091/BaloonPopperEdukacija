using UnityEngine;

public class Balloon : MonoBehaviour
{

    [SerializeField] private int clicksToPop = 3;
    public int balloonSpeed = 5;

    public Material[] balloonMaterials;
    private GameManager gameManager;
    [SerializeField] private AudioClip audioClip;


    private MeshRenderer meshRenderer;
    private Rigidbody rigidbody;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        //povecavamo scale za 0.2f
        clicksToPop--;
        if(clicksToPop <= 0)
        {
            AudioManager.instance.PlayPopSound();
            Destroy(gameObject);
        }
        else
        {
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
        rigidbody.linearDamping = Random.Range(0,index);
    }

}
