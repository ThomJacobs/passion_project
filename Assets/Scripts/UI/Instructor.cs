using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NamelessProgrammer.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public sealed class Instructor : MonoBehaviour
    {
        #region Attributes
        private TMPro.TextMeshProUGUI m_textbox;
        private Text text;
        private static Instructor sceneSingleton;
        #endregion

        #region Properties
        public static Instructor SceneInstance
        {
            get
            {
                if (sceneSingleton == null) sceneSingleton = FindObjectOfType<Instructor>();
                return sceneSingleton;
            }
        }
        #endregion

        #region Methods
        private void Awake() => m_textbox = GetComponent<TMPro.TextMeshProUGUI>();

        [UnityEditor.MenuItem("GameObject/UI/Instructor - Instructor (UI)")]
        public static void Generate()
        {
            Instructor instructor = new GameObject("Instructor").AddComponent<Instructor>();
            instructor.m_textbox = instructor.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            instructor.transform.SetParent(FindObjectOfType<Canvas>().transform);
        }

        public void UpdateText(string p_text) => m_textbox.text = p_text;
        public void ClearText() => m_textbox.text = string.Empty;
        #endregion
    }
}