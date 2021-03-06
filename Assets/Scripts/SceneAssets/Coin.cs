﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpawnManager m_spawnManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PlayerAttributes.Tag)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                player.UpdateScore();
            }

            Destroy(this.gameObject);
            m_spawnManager.SpawnCoin();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_spawnManager = GameObject.Find(SceneManagers.SpawnManager).GetComponent<SpawnManager>();
        if (!m_spawnManager)
        {
            Debug.LogError(ErrorMessages.SpawnMgrNotFound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
