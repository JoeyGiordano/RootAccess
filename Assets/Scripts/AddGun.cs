using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGun : MonoBehaviour
{
    [SerializeField] Shooting weaponHandler;
    [SerializeField] string gunName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            weaponHandler.addWeapon(gunName);
            Destroy(gameObject);
        }
    }
}
