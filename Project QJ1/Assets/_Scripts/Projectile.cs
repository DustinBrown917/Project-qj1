using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QJ1 {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D rb2d;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float damage;
        [SerializeField] private float damageTolerance;
        private AudioSource audioSource;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            Destroy(gameObject, 5.0f);
        }

        public void SetVelocity(Vector2 v)
        {
            v = v.normalized * maxSpeed;

            rb2d.velocity = v;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Player p = collision.gameObject.GetComponent<Player>();

            if (p != null) {
                p.Damage(damage + UnityEngine.Random.Range(-damageTolerance, damageTolerance));
                audioSource.Play();
            }

            Destroy(gameObject);
        }
    }
}


