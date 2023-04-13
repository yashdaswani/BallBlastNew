using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Cannon : MonoBehaviour 
{
	Camera cam;
	Rigidbody2D rb;

	[SerializeField] HingeJoint2D[] wheels;
	JointMotor2D motor;

	[SerializeField] float CannonSpeed;
	bool isMoving = false;

	Vector2 pos;
	float screenBounds;
	float velocityX;

	public GameObject TouchToStartPanel;
	public TMPro.TMP_Text TouchToStartText;
	public float durationOfMovingText = 2;
	public static Cannon instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
	{
		Time.timeScale = 0;
		cam = Camera.main;

		rb = GetComponent<Rigidbody2D>();
		pos = rb.position;

		motor = wheels[0].motor;

		screenBounds = Game.Instance.screenWidth - 0.56f;



		TouchToStartText.transform.DOMove(new Vector3(10, 0, 0), durationOfMovingText).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);

	}

	void Update()
	{
		if(!Game.Instance.isGameover && (Input.touchCount>0 || Input.GetMouseButton(0) ))
        {
			Time.timeScale = 1;
			TouchToStartPanel.SetActive(false);
        }


		//Check player input ( hand or mouse drag)
		isMoving = Input.GetMouseButton(0);

		if (isMoving)
		{
			pos.x = cam.ScreenToWorldPoint(Input.mousePosition).x;
			
		}
	}

	void FixedUpdate()
	{
		//Move the cannon
		if (isMoving)
		{
			rb.MovePosition(Vector2.Lerp(rb.position, pos, CannonSpeed * Time.fixedDeltaTime));
			velocityX = 2.5f;
		}
		else
		{
			rb.velocity = Vector2.zero;
			velocityX = 0f;
		}

		//Rotate wheels
		//velocityX = rb.GetPointVelocity(rb.position).x;
		
		
		if (Mathf.Abs(velocityX) > 0.0f && Mathf.Abs(rb.position.x) < screenBounds)
		{
			motor.motorSpeed = velocityX * 150f;
			MotorActivate(true);
		}
		else
		{
			motor.motorSpeed = 0f;
			MotorActivate(false);
		}
	}

	void MotorActivate(bool isActive)
	{
		wheels[0].useMotor = isActive;
		wheels[1].useMotor = isActive;

		wheels[0].motor = motor;
		wheels[1].motor = motor;
	}

	public void ResetValues()
    {
		missile.Instance.speed = 20f;
		missile.Instance.delay = 0.3f;
    }


	public void ResetValuesInvoke()
    {
		Invoke("ResetValues", 3f);
    }

	//public void LeftpointerDown()
	//{
	//	leftpress = true;
	//}

	//public void LeftpointerUp()
	//{
	//	leftpress = false;
	//}


	//public void OnPointerDown(PointerEventData eventData)
	//{
	//	leftpress = true;
	//}

	//public void OnPointerUp(PointerEventData eventData)
	//{
	//	leftpress = false;
	//}


	//   private void OnTriggerEnter2D(Collider2D other)
	//   {
	//       if(other.tag.Equals("Invulnerable"))
	//       {
	//		transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

	//		if(other.tag.Equals("missile"))
	//           {
	//			Game.Instance.isGameover = false;
	//		}
	//       }
	//   }


	//   private void OnCollisionEnter2D(Collision2D collision)
	//   {
	//	if (collision.gameObject.tag == "Invulnerable")
	//	{
	//		gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
	//		Invoke("makeCannonTriggerFalse", 3f);
	//	}

	//}


	public void makeCannonVulnerable()
	{
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
		gameObject.GetComponent<BoxCollider2D>().enabled = true;

	}

	public void InvokeCannonVulnerable()
    {
		Invoke("makeCannonVulnerable", 5f);
    }

}
