using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    private Rigidbody2D m_rigidBody = null;
    private UIManager m_uiManager = null;

    private float m_velocity = 50f;
    private float m_yBoundary = -4.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Destroy player. Game over
            Destroy(other.gameObject);
            m_uiManager.GameOver();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        if (!m_rigidBody)
        {
            Debug.LogError("Rigidbody not found");
        }

        m_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (!m_uiManager)
        {
            Debug.LogError("UI Manager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidBody.velocity = Vector2.down * m_velocity * Time.deltaTime;
        if (transform.position.y < m_yBoundary)
        {
            Destroy(this.gameObject);
        }
    }
}
