using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WagonWalls: MonoBehaviour {
        [SerializeField] private int wagonWallsHP = 5;

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.attachedRigidbody.CompareTag("EnemyBullet") || col.attachedRigidbody.CompareTag("EnemyTouch")) {
                TakeDamage();
            }
            
        }
        
        private void TakeDamage() {
            wagonWallsHP--;

            if (wagonWallsHP <= 0)
            {
                Die();
            }
        }
        
        public void Die() {
            gameObject.SetActive(false);
        }
    }
}