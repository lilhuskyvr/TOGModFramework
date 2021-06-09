using UnityEngine;

namespace TOGModFramework
{
    public class EventManager : MonoBehaviour
    {
        public static event CrossbowBoltFiredEvent OnCrossbowBoltFired;

        public static event PlayerLoadedEvent OnPlayerLoaded;

        public delegate void CrossbowBoltFiredEvent(CrossBowScript crossBowScript);

        public delegate void PlayerLoadedEvent();

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
    }
}