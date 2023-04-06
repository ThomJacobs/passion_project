using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledProgrammer.Inventory
{
    public sealed class InventoryManager : MonoBehaviour
    {
        //Attributes:
        private Dictionary<uint, Inventory> m_inventories = new Dictionary<uint, Inventory>();
        private static InventoryManager manager = null;
        private int m_current_id = default;

        //Properties:
        public static InventoryManager World
        {
            get
            {
                if(manager == null)
                {
                    //Locate a inventory manager instance from the currently loaded scene.
                    manager = FindObjectOfType<InventoryManager>();

                    //If an inventory manager is not present in scene create a new one.
                    if (manager == null) manager = new GameObject("inventory_manager").AddComponent<InventoryManager>();
                }

                return manager;
            }
        }

        //Methods:
        public Inventory Get(uint id)
        {
            if (IsActive(id)) return null;

            return m_inventories[id];
        }

        public Inventory Add(uint id)

        public bool IsActive(uint id) { return m_inventories.ContainsKey(id); }
    }
}