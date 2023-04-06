using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.Interactable
{
    [System.Serializable]
    public enum WeaponType
    {
        STATIC = 0,
        AUTOMATIC = 1
    }

    [RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(LineRenderer))]
    public class Weapon : MonoBehaviour, NamelessProgrammer.Item
    {
        #region Attributes
        [Header("Main Settings")]
        [SerializeField] private float m_offset;
        [SerializeField] private Vector2 m_fireDirection;
        [SerializeField] private float m_range;

        [Header("Ammunition")]
        [SerializeField] private int m_maxRounds;
        [SerializeField] private int m_maxClips;
        [SerializeField] private WeaponType m_type;

        [Header("Effects")]
        [SerializeField] private AudioClip m_audioClip;

        private int m_roundsFired;
        private int m_clipsUsed;
        private SpriteRenderer m_spriteRenderer;
        private Animator m_animator;
        private LineRenderer m_lineRenderer;
        private AudioSource m_audioSource;
        private Mamba.UI.WeaponUI m_weaponUI;
        private const int LEFT_MOUSE_BUTTON = 0;
        private const int MAX_POSITIONS = 2;
        private const int START_POINT_INDEX = 0, END_POINT_INDEX = 1;
        #endregion

        #region Properties
        protected bool DetectInput { get => m_type == WeaponType.AUTOMATIC ? Input.GetMouseButton(LEFT_MOUSE_BUTTON) : Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON); }
        protected Vector2 TransformDirection { get => transform.TransformDirection(m_fireDirection); }
        protected Vector2 StartPoint { get => transform.position + transform.TransformDirection(m_fireDirection) * m_offset; }
        protected Vector2 EndPoint { get => StartPoint + TransformDirection * m_range; }
        #endregion

        #region Methods
        private void Awake()
        {
            //Initialise components.
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_animator = GetComponent<Animator>();
            m_lineRenderer = GetComponent<LineRenderer>();
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_weaponUI = GetComponent<Mamba.UI.WeaponUI>();

            //Setup line renderer.
            m_lineRenderer.positionCount = MAX_POSITIONS;
            m_lineRenderer.useWorldSpace = true;
        }
        public void OnDrop()
        {
            Debug.Log("OnDrop Invoke.");
            m_lineRenderer.enabled = false;
        }

        public void OnPickup()
        {
            Debug.Log("OnPickup Invoke.");
            m_lineRenderer.enabled = true;
        }

        public void OnUse()
        {
            //Animate line/direction of fire.
            RaycastHit2D endPoint = Physics2D.Raycast(StartPoint, TransformDirection, m_range);
            m_lineRenderer.SetPosition(START_POINT_INDEX, StartPoint);
            m_lineRenderer.SetPosition(END_POINT_INDEX, endPoint.collider == null ? EndPoint : endPoint.point);

            //Fire.
            if (DetectInput && m_roundsFired < m_maxRounds)
            {
                //Account for fire.
                m_roundsFired++;
                m_audioSource.PlayOneShot(m_audioClip);
                StartCoroutine(Fire());

                //Raycast.
                if (endPoint.collider == null) return;
                Interactable.Destructable target = endPoint.collider.gameObject.GetComponent<Interactable.Destructable>();
                if (target != null) target.OnDestruct();
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

        private IEnumerator Fire()
        {
            m_lineRenderer.startColor = Color.green;
            yield return new WaitForSecondsRealtime(0.25f);
            m_lineRenderer.startColor = Color.red;
        }
        #endregion

        #region DEVELOPMENT_ONLY
#if UNITY_EDITOR
        //Attributes:
        private const float RADIUS = 0.01f;

        //Methods:
        private void OnDrawGizmos()
        {
            //Draw direction/line of fire.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(StartPoint, EndPoint);

            //Draw start point.
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(StartPoint, RADIUS * transform.lossyScale.magnitude);
        }
#endif
        #endregion

    }
}