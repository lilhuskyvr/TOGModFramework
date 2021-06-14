using UnityEngine;

namespace TOGModFramework
{
    public class EventManager : MonoBehaviour
    {
        public static event CrossbowBoltFiredEvent OnCrossbowBoltFired;

        public static event PlayerLoadedEvent OnPlayerLoaded;

        public static event WeaponJointCreatedEvent OnWeaponJointCreated;

        public static event WeaponOnCollisionEnteredEvent OnWeaponCollisionEntered;

        public delegate void CrossbowBoltFiredEvent(CrossBowScript crossBowScript);

        public delegate void PlayerLoadedEvent();

        public delegate void WeaponJointCreatedEvent(WepDesc weapon, ConfigurableJoint joint);

        public delegate void WeaponOnCollisionEnteredEvent(WepDesc weapon, Collision collision, Stats source,
            Stats target);

        public static void InvokeCrossbowBoltFiredEvent(CrossBowScript crossBowScript)
        {
            if (OnCrossbowBoltFired != null)
                OnCrossbowBoltFired(crossBowScript);
        }


        public static void InvokePlayerLoadedEvent()
        {
            if (OnPlayerLoaded != null)
                OnPlayerLoaded();
        }

        /**
         * To make weapon handling easier or harder
         */
        public static void InvokeWeaponJointCreatedEvent(WepDesc weapon, ConfigurableJoint joint)
        {
            if (OnWeaponJointCreated != null)
                OnWeaponJointCreated(weapon, joint);
        }

        public static void InvokeWeaponOnCollisionEnteredEvent(WepDesc weapon, Collision collision, Stats source,
            Stats target)
        {
            if (OnWeaponCollisionEntered != null)
                OnWeaponCollisionEntered(weapon, collision, source, target);
        }
    }
}