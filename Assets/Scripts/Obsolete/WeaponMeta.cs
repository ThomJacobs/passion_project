using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.Interactable
{
    [CreateAssetMenu(fileName = "WeaponMeta", menuName = "Mamba/Items", order = 0)]
    public class WeaponMeta : ScriptableObject
    {
        #region Attributes
        [SerializeField] private Sprite m_sprite;
        [SerializeField] private float m_delay;
        [SerializeField] private WeaponType type;
        private const int LEFT_MOUSE_BUTTON = 0;
        #endregion

        #region Properties
        public Sprite Sprite { get => m_sprite; }
        public bool DetectInput { get => type == WeaponType.AUTOMATIC ? Input.GetMouseButton(LEFT_MOUSE_BUTTON) : Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON); }
        #endregion
    }
}