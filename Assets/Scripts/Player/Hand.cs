using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Hand : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private Vector2 m_forwardDirection;
        [SerializeField] private Weapon m_currentWeapon;
        #endregion

        #region Properties
        public Vector2 LocalForward { get => transform.TransformDirection(m_forwardDirection.normalized); }
        #endregion

        #region Methods
        private void Update()
        {
            if (m_currentWeapon != null) m_currentWeapon.OnUse();
        }
        #endregion

        #region DEVELOPMENT_ONLY
#if UNITY_EDITOR
        //Attributes:
        private const float LENGTH = 0.1f;

        //Methods:
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine((Vector2)transform.position + (LocalForward * LENGTH * transform.lossyScale.magnitude), transform.position);
        }
#endif
        #endregion
    }
}