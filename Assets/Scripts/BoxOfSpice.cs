using UnityEngine;

namespace DefaultNamespace
{
    public class BoxOfSpice : MonoBehaviour
    {
        [SerializeField] private int boxOfSpiceHP = 10;
        
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.attachedRigidbody.CompareTag("EnemyBullet") || col.attachedRigidbody.CompareTag("EnemyTouch")) {
                TakeDamage();
            }
            
        }
        
        private void TakeDamage() {
            boxOfSpiceHP--;

            if (boxOfSpiceHP <= 0)
            {
                Die();
            }
        }
        
        public void Die() {
            gameObject.SetActive(false);
        }
    }
}