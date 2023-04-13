using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CoinSpawn : MonoBehaviour
{
    public Rigidbody2D rb;
	//[SerializeField] GameObject target;
	[SerializeField] Ease easeType;
	[SerializeField] Vector3 targetPosition;
	[SerializeField][Range(0.5f, 0.9f)] float minAnimDuration;
	[SerializeField][Range(0.9f, 2f)] float maxAnimDuration;

	private void Awake()
    {
		//targetPosition = new Vector3(2,10,0) ;
	}

	private void Start()
    {
		Game.CoinAmount = PlayerPrefs.GetFloat("Coin");
		UpdateCoinUI();
    }

 
	public void UpdateCoinUI()
    {
		Game.Instance.CoinText.text =  PlayerPrefs.GetFloat("Coin").ToString();
	}






    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag.Equals("wall"))
		{//-----------------------------------
		 //hit wall
			float posX = transform.position.x;
			if (posX > 0)
			{
				//hit right wall
				rb.AddForce(Vector2.right * 100f);
			}
			else
			{
				//hit left wall
				rb.AddForce(Vector2.right * 100f);
			}

			rb.AddTorque(posX * 1.5f);
		}

		if (other.tag.Equals("ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 4);
            rb.AddTorque(-rb.angularVelocity * 4f);
        }

		if(other.tag.Equals("cannon"))
        {
			Game.CoinAmount = PlayerPrefs.GetFloat("Coin");
			Debug.Log(Game.CoinAmount);
			Game.CoinAmount =Game.CoinAmount + Game.CoinValue;
			Game.Instance.CoinPickAudio.Play();
			Debug.Log(Game.CoinAmount);
			Debug.Log(Game.CoinValue);
			PlayerPrefs.SetFloat("Coin", Game.CoinAmount);
			UpdateCoinUI();
			float duration = Random.Range(minAnimDuration, maxAnimDuration);
			gameObject.transform.DOMove(targetPosition, duration)
				.SetEase(easeType)
				.OnComplete(() => {
					//executes whenever coin reach target position
					Destroy(gameObject);

				});
			
		}

		
	}

	public void CoinDie()
    {
		Destroy(gameObject);
    }

}
