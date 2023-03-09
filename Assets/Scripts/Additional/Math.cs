using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer.Additional
{
    [System.Serializable]
    public struct Rectangle
    {
        #region Attributes
        public Vector2 centre;
        public Vector2 size;
        #endregion
    }

    /**
     *  A simple math library to wrap up frequently used arithmetic.
     * 
     *  @owner Thomas Jacobs.
     *  @project Project Mamba.
     */
    public static class Math2D
    {
        #region Attributes
        public const float RIGHT_EULER_ANGLE = 90.0f;
        #endregion

        #region Methods

        /**
         *  Rotates a transform to look towards the specifed target position.
         * 
         *  @param p_target: The vector intended for looking towards.
         *  @param p_focus: The transform intended for rotation.
         */
        public static void LookAt(Transform p_focus, Vector3 p_target)
        {
            Vector3 difference = (p_target - p_focus.position).normalized;

            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            p_focus.rotation = Quaternion.Euler(default, default, rotation_z - RIGHT_EULER_ANGLE);
        }

        /**
         *  Produces a quaternion that directs a transform's rotation to the specifed target position.
         * 
         *  @param p_target: The vector intended for looking towards.
         *  @param p_focus: The transform intended for rotation.
         *  @return The resulting rotation.
         */
        public static Quaternion LookAtQuaternion(Transform p_focus, Vector3 p_target)
        {
            Vector3 difference = (p_target - p_focus.position).normalized;

            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(default, default, rotation_z - RIGHT_EULER_ANGLE);
        }
        #endregion
    }
}