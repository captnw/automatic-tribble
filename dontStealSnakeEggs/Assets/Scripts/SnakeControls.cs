using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SnakeControls : MonoBehaviour
{
    // Current Movement Direction
    private Vector2 m_dir = Vector2.right;
    private Vector2 m_dirHeadIsFacing = Vector2.right;

    // keeps track of the bodies
    private Queue<Transform> m_body = new Queue<Transform>();
    private int m_numberOfBodiesLeft = 100; // used for sorting order

    // did snake eat something
    private bool m_hasEaten = false;

    // Tail Prefab
    [SerializeField] private GameObject m_tailPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        // make sure head is always displayed over everything else
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = m_numberOfBodiesLeft + 1;

        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Prevent reversing with the second condition
        if (h == 1 && m_dirHeadIsFacing != Vector2.left)
            m_dir = m_dirHeadIsFacing = Vector2.right;
        else if (h == -1 && m_dirHeadIsFacing != Vector2.right)
            m_dir = m_dirHeadIsFacing = Vector2.left;
        else if (v == 1 && m_dirHeadIsFacing != Vector2.down)
            m_dir = m_dirHeadIsFacing = Vector2.up;
        else if (v == -1 && m_dirHeadIsFacing != Vector2.up)
            m_dir = m_dirHeadIsFacing = Vector2.down;
        else
        {
            // Do not move, if not actively holding down input
            m_dir = Vector2.zero;
        }
    }

    private void Move()
    {
        // only run the function if we're moving
        if (m_dir != Vector2.zero)
        {
            // Save current position (gap will be here)
            Vector2 lastHeadPosition = transform.position;

            // Move head into new direction (now there is gap)
            transform.Translate(m_dir);

            // Ate something? Insert new element into gap
            if (m_hasEaten)
            {
                // Load Prefab into the world
                GameObject g = Instantiate(m_tailPrefab, lastHeadPosition, Quaternion.identity);

                g.name = "SnakeBody" + m_body.Count.ToString();
                
                g.transform.SetParent(transform.parent);

                // Make sure that the snake bodies are displayed by their proximity to the snake head
                SpriteRenderer gSpriteRenderer = g.GetComponent<SpriteRenderer>();
                gSpriteRenderer.sortingOrder = m_numberOfBodiesLeft - m_body.Count;

                // Keep track of it in our tail list
                //tail.Insert(0, g.transform);
                m_body.Enqueue(g.transform);

                // Reset flag
                m_hasEaten = false;
            }
            // Do we have tail
            else if (m_body.Count > 0)
            {
                // Move last Tail Element to where Head was
                m_body.Peek().position = lastHeadPosition;

                // Dequeue the item, and then enqueue the same item
                // imagine the queue starting from right behind the head to the end
                // of its tail
                m_body.Enqueue(m_body.Dequeue());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Is the collided object a food?
        if (collision.name.StartsWith("FoodPrefab"))
        {
            // Get longer in next Move call
            m_hasEaten = true;

            // Remove the Food
            Destroy(collision.gameObject);
        }
        else
        {
            // ToDo 'You lose'...

            // not sure how we would handle this, because ideally I'd like to
            // prevent the snake from moving into a wall (and avoid of Undo-ing)
            // the snake entirely
        }
    }
}
