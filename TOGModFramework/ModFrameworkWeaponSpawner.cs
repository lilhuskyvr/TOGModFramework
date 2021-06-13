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

        public static event WeaponsLoadedEvent OnWeaponsLoaded;

        public delegate void WeaponsLoadedEvent();

        public static void InvokeWeaponsLoadedEvent()
        {
            if (OnWeaponsLoaded != null)
                OnWeaponsLoaded();
        }

        public GameObject SpawnWeapon(int index, Vector3 position, Quaternion rotation)
        {
            var weapon = Object.Instantiate(weapons[index]);
            weapon.SetActive(true);
            weapon.transform.position = position;
            weapon.transform.rotation = rotation;
            weapon.GetComponentInChildren<WepDesc>().EnableHandle(Time.time);

            return weapon;
        }

        public GameObject SpawnARandomWeapon(Vector3 position, Quaternion rotation)
        {
            var index = Random.Range(0, weapons.Count);

            return SpawnWeapon(index, position, rotation);
        }
    }
}