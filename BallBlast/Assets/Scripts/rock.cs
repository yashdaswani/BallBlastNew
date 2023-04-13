using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rock : MonoBehaviour
{
	public static rock instance;

	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] public float health;

	[SerializeField] protected TMP_Text textHealth;
	[SerializeField] public float jumpForce;

	protected float[] leftAndRight = new float[2] { -1f, 1f };

	[HideInInspector] public bool isResultOfFission = true;

	protected bool isShowing;
	public ParticleSystem effectSmoke;
	public AudioSource rockblast;
	public GameObject CoinPrefab;


    private void Awake()
    {
        instance = this;
    }


    void Start()
	{
		UpdateHealthUI();

		isShowing = true;
		rb.gravityScale = 0f;

		if (isResultOfFission)
		{
			FallDown();
		}
		else
		{
			float direction = leftAndRight[Random.Range(0, 2)];
			float screenOffset = Game.Instance.screenWidth * 1.3f;
			transform.position = new Vector2(screenOffset * direction, transform.position.y);

			rb.velocity = new Vector2(-direction, 0f);
			//push meteor down after few seconds
			Invoke("FallDown", Random.Range(screenOffset - 2.5f, screenOffset - 1f));
		}

	}

	void FallDown()
	{
		isShowing = false;
		if(Game.changeGravitytoZero==false)
        {
			rb.gravityScale = 1f;
        }
		rb.AddTorque(Random.Range(-20f, 20f));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("cannon"))
		{//--------------------------------
		 //gameover
			
			if(gameObject.tag.Equals("meteor"))

            {
				Game.Instance.isGameover = true;
				Game.changeGravitytoZero = false;
				Game.Instance.GameOverSound.Play();
			}

            else
            {
				if (gameObject.tag.Equals("IBS"))
				{
					DiePrize();
					missile.Instance.speed = 50f;
					missile.Instance.delay = 0.2f;
					Cannon.instance.ResetValuesInvoke();
				}
				if (gameObject.tag.Equals("ZG"))
				{
					Game.changeGravitytoZero = true;
					if (Game.changeGravitytoZero == true)
					{
						Physics.gravity = Vector3.zero;
						Invoke("ChangeGravityToDefault", 1f);
					}

					DiePrize();
				}
				if (gameObject.tag.Equals("Invulnerable"))
				{
					other.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
					other.GetComponent<BoxCollider2D>().enabled = false;
					Cannon.instance.InvokeCannonVulnerable();
					DiePrize();
				}
			}

		}

		if (other.tag.Equals("missile"))
		{//--------------------------------
		 //takedamage
		 if(gameObject.tag.Equals("IBS") || gameObject.tag.Equals("ZG") || gameObject.tag.Equals("Invulnerable"))
            {
				return;
            }

		 else
            {
			TakeDamage(Game.MissileDamage);
			Game.Instance.score = Game.Instance.score+ 1;
			PlayerPrefs.SetFloat("score", Game.Instance.score);
			//destroy missile
			missile.Instance.DestroyMissile(other.gameObject);
            }

		}

		if (!isShowing && other.tag.Equals("wall"))
		{//-----------------------------------
		 //hit wall
			float posX = transform.position.x;
			if (posX > 0)
			{
				//hit right wall
				rb.AddForce(Vector2.left * 150f);
			}
			else
			{
				//hit left wall
				rb.AddForce(Vector2.right * 150f);
			}

			rb.AddTorque(posX * 4f);
		}

		if (other.tag.Equals("ground"))
		{//----------------------------------

			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			rb.AddTorque(-rb.angularVelocity * 4f);
		}
	}

	public void TakeDamage(float damage)
	{
		
		if (health  > 0)
		{
			health -= damage;
			UpdateHealthUI();
		}
		if(health <=0)
		{
			gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
			rockblast.Play();
			SpawnCoin();
			Die();

		}
		
	}

	public void SpawnCoin()
    {
		GameObject coing;
		coing = Instantiate(CoinPrefab, transform.position, Quaternion.identity, RockSpawner.Instance.transform);
		coing.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAndRight[0], 5f);
	}

	virtual public void Die()
	{
		
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = false;
		Destroy(gameObject,2f);
	}



	void DiePrize()
    {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;  
		Destroy(gameObject,2f);
    }

	protected void UpdateHealthUI()
	{
		textHealth.text = health.ToString();
	}

	public void ChangeGravityToDefault()
    {
		Game.changeGravitytoZero = false;
		Physics.gravity = new Vector3(0, 10, 0);
    }

	

	
}
