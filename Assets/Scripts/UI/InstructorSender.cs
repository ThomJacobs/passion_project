using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer.UI
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class InstructorSender : MonoBehaviour
    {
        #region Attributes
        [SerializeField] private string m_message;
        private Instructor m_instructor;
        #endregion

        #region Methods
        private void Awake()
        {
            m_instructor = Instructor.SceneInstance;
            GetComponent<Collider2D>().isTrigger = true;

#if UNITY_EDITOR
            if (m_instructor == null)
            {
                Debug.LogError("IntructorSender: Could not find instructor instance in scene.");
                Destroy(this);
            }
#endif
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Updating Text");
            m_instructor.UpdateText(m_message);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            m_instructor.ClearText();
        }
        #endregion
    }
}