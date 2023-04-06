using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.Core
{
    [System.Serializable]
    public struct Axis
    {
        //Attributes:
        public KeyCode m_positiveKey;
        public KeyCode m_negativeKey;
        public float m_sensitivity;
        private const float NEGATIVE = -1.0f, POSITIVE = 1.0f, NEATURAL = 0.0f;

        //Properties:
        public float Value
        {
            get
            {
                if (Input.GetKey(m_positiveKey)) return POSITIVE * m_sensitivity;
                return m_sensitivity * (Input.GetKey(m_negativeKey) ? NEGATIVE : NEATURAL);
            }
        }
    }

    [RequireComponent(typeof(Camera))]
    public class Follow : MonoBehaviour
    {
        #region Attributes
        [Header("Follow Settings")]
        public Vector2 m_offset;
        public float m_force;
        public Transform m_target;
        private Camera m_camera;
        [Header("Control Settings")]
        public Axis m_zoomAxis;
        public NamelessProgrammer.Additional.Range m_zoomRange;
        #endregion

        #region Properties
        public Vector2 TargetPosition { get => m_target.position == null ? transform.position : m_target.transform.position; }
        #endregion

        #region Methods
        private void Awake()
        {
            //Initialise components.
            m_camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            //Movement.
            Vector2 targetDirection = (m_offset + TargetPosition) - (Vector2)transform.position;
            transform.position += (Vector3)(targetDirection * m_force) * Time.deltaTime;

            //Camera adjustment.
            m_camera.orthographicSize = m_zoomRange.Clamp(Time.deltaTime * m_zoomAxis.Value + m_camera.orthographicSize);
        }
        #endregion
    }
}