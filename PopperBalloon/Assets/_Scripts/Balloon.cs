using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
   [SerializeField] private int clicksToPop = 3;
   public Material[] balloonMaterials;
   [SerializeField] private float lifetime = 5;
   
   private MeshRenderer meshRenderer;
   private Rigidbody rigidbody;

   private void Awake()
   {
      meshRenderer = GetComponent<MeshRenderer>();
      rigidbody = GetComponent<Rigidbody>();
   }

   private void Start()
   {
      RandomBalloonSpeed();
      
      //Invoke(nameof(DestroyBalloon), lifetime);
      StartCoroutine(BalloonFade());
   }

   private IEnumerator BalloonFade()
   {
        // Preuzima boju materiala
      Color color = meshRenderer.materials[0].color;

        // Čeka da lifetime istekne
      yield return new WaitForSeconds(lifetime);

        // Postepeno mijenja prozirnost materiala
      for (float alpha = 1; alpha >= 0; alpha -= 0.1f)
      { 
         color.a = alpha;
         meshRenderer.materials[0].color = color;
        
            // Brzina mijenjanja prozirnosti
         yield return new WaitForSeconds(.1f);
      }


      // Uništava balon nakon što se sve prethodno obavi i makne zivot
      DestroyBalloon();
   }
   
   

   private void KillYourself()
   {
      Destroy(gameObject);
   }

    private IEnumerator IncreaseScale()
    {
        // Originalna veličina
        float originalScale = transform.localScale.x;

        // Postepeno mijenja veličinu balona
        for (float scale = transform.localScale.x; scale <= originalScale + 0.2f; scale += 0.01f)
        {
            transform.localScale = new Vector3(scale, scale, scale);

            // Brzina mijenjanja veličine
            yield return new WaitForSeconds(.01f);
        }
    }

   private void OnMouseDown()
   {
      if (Time.timeScale == 0)
      {
         return;
      }
      clicksToPop--;
      if (clicksToPop <= 0)
      {
         AudioManager.Instance.PlayPopSound();
         GameManager.Instance.IncreaseScore();
         Destroy(gameObject);
      }

      else
      {
         AudioManager.Instance.PlayFillSound();
      }
      StartCoroutine(IncreaseScale());
      //transform.localScale += Vector3.one * 0.2f;
      Debug.Log($"Pressed");
   }

   public void ChangeMaterial(int index)
   {
      meshRenderer.material = balloonMaterials[index];
   }

   private void RandomBalloonSpeed()
   {
      rigidbody.linearDamping = UnityEngine.Random.Range(3, 10);
   }

   private void DestroyBalloon()
   {
      GameManager.Instance.RemoveLife();
      Destroy(gameObject);
   }
}
