using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QJ1
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float timeBetweenShots = 5.0f;
        [SerializeField] private float randomTolerance = 2.5f;
        private Coroutine cr_shoot;
        [SerializeField] private GameObject projectilePrefab;
        private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private float shotAngleTolerance = 1.0f;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Start is called before the first frame update
        void Start()
        {

            Vector3 direction = Player.Instance.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            this.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90);

            CoroutineManager.BeginCoroutine(ShootRoutine(), ref cr_shoot, this);
        }

        // Update is called once per frame
        void Update()
        {
        }

        private IEnumerator ShootRoutine()
        {
            float t;
            while (Player.Instance.IsAlive) {
                t = timeBetweenShots + UnityEngine.Random.Range(-randomTolerance, randomTolerance);
                while(t > 0) {
                    t -= Time.deltaTime;
                    yield return null;
                }
                Shoot();
                timeBetweenShots -= 0.1f;
            }
        }

        private void Shoot()
        {
            audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            audioSource.Play();
            Projectile p = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            Vector3 fireAngle = new Vector3(transform.position.x + UnityEngine.Random.Range(-shotAngleTolerance, shotAngleTolerance), transform.position.y + UnityEngine.Random.Range(-shotAngleTolerance, shotAngleTolerance), transform.position.z);
            p.SetVelocity( -fireAngle);
        }
    }
}

