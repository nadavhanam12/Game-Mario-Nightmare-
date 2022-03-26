using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag =="Player 2")
        {
            collision.gameObject.gameObject.SetActive(false);
            //SceneManager.LoadScene("GameOver");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<EnemyAI>();
            enemy.Crush();

        }
    }

}
