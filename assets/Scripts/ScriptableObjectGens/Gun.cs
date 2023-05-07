using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public GameObject prefab;
    public float aimSpeed;
    public int totalAmmo;
    public float fireRate;
    public AudioClip Shoot_snd;
}
