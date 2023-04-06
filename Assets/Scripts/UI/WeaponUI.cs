using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mamba.UI
{
    public class WeaponUI : MonoBehaviour
    {
        #region Attributes
        [SerializeField] TMPro.TextMeshProUGUI m_ammoTextBox;
        [SerializeField] TMPro.TextMeshProUGUI m_magazineTextBox;
        #endregion

        #region Methods
        public void UpdateAmmo(int p_value)
        {
            m_ammoTextBox.text = p_value.ToString();
        }

        public void UpdateMagazine(int p_value)
        {
            m_ammoTextBox.text = p_value.ToString();
        }
        #endregion
    }
}