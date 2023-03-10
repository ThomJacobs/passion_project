using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    public interface Item
    {
        #region Methods
        void OnPickup();
        void OnDrop();
        void OnUse();
        #endregion
    }
}