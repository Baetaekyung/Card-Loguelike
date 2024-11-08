using System;
using UnityEngine;

namespace CardGame
{
    public class LookCameraUI : MonoBehaviour
    {
        private Transform cam;

        private void Awake()
        {
            cam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}
