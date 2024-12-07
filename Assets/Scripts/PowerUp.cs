using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private static readonly int Animate = Animator.StringToHash("Animate");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Bomb.Range++;
            GetComponent<Animator>().SetTrigger(Animate);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
