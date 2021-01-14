using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed; // enemy bullet speed
    Vector2 _direction; // the direction of the bullet
    bool isReady; // to know when the bullet direction is set

    void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // function to set the bullet’s direction
    public void SetDirection(Vector2 direction)
    {
        // set the direction normalized, to get an unit vector
        _direction = direction.normalized;
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            // get the bullet’s current position
            Vector2 position = transform.position;

            // compute the bullet’s new position
            position += _direction * speed * Time.deltaTime;

            // Update the bullet’s position
            transform.position = position;

            // Next we need to remove bullet from our game
            // if the bullet goes outside the screen

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if ((transform.position.x < min.x || transform.position.x > max.x) ||
                (transform.position.y < min.y || transform.position.y > max.y)) 
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "AsteroidTag"))
        {
            Destroy(gameObject);
        }
    }
}
