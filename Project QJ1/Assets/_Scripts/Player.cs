using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QJ1
{
    public class Player : MonoBehaviour
    {
        private static Player instance_;
        public static Player Instance { get { return instance_; } }
        private Rigidbody2D rb2d;
        [SerializeField] private float maxHealth_ = 100.0f;
        public float MaxHealth { get { return maxHealth_; } }
        private float health_ = 100.0f;
        public float Health { get { return health_; } }

        [SerializeField] private float rotationSpeed;

        [SerializeField] private Transform rotationAnchor;

        [SerializeField] private Transform particleTransform;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite deathSprite;
        [SerializeField] private GameObject gameOverScreen;

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] hurtClips;
        [SerializeField] private AudioClip deathClip;

        private bool isAlive_ = true;
        public bool IsAlive { get { return isAlive_; } }

        private void Awake()
        {
            if(instance_ != null)
            {
                Destroy(gameObject);
            } else
            {
                instance_ = this;
                rb2d = GetComponent<Rigidbody2D>();
                audioSource = GetComponent<AudioSource>();
            }
            
        }


        private void FixedUpdate()
        {
            if (!isAlive_) { return; }
            transform.RotateAround(rotationAnchor.position, Vector3.forward, Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed * -1);
        }

        private void OnDestroy()
        {
            if(instance_ == this)
            {
                instance_ = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile p = collision.GetComponent<Projectile>();

            if (p != null)
            {
                Vector3 direction = p.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x);
                particleTransform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90);
                ps.Emit(30);
            }
        }

        public void Damage(float howMuch)
        {
            health_ -= howMuch;
            OnDamaged();
            if(health_ <= 0 && isAlive_)
            {
                isAlive_ = false;
                spriteRenderer.sprite = deathSprite;
                OnDeath();
                gameOverScreen.SetActive(true);

                audioSource.clip = deathClip;
                audioSource.Play();
            } else
            {
                if (isAlive_)
                {
                    audioSource.clip = hurtClips[UnityEngine.Random.Range(0, hurtClips.Length)];
                    audioSource.Play();
                }
            }
        }

        public event EventHandler Death;

        private void OnDeath()
        {
            Death?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Damaged;

        private void OnDamaged()
        {
            Damaged?.Invoke(this, EventArgs.Empty);
        }
    }
}

