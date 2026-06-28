using Nitzz.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nitzz.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;
        [SerializeField] private Vector2 moveInput;
        [SerializeField] private bool isAutoMoving;
        [SerializeField] private Vector3 targetPosition;
        private UnityAction onReachTarget;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            enabled = SceneManager.GetActiveScene().name == "GamePlay"; 
        }

        private void Update()
        {
            if (isAutoMoving)
                return;

            if (!GameManager.Instance.canMove)
            {
                moveInput = Vector2.zero;
                return;
            }

            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();
        }

        private void FixedUpdate()
        {
            if (isAutoMoving)
            {
                Vector2 dir = ((Vector2)targetPosition - rb.position).normalized;

                rb.velocity = dir * moveSpeed;

                Debug.Log("Pop tsr" + targetPosition);
                Debug.Log("Pop rb" + rb.position);

                if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
                {
                    rb.velocity = Vector2.zero;

                    isAutoMoving = false;

                    onReachTarget?.Invoke();
                }

                return;
            }
            

            if (!GameManager.Instance.canMove)
            {
                return;
            }

            rb.velocity = moveInput * moveSpeed;
        }

        public void AutoMove(Vector3 target, UnityAction callback) {
            Debug.Log("Auto move");
            GameManager.Instance.canMove = false;
            targetPosition = target;

            isAutoMoving = true;

            onReachTarget = callback;
        }
    }
}