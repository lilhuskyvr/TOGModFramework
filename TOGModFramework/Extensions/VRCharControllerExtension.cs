using UnityEngine;

namespace TOGModFramework.Extensions
{
    public static class VRCharControllerExtension
    {
        public static bool IsFlying(this VRCharController player)
        {
            RaycastHit hitInfo;
            var hasGroundTarget = Physics.Raycast(
                Player.local.transform.position,
                -Player.local.transform.up, out hitInfo, 1000, 1 << 0);

            if (hasGroundTarget)
                return Vector3.Distance(Player.local.transform.position, hitInfo.point) >= 1;
            return false;
        }
    }
}