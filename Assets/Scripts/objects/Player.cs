using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Bullet bulletPrefab;

    public float thrustSpeed = 1.0f;
    public bool thrusting { get; private set; }

    public float turnDirection { get; private set; } = 0.0f;
    //public float rotationSpeed = 0.1f;

    public float respawnDelay = 3.0f;
    public float respawnInvulnerability = 3.0f;

    [SerializeField]
    float maxDistance = 10;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //StartCoroutine(AutoFire());
    }

    private void OnEnable()
    {
        // Turn off collisions for a few seconds after spawning to ensure the
        // player has enough time to safely move away from asteroids
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Invoke(nameof(TurnOnCollisions), GameManager.invincibilityTime);

        StartCoroutine(AutoFire());
    }

    bool playerAlreadyRotate = false;

    private void Update()
    {
        //this.thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        /*
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            this.turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            this.turnDirection = -1.0f;
        } else {
            this.turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
        */

        GameObject closestAsteroid = findClosestAsteroid();

        if (closestAsteroid != null)
        {
            Debug.Log(closestAsteroid.name, closestAsteroid);

            //Vector3 rotation = new Vector3(0, 0, Vector3.Angle(transform.position, closestAsteroid.transform.position));

            //transform.rotation = Quaternion.Euler(rotation);

            Vector3 direction = closestAsteroid.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            //transform.eulerAngles = Vector3.forward * angle;


                //StartCoroutine(RotatePlayer(angle));

            

            /*if (transform.eulerAngles.z < angle - 1 || transform.eulerAngles.z > angle + 1)
            {
                transform.Rotate(Vector3.forward * angle, angle * Time.deltaTime * GameManager.autorotationSpedd);
            }*/


        }

        /*LayerMask mask = LayerMask.GetMask("Asteroid");

        if (!Physics2D.Raycast(transform.position, transform.up,maxDistance, mask))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime *GameManager.autorotationSpeed);

            //Debug.Log("haha");
        }*/

        transform.Rotate(Vector3.forward * Time.deltaTime * GameManager.autorotationSpeed);

        Debug.DrawRay(transform.position, transform.up * maxDistance, Color.green, 1);
    }

    /*
    IEnumerator RotatePlayer(float angle = 0)
    {
        playerAlreadyRotate = true;

        float oldRotation = transform.rotation.z;

        bool reverseRotation;

        if(angle < 0)
        {
            turnDirection = -1;

            reverseRotation = true;
        }

        else
        {
            turnDirection = 1;

            reverseRotation = false;
        }

        //rigidbody.AddTorque(turnDirection * 100);

        if (reverseRotation)
        {
            while (transform.rotation.eulerAngles.z > oldRotation - angle)
            {
                Vector3 rotation = transform.rotation.eulerAngles;

                rotation.z += turnDirection * Time.deltaTime * 250;

                transform.rotation = Quaternion.Euler(rotation);

                yield return new WaitForEndOfFrame();
            }

        }

        else
        {
            while (transform.rotation.eulerAngles.z < oldRotation + angle)
            {
                Vector3 rotation = transform.rotation.eulerAngles;

                rotation.z += turnDirection * Time.deltaTime * 250;

                transform.rotation = Quaternion.Euler(rotation);

                yield return new WaitForEndOfFrame();
            }
        }

        yield return new WaitForEndOfFrame();

        playerAlreadyRotate = false;
    }
    */
    private void FixedUpdate()
    {
        /*
        if (this.thrusting) {
            this.rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (this.turnDirection != 0.0f) {
            this.rigidbody.AddTorque(this.rotationSpeed * this.turnDirection);
        }*/
    }

    

    public void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    public void TapShoot()
    {
        for (int i = 0; i < GameManager.generatedBullet; i++)
            {
            Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
        }
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(3 / GameManager.fireRate);

            Shoot();
        }
    }

    private void TurnOnCollisions()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDeath(this);
        }
    }


    



    private GameObject findClosestAsteroid()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject closestAsteroid = null;
        float closestDistance = maxDistance;
        bool first = true;

        foreach (var obj in objs)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (first)
            {
                closestDistance = distance;

                first = false;
            }
            else if (distance < closestDistance)
            {
                closestAsteroid = obj;
                closestDistance = distance;
            }

        }

        return closestAsteroid;
    }



}
