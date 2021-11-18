using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class Bomb : MonoBehaviour
{
    CircleCollider2D circleCollider;

    Disc disc;

    float radius = 0.0001f;

    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        disc = GetComponent<Disc>();
    }

    // Update is called once per frame
    void Update()
    {
        radius += speed * Time.deltaTime;

        circleCollider.radius = radius;
        disc.Radius = radius;

        if(radius > 12)
        {
            Destroy(gameObject);
        }
    }
}
