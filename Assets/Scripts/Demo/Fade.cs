using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.Demo
{
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public sealed class Fade : MonoBehaviour
    {
        //Attributes:
        public float m_transitionLength;
        public string m_targetTag;
        private SpriteRenderer m_spriteRenderer;
        private float m_currentTime;
        private float m_start = 0.0f, m_end = 1.0f;

        //Methods:
        private void Awake()
        {
            //Initialise components.
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void Update()
        {
            if (Mathf.Approximately(m_spriteRenderer.color.a, m_end)) return;

            m_currentTime += Time.deltaTime;
            m_spriteRenderer.color = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, Mathf.Lerp(m_start, m_end, m_currentTime / m_transitionLength));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != m_targetTag) return;

            m_currentTime = default;

            //Swap fade values.
            float temp = m_end;
            m_end = m_start;
            m_start = temp;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != m_targetTag) return;

            m_currentTime = default;

            //Swap fade values.
            float temp = m_end;
            m_end = m_start;
            m_start = temp;
        }
    }
}