using System.Collections;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Renderer renderer;
    private Vector3[] positions;
    private Color[] colors;
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private float moveDuration = 1.5f;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        Vector3 start = transform.position;
        positions = new[]
        {
            start,
            start + Vector3.forward * 4f,
            start + Vector3.forward * 4f + Vector3.right * 4f
        };
        colors = new[] { Color.red, Color.green, Color.blue };
        StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        int idx = 0;
        while (true)
        {
            int nextIdx = (idx + 1) % positions.Length;
            yield return StartCoroutine(MoveAndFade(positions[nextIdx], colors[nextIdx], moveDuration));
            yield return new WaitForSeconds(waitTime);
            idx = nextIdx;
        }
    }

    private IEnumerator MoveAndFade(Vector3 targetPos, Color targetColor, float duration)
    {
        Vector3 startPos = transform.position;
        Color startColor = renderer.material.color;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            renderer.material.color = Color.Lerp(startColor, targetColor, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        renderer.material.color = targetColor;
    }
}
