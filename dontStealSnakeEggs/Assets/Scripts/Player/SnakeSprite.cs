using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_snakeHead;
    [SerializeField] private SnakeControls m_snakeHeadControls;

    private Sprite[] spriteArray;
    private Color m_snakeColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        spriteArray = Resources.LoadAll<Sprite>("Sprites/SnakeGame");
        ColorUtility.TryParseHtmlString("#F1BF4E", out m_snakeColor); // snake orange color
        m_snakeHeadControls.OnSnakeDirectionChange += SnakeDirectionChange;
        m_snakeHeadControls.OnSnakeMoved += SnakeMoved;
    }


    private void SnakeDirectionChange(Vector2 newDir)
    {
        // Snake head sprites
        if (newDir == Vector2.up)
        {
            m_snakeHead.sprite = spriteArray[1];
        }
        else if (newDir == Vector2.down)
        {
            m_snakeHead.sprite = spriteArray[3];
        }
        else if (newDir == Vector2.left)
        {
            m_snakeHead.sprite = spriteArray[0];
        }
        else if (newDir == Vector2.right)
        {
            m_snakeHead.sprite = spriteArray[2];
        }
    }

    private void SnakeMoved()
    {
        //print(m_snakeHeadControls.SnakeBodies.Count);

        if (m_snakeHeadControls.SnakeBodies.Count >= 1)
        {
            if (m_snakeHeadControls.SnakeBodies.Count > 1)
            {
                // fetch the body after the head and alter it
                SpriteRenderer lbRenderer = m_snakeHeadControls.LastBody.gameObject.GetComponent<SpriteRenderer>();
                //Debug.Log(m_snakeHeadControls.LastBody.gameObject.name + " " + m_snakeHeadControls.PreviousDirection + " " + m_snakeHeadControls.DirectionHeadIsFacing);

                // check for orientation
                if (m_snakeHeadControls.PreviousDirection == m_snakeHeadControls.DirectionHeadIsFacing)
                {
                    // ==
                    if (m_snakeHeadControls.PreviousDirection == Vector2.left || m_snakeHeadControls.PreviousDirection == Vector2.right)
                    {
                        lbRenderer.sprite = spriteArray[8];
                    }
                    // ||
                    // ||
                    else if (m_snakeHeadControls.PreviousDirection == Vector2.up || m_snakeHeadControls.PreviousDirection == Vector2.down)
                    {
                        lbRenderer.sprite = spriteArray[9];
                    }
                }
                else
                {
                    // \\==
                    if ((m_snakeHeadControls.PreviousDirection == Vector2.left && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.up) ||
                        (m_snakeHeadControls.PreviousDirection == Vector2.down && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.right))
                    {
                        lbRenderer.sprite = spriteArray[12];
                    }
                    // ==//
                    else if ((m_snakeHeadControls.PreviousDirection == Vector2.right && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.up) ||
                        (m_snakeHeadControls.PreviousDirection == Vector2.down && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.left))
                    {
                        lbRenderer.sprite = spriteArray[11];
                    }
                    // //==
                    else if ((m_snakeHeadControls.PreviousDirection == Vector2.up && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.right) ||
                        (m_snakeHeadControls.PreviousDirection == Vector2.left && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.down))
                    {
                        lbRenderer.sprite = spriteArray[13];
                    }
                    // ==\\
                    else if ((m_snakeHeadControls.PreviousDirection == Vector2.up && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.left) ||
                        (m_snakeHeadControls.PreviousDirection == Vector2.right && m_snakeHeadControls.DirectionHeadIsFacing == Vector2.down))
                    {
                        lbRenderer.sprite = spriteArray[10];
                    }
                }
            }

            // Last body becomes a tail
            // we may have to store directions within the body parts or some garbo
        }

        //Debug.Log("BLAH" + newDir);
    }
}
