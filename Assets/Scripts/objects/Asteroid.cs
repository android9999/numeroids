using UnityEngine;
using TMPro;
using Shapes;
using System.Collections.Generic;

//[RequireComponent(typeof(RegularPolygon))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public RegularPolygon regularPolygon { get; private set; }
    public Sprite[] sprites;

    public float size = 1.0f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50.0f;
    public float maxLifetime = 30.0f;
    Transform worldSpaceCanvas;
    
    public int health = 1;
    public int maxHealth;
    [SerializeField]
    TMP_Text number;

    public bool monoChromatic = true;

    public Color[] colors;


    private void Awake()
    {
        this.regularPolygon = GetComponent<RegularPolygon>();
        this.rigidbody = GetComponent<Rigidbody2D>();

        worldSpaceCanvas = transform.parent;
    }

    private void Start()
    {
        number.text = health.ToString();

        maxHealth = health;

        // Assign random properties to make each asteroid feel unique
        this.regularPolygon.Angle = Random.Range(0, 180);
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        if (monoChromatic)
        {
            regularPolygon.Color = Color.white;

            number.color = Color.white;
        }

        else
        {
            Debug.Log(colors.Length);
            regularPolygon.Color = colors[Random.Range(0, colors.Length)];
        }

        // Set the scale and mass of the asteroid based on the assigned size so
        // the physics is more realistic
        this.transform.localScale = Vector3.one * this.size;
        this.rigidbody.mass = this.size;



        // Destroy the asteroid after it reaches its max lifetime
        Destroy(this.gameObject, this.maxLifetime);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            bool hitted = false;

            LayerMask layerMask = LayerMask.GetMask("Asteroid");

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 100, layerMask);
            if (hit.collider != null && !hitted)
            {
                Debug.DrawRay(mousePos2D, Vector2.zero, Color.red, 5);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
                Debug.Log($"you tap {hit.collider.gameObject.name}");

                GetDamage(GameManager.tapDamage);

                hitted = true;
            }
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        // The asteroid only needs a force to be added once since they have no
        // drag to make them stop moving
        this.rigidbody.AddForce(direction * this.movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetDamage(GameManager.damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            DestroyAsteroid();
        }
    }

    void GetDamage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            DestroyAsteroid();
        }

        else
        {
            number.text = health.ToString();
        }
    }

    private Asteroid CreateSplit()
    {
        // Set the new asteroid poistion to be the same as the current asteroid
        // but with a slight offset so they do not spawn inside each other
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new asteroid at half the size of the current
        Asteroid half = Instantiate(this, position, this.transform.rotation, worldSpaceCanvas);
        half.size = this.size * 0.5f;

        // Set a random trajectory
        half.SetTrajectory(Random.insideUnitCircle.normalized);

        half.health = (int)(maxHealth / 2) +1;

        return half;
    }

    private void DestroyAsteroid()
    {
        // Check if the asteroid is large enough to split in half
        // (both parts must be greater than the minimum size)
        if ((this.size * 0.5f) >= this.minSize)
        {
            CreateSplit();
            CreateSplit();
        }

        FindObjectOfType<GameManager>().AsteroidDestroyed(this);

        // Destroy the current asteroid since it is either replaced by two
        // new asteroids or small enough to be destroyed by the bullet
        Destroy(this.gameObject);
    }

}

/*
using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }

}*/