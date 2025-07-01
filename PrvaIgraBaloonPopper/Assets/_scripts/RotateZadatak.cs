    using UnityEngine;
    using System.Collections;


    public class RotateZadatak : MonoBehaviour
    {
        [SerializeField] private float windSpeed = 1.0f;

        private void Update()
        {
            transform.Rotate(0f, 0f, 360f * windSpeed * Time.deltaTime);
        }
    }


