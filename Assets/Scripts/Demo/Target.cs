using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.Demo
{
    /**
     * An destructable component which changes sprite when shot by the player.
     */
    public class Target : MonoBehaviour, Interactable.Destructable
    {
        #region Attributes
        [SerializeField] private Sprite m_activeSprite;
        private SpriteRenderer m_spriteRenderer;
        #endregion

        #region Methods
        private void Awake()
        {
            if (m_activeSprite == null) Destroy(this);
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnDestruct()
        {
            m_spriteRenderer.sprite = m_activeSprite; //Switch to the activated sprite.
            Destroy(this); //Destroy this script since it no longer serves any purpose.
        }
        #endregion
    }
}