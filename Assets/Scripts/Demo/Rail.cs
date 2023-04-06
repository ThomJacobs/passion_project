using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.Demo
{
    /**
     * Moves a game object between a start and end point in world space.
     */
    public sealed class Rail : MonoBehaviour
    {
        #region Attributes
        public float m_delay = default;
        public Vector2 m_startPoint;
        public Vector2 m_endPoint;
        private float m_currentTime = default;
        private const float TOLERANCE = 0.01f;
        #endregion

        #region Methods
        private void Awake()
        {
            m_startPoint += (Vector2)transform.position;
            m_endPoint += (Vector2)transform.position;
        }

        private void Update()
        {
            //If the delay has been met, swap the start and end point positions.
            if (Vector2.Distance(transform.position, m_endPoint) <= TOLERANCE) Swap();

            //Lerp game object between two world space positions relative to current time.
            m_currentTime += Time.deltaTime;
            transform.position = (Vector3)Vector2.Lerp(m_startPoint, m_endPoint, m_currentTime / m_delay) + (transform.position.z * Vector3.forward);
        }

        private void Swap()
        {
            m_currentTime = default;
            Vector2 temp = m_startPoint;
            m_startPoint = m_endPoint;
            m_endPoint = temp;
        }
        #endregion

        #region DEVELOPMENT
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector3)m_startPoint + transform.position, (Vector3)m_endPoint + transform.position);
        }
#endif
        #endregion
    }
}