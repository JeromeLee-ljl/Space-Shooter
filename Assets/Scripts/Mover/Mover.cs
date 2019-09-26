using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Mover : MyMono
{
    public float speed;
    public float hitDamage = 1f;
    public float maxHealth = 1f;
    public float currentHealth = 1f;

    public GameObject explosionPrefab;
    public float explosionDamage;
    public float explosionRadius;
    public AudioClip explosionClip;
    public Rigidbody2D _rigidbody2D { get; private set; }
    private float destroyDistance = 10f;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Reset();
    }

    public virtual void Reset()
    {
        currentHealth = maxHealth;
    }

    public virtual void ChangeHealth(float delta)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth + delta, 0, maxHealth);
        if (delta < 0 && currentHealth <= 0)
            DestroySelf();
    }

    protected virtual void DestroySelf()
    {
        if (!gameObject.activeSelf) return;
        Explosion();
        PoolManager.Instance.GiveBack(gameObject);
    }

    protected void Explosion()
    {
        if (explosionPrefab == null) return;
        AudioManager.Instance.PlayGameClip(explosionClip);
        GameObject explosion = PoolManager.Instance.GetInstance(explosionPrefab.name);
        explosion.transform.position = transform.position;
        explosion.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, 1);

        if (explosionDamage > 0 && explosionRadius > 0)
        {
            // 设置爆炸的layer
            int explosionLayer;
            if (LayerMask.LayerToName(gameObject.layer) == "Player")
                explosionLayer = LayerMask.GetMask("Enemy");
            else
                explosionLayer = LayerMask.GetMask("Player");

            var hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<Mover>()?.ChangeHealth(-explosionDamage);
            }
        }
    }

    protected virtual void Update()
    {
        // 超出范围自动回收
        if (transform.position.magnitude > destroyDistance)
            PoolManager.Instance.GiveBack(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log(name + " trigger:" + other.name);
        Mover mover = other.GetComponent<Mover>();
        if (mover != null)
            mover.ChangeHealth(-hitDamage);
    }
}