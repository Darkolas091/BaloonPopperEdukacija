using UnityEngine;

public class FloorGrow : MonoBehaviour
{
    // Move the tree up by 0.1, then scale up for 5 seconds to scale of 10 from current scale
    [SerializeField] GameObject tree;

    private Vector3 initialScale;
    private Vector3 targetScale;
    private float duration = 5f;
    private float elapsed = 0f;
    private bool started = false;

    private int moveSteps = 10;
    private float moveAmount = 0.1f;
    private int movesDone = 0;

    void Start()
    {
        tree.transform.position += new Vector3(0, moveAmount, 0);
        initialScale = tree.transform.localScale;
        targetScale = initialScale * 20;
        started = true;
        movesDone = 1;
    }

    void Update()
    {
        if (started && elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            if (t < 0f) t = 0f;
            if (t > 1f) t = 1f;
            tree.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);



                int shouldHaveMoved = (int)((elapsed / duration) * moveSteps) + 1;
                while (movesDone < shouldHaveMoved && movesDone < moveSteps)
                {
                    tree.transform.position += new Vector3(0, moveAmount, 0);
                    movesDone++;
                }
        }
    }
}
