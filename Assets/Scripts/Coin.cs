using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int m_coinValue = 1;

    [SerializeField]
    private SpawnManager m_spawnManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                player.UpdateScore(m_coinValue);
            }

            Destroy(this.gameObject);
            m_spawnManager.SpawnCoin();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (!m_spawnManager)
        {
            Debug.LogError("Spawn Manager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
