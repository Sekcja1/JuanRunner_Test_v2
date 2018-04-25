﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
    public string Name { get; set; }
    public float Damage { get; set; }
    public float FireRate { get; set; }
    public int AmmoCount { get; set; }

    public bool Splash { get; set; }
    public float SplashDamage { get; set; }
    public float SplashRange { get; set; }
    public Sprite WeaponSprite { get; set; }

    public GameObject AmmoPrefab { get; set; }
    public GameObject WeaponGameObject { get; set; }

    public float ProjectileSpeed { get; set; }
    public float ProjectileRange { get; set; }

    public void Shoot(Vector3 playerPosition, Vector3 clickPosition, Quaternion rotation)
    {
        if (AmmoCount > 0)
        {
            var buff = clickPosition;

            var bullet = Instantiate(AmmoPrefab, playerPosition, rotation);

            bullet.GetComponent<Rigidbody2D>().AddForce(
                new Vector2((buff.x - playerPosition.x), (buff.y - playerPosition.y)),
                ForceMode2D.Impulse);

            Destroy(AmmoPrefab, ProjectileRange);

            AmmoCount--;
            Debug.Log("Zostało ammo: " + AmmoCount + " dla " + Name);
        }
    }

    void OnDestroy()
    {
        if (Splash)
        {
            var element = Physics2D.OverlapCircleAll(transform.position, SplashRange);

            Debug.Log(element);

            Debug.Log("Ilość obiektów w zasięgu: " + element.Length);

            for(var i=0;i<element.Length;i++)
            //foreach (var item in element)
            {
                element[i]./*item.*/gameObject.SendMessage("AddDamage", 100.0f);
                Debug.Log(element[i].gameObject.name);
            }
        }
    }

}
