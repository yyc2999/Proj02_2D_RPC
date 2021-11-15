using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPGM.Gameplay
{
    /// <summary>
    /// A simple camera follower class. It saves the offset from the
    ///  focus position when started, and preserves that offset when following the focus.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Transform focus;
        public float smoothTime = 2;

        public Vector3 offset;

        void Awake()
        {
            transform.position = focus.position;
        }

        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, focus.position - offset, Time.deltaTime * smoothTime);
        }
    }
}