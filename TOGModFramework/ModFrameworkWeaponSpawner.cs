using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TOGModFramework
{
    public class ModFrameworkWeaponSpawner
    {
        public static ModFrameworkWeaponSpawner local;
        public List<GameObject> weapons = new List<GameObject>();
        public bool loaded;
    }
}