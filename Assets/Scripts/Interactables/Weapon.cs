using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [System.Serializable]
    public enum WeaponType
    {
        STATIC = 0,
        AUTOMATIC = 1
    }

    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Weapon : MonoBehaviour, Item
    {
        #region Attributes
        [SerializeField] private int m_maxRounds;
        [SerializeField] private int m_maxClips;
        [SerializeField] private WeaponType m_type;
        private int m_roundsFired;
        private int m_clipsUsed;
        private SpriteRenderer m_spriteRenderer;
        private Animator m_animator;
        private const int LEFT_MOUSE_BUTTON = 0;
        #endregion

        #region Properties
        protected bool DetectInput { get => m_type == WeaponType.AUTOMATIC ? Input.GetMouseButton(LEFT_MOUSE_BUTTON) : Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON); }
        #endregion

        #region Methods
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_animator = GetComponent<Animator>();
        }
        public void OnDrop()
        {
            throw new System.NotImplementedException();
        }

        public void OnPickup()
        {
            throw new System.NotImplementedException();
        }

        public void OnUse()
        {
            //Fire.
            if (DetectInput && m_roundsFired < m_maxRounds)
            {
                Debug.Log("Fire");
                m_roundsFired++;
            }

            //Reload.
            else if (Input.GetKeyDown(KeyCode.R) && m_clipsUsed < m_maxClips)
            {
                Debug.Log("Reload");
                m_roundsFired = default;
                m_clipsUsed++;
            }

            else { Debug.Log("Empty"); }
        }
        #endregion

    }
}