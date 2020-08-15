using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private UIManager m_uiManager;

    // Player colliders
    private bool m_isGrounded;
    private bool m_rightCollided;
    private bool m_leftCollided;

    // Move properties
    private float m_boundary = 9f;
    private float m_moveInput;

    private int m_score;

    [SerializeField]
    private float m_speed;
    [SerializeField]
    private float m_jumpSpeed;
    [SerializeField]
    private float m_gravity;

    [SerializeField]
    private Transform m_groundCheck;
    [SerializeField]
    private Transform m_rightCheck;
    [SerializeField]
    private Transform m_leftCheck;
    [SerializeField]
    private Vector2 m_radius;
    [SerializeField]
    private LayerMask m_layerMask;

    [SerializeField]
    private float m_checkRadius;
    [SerializeField]
    private LayerMask m_whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        if (!m_rigidBody)
        {
            Debug.LogError(ErrorMessages.RigidBodyNotFound);
        }

        m_uiManager = GameObject.Find(SceneAssets.Canvas).GetComponent<UIManager>();
        if (!m_uiManager)
        {
            Debug.Log(ErrorMessages.UIMgrNotFound);
        }

        m_rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_moveInput = Input.GetAxis(PlayerAttributes.Horizontal) * m_speed;

        if (m_isGrounded && Input.GetButton(PlayerAttributes.Jump))
        {
            m_rigidBody.velocity = Vector2.up * m_jumpSpeed;
        }
        else if (m_isGrounded && m_rigidBody.velocity.y > 0)
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y * 0.5f);
        }
    }

    void FixedUpdate()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_checkRadius, m_whatIsGround);
        m_rightCollided = Physics2D.OverlapBox(m_rightCheck.position, m_radius, 0f, m_layerMask);
        m_leftCollided = Physics2D.OverlapBox(m_leftCheck.position, m_radius, 0f, m_layerMask);
        Move();
    }

    private void Move()
    {
        m_rigidBody.velocity = new Vector2(m_moveInput, m_rigidBody.velocity.y);

        // If the player is colliding with a wall, apply a downward force on the player.
        if (m_rightCollided || m_leftCollided)
        {
            m_rigidBody.AddForce(Vector2.down * m_jumpSpeed);
        }

        // If the player move outside of the boundary, position them on the opposite side.
        if (transform.position.x > m_boundary)
        {
            transform.position = new Vector2(-(m_boundary), transform.position.y);
        }
        else if (transform.position.x < -(m_boundary))
        {
            transform.position = new Vector2(m_boundary, transform.position.y);
        }
    }

    public void UpdateScore()
    {
        ++m_score;
        m_uiManager.UpdateScore(m_score);
    }
}
