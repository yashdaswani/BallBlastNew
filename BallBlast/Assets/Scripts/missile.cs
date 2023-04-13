using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    Queue<GameObject> missilesQueue;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] int missilesCount;

    [Space]
    [SerializeField] public float delay ;
    [SerializeField] public float speed ;

    GameObject g;
    float t = 0f;
    // Start is called before the first frame update
    #region Singleton class: Missiles

    public static missile Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion
    void Start()
    {
        PrepareMissiles();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= delay)
        {
            t = 0f;
            g = SpawnMissile(transform.position);
            if (g != null)
                g.GetComponent<Rigidbody2D>().velocity = Vector2.up * Game.MissileSpeed;
        }
    }

    void PrepareMissiles()
    {
        missilesQueue = new Queue<GameObject>();
        for (int i = 0; i < missilesCount; i++)
        {
            g = Instantiate(missilePrefab, transform.position, transform.rotation * Quaternion.Euler(0,0,-90), transform);
            g.SetActive(false);
            missilesQueue.Enqueue(g);
        }
    }

    public GameObject SpawnMissile(Vector2 position)
    {
        if (missilesQueue.Count > 0)
        {
            g = missilesQueue.Dequeue();
            g.transform.position = position;
            g.SetActive(true);
            return g;
        }

        return null;
    }
    public void DestroyMissile(GameObject missile)
    {
        missilesQueue.Enqueue(missile);
        missile.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("missile"))
        {
            DestroyMissile(other.gameObject);
        }
    }
}
