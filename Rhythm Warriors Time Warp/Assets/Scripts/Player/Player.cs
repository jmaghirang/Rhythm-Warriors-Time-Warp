using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthBar;
	public string playerName = "";

	public bool isSpeaking = true;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		playerName = PlayerPrefs.GetString("PlayerName", "Player");
    }

    // Update is called once per frame
    void Update()
    {
		/*if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(20);
		}*/
    }

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
}