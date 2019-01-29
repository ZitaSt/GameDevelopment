using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleWarrior.Properties
{
    public class LW_Health : MonoBehaviour
    {
        [SerializeField]
        private const float maxHealth = 100.0f;
        public float currentHealth { get; private set; }
        
        // private GameObject _PlayerDead = null;

        /* void Start()
        {
            _PlayerDead = GameObject.Find("Name of the message box");
            if(_PlayerDead)
            {
                _PlayerDead.SetActive(false);
            }
        }*/
        
        void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (gameObject.tag == "Player")
                {
                    // Player is dead
                    // Show message
                    /*if (_PlayerDead)
                    {
                        _PlayerDead.SetActive(true);
                    }
                    // Give the player time to read the message
                    System.Threading.Thread.Sleep(3000);
                    */
                    // Player goes back to main menu automatically
                    Application.LoadLevel("MainMenu");
                }
            }
        }
    }
}
