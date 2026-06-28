using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitss.Utility
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 5f;


        public void SetupTarget(Transform playerTarget) {
            target = playerTarget;
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 targetPos = new Vector3(
                target.position.x,
                target.position.y,
                transform.position.z);

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                smoothSpeed * Time.deltaTime);
        }
    }
}
