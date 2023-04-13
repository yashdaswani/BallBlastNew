using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPrize : rock
{

   [SerializeField] GameObject[] splitsPrefabs;




	override public void Die()
	{
		SplitMeteor();

		Destroy(gameObject);
	}

	void SplitMeteor()
	{
		GameObject g;
		int n = splitsPrefabs.Length;
		for (int i = 0; i < 1; i++)
		{
			g = Instantiate(splitsPrefabs[Random.Range(0,n)], transform.position, Quaternion.identity, RockSpawner.Instance.transform);
			g.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAndRight[i], 5f);
		}
	}
}
