using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [System.Serializable]
    public struct SprintMeta
    {
        #region Attributes
        [SerializeField] private float m_walkSpeed;
        [SerializeField] private float m_sprintSpeed;
        [SerializeField] private KeyCode m_sprintKey;
        #endregion

        #region Properties
        public float Speed { get => Input.GetKey(m_sprintKey) ? m_sprintSpeed : m_walkSpeed; }
        #endregion
    }

    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D), typeof(Rigidbody2D))]
    public sealed class Player2D : MonoBehaviour
    {
        #region Attributes
        [Header("Rotation")]
        public float m_rotationSpeed;
        [Header("Movement")]
        public SprintMeta m_sprintMeta;
        private SpriteRenderer m_spriteRenderer;
        private Rigidbody2D m_rigidbody;
        #endregion

        #region Properties
        public float HorizontalInput { get => Input.GetAxis("Horizontal"); }
        public float VerticalInput { get => Input.GetAxis("Vertical"); }
        public float RotationInput { get => Input.GetAxis("Rotation"); }
        public Vector2 MousePosition { get => Camera.main.ScreenToWorldPoint(Input.mousePosition); }
        #endregion

        #region Methods
        private void Awake()
        {
            //Initialise component variables.
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Rotation.
            transform.rotation = Additional.Math2D.LookAtQuaternion(transform, MousePosition);

            //Movement.
            m_rigidbody.MovePosition(transform.position + Time.deltaTime * m_sprintMeta.Speed * (Vector3.up * VerticalInput + Vector3.right * HorizontalInput));
        }
        #endregion
    }
}