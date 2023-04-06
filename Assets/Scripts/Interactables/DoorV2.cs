using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    public class DoorV2 : MonoBehaviour
    {
        #region Attributes
        public Vector2 m_pivot;
        public float m_eulerAngles;
        public KeyCode m_actionKey;
        public UnityEngine.Events.UnityEvent m_onOpen;
        #endregion

        #region Methods
        private void Awake()
        {
            m_pivot += (Vector2)transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(m_pivot + (Vector2)transform.position, Quaternion.Euler(0.0f, 0.0f, m_eulerAngles), Gizmos.matrix.lossyScale);
            Gizmos.DrawWireCube(new Vector2(-1,0), new Vector2(2, 2));
            Gizmos.DrawSphere(Vector2.zero, 0.1f);
        }
        #endregion
    }
}