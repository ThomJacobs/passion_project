using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Inventory : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private KeyCode m_dropKey;
        [SerializeField] private KeyCode m_interactKey;
        [SerializeField] private Weapon m_currentWeapon;
        [SerializeField] private Rect m_interactionArea;
        #endregion

        #region Properties
        public bool IsEmpty { get => m_currentWeapon == null; }
        public Collider2D BoxCast { get => Physics2D.OverlapBox(transform.position + (Vector3)m_interactionArea.position, m_interactionArea.size, transform.eulerAngles.z); }
        public float RandomEulerAngle { get => Random.Range(0.0f, 360.0f); }
        #endregion

        #region Methods
        private bool TryBoxCast(out Weapon p_weapon)
        {
            p_weapon = BoxCast.gameObject.GetComponent<Weapon>();
            return p_weapon == null ? false : true;
        }

        private void Awake()
        {
            if (!m_currentWeapon) return;
            SetActiveWeapon(m_currentWeapon);
        }

        public void SetActiveWeapon(Weapon p_weapon)
        {
            if (!IsEmpty) DropActiveWeapon();

            m_currentWeapon = p_weapon;
            m_currentWeapon.transform.position = transform.position;
            m_currentWeapon.transform.SetParent(transform);
            m_currentWeapon.transform.localPosition = Vector3.zero;
            m_currentWeapon.transform.localEulerAngles = Vector3.zero;
            m_currentWeapon.OnPickup();
        }

        private void DropActiveWeapon()
        {
            if (!m_currentWeapon) return;

            m_currentWeapon.OnDrop();
            m_currentWeapon.transform.SetParent(null);
            m_currentWeapon.transform.localEulerAngles = Vector3.forward * RandomEulerAngle; //Random rotation.
            m_currentWeapon = null;
        }

        private void Update()
        {
            //Drop currently selected/active weapon.
            if(m_currentWeapon && Input.GetKeyDown(m_dropKey))
            {
                m_currentWeapon.OnDrop();
                m_currentWeapon.transform.SetParent(null);
                m_currentWeapon = null;
            }

            //Pickup new weapon.
            else if(Input.GetKeyDown(m_interactKey) && TryBoxCast(out Weapon p_weapon))
            {
                SetActiveWeapon(p_weapon);
            }

            //Update current weapon if a current weapon is not null.
            else if (m_currentWeapon != null) m_currentWeapon.OnUse();
        }
        #endregion

        #region DEVELOPMENT_ONLY
#if UNITY_EDITOR
        //Attributes:
        private const float LENGTH = 0.1f;

        //Methods:
        private void OnDrawGizmos()
        {
            //Draw interaction area.
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Gizmos.matrix.lossyScale);
            Gizmos.DrawWireCube((Vector3)m_interactionArea.position, m_interactionArea.size);
        }
#endif
        #endregion
    }
}