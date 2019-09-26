using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Item : MonoBehaviour
{
    public float velocity = 0.5f;
    public AudioClip takeUpClip;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody2D.velocity = Vector2.down * velocity;
        Debug.Log("life velocity" + _rigidbody2D.velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                TriggerPlayer(player);
            }
            
            AudioManager.Instance.PlayGameClip(takeUpClip);
            PoolManager.Instance.GiveBack(gameObject);
        }
    }

    protected abstract void TriggerPlayer(Player player);
}