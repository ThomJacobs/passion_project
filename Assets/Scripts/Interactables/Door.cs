using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Door : MonoBehaviour
    {
        #region Attributes
        public Additional.Rectangle m_enterRect;
        public Additional.Rectangle m_exitRect;
        [SerializeField] private LayerMask m_layerMask;
        [SerializeField] private Vector2 m_pivot;
        [SerializeField] private float m_speed;
        private const int ENTER = 1, EXIT = -1;
        #endregion

        #region Properties
        private Vector2 Pivot { get => (Vector2)transform.position + m_pivot; }
        #endregion

        #region Methods
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = false;
        }

        private void LateUpdate()
        {
            //Check for enter rectangle.
            if(Physics2D.OverlapBox(m_enterRect.centre + (Vector2)transform.position, m_enterRect.size, transform.eulerAngles.z, m_layerMask))
            {
                transform.RotateAround(Pivot, Vector3.forward, m_speed * Time.deltaTime * ENTER);
            }

            //Check for exit rectangle.
            else if(Physics2D.OverlapBox(m_exitRect.centre + (Vector2)transform.position, m_exitRect.size, transform.eulerAngles.z, m_layerMask))
            {
                transform.RotateAround(Pivot, Vector3.forward, m_speed * Time.deltaTime * EXIT);
            }
        }

        /*
        private void OnCollisionStay2D(Collision2D collision)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            transform.RotateAround(Pivot, Vector3.forward, m_speed * Time.deltaTime);
        }
        */
        #endregion

        #region FOR_DEVELOPMENT_ONLY
#if UNITY_EDITOR
        //Attributes:
        private const float RADIUS = 0.02f;

        //Methods:
        private void OnDrawGizmos()
        {
            //Pivot
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(Pivot, RADIUS * transform.lossyScale.magnitude);

            //Apply rotation and position to Gizmo's transformation matrix.
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Gizmos.matrix.lossyScale);

            //Exit
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_exitRect.centre, m_exitRect.size);

            //Enter
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(m_enterRect.centre, m_enterRect.size);
        }
#endif
        #endregion
    }
}