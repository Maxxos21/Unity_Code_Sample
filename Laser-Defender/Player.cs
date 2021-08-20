using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //configuration parameter 

    [Header("Player")]
    [SerializeField] [Range(0f, 50f)] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.25f;




    [Header("Projectile")]
    [SerializeField] [Range(0f, 50f)] float projectileSpeed = 5f;
    [SerializeField] [Range(0f, 50f)] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject laserPrefab;
    

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;



    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Shooting();
    }

    private void Move()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()

    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    public int GetHealth()
    {
        return health;
    }
}
