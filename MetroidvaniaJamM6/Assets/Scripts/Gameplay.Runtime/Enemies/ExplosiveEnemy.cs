using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExplosiveEnemy : MonoBehaviour
{
    private DamageableObject damageReceiver; //Make this a list?
    private Tween damage;
    private Tween move;
    [SerializeField] private Transform target;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triger enter");
        if (other.gameObject.CompareTag($"DamegeableObject"))
        {
            damageReceiver = other.gameObject.GetComponent<DamageableObject>();
            float progress = 0;
            damage = DOTween.To(() => progress, x => progress = x, 1, 2);
            damage.OnComplete(Damage);
            damage.Play();
            move.Pause();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger exit");
        if (other.gameObject.GetComponent<DamageableObject>() == damageReceiver)
        {
            damageReceiver = null;
            damage.Kill();
            move = transform.DOMove(target.position, 2);
        }
    }

    private void Damage()
    {
        damageReceiver.Damage(50);
        Debug.Log("damageReceiver.CurrentHealth = " + damageReceiver.CurrentHealth);
        Destroy(gameObject);
    }
    private void Start()
    {
        move = transform.DOMove(target.position, 2);
    }
}
