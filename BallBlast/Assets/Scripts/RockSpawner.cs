using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] meteorPrefabs;
	[SerializeField] int meteorsCount;
	[SerializeField] float spawnDelay;

	Queue<GameObject> meteors;
	GameObject Go;
	float t = 4f;

	#region Singleton class: MeteorSpawner

	public static RockSpawner Instance;

	void Awake()
	{
		Instance = this;
	}

	#endregion

	void Start()
	{
		PrepareMeteors();
		
	}

    private void Update()
    {
		t += Time.deltaTime;
		if (t >= spawnDelay)
		{
			t = 0f;
			Go = SpawnMeteors();
		}

		if (meteors.Count == 2)
		{
			PrepareMeteors();
			meteorsCount = meteorsCount+5;

		}
	}


    public GameObject SpawnMeteors()
	{
		if (meteors.Count > 0)
		{
			Go = meteors.Dequeue();
			Go.SetActive(true);
			return Go;
		}

		

		return null;
	}

	//IEnumerator SpawnMeteors()
	//{
	//	if (meteors.Count > 0)
	//	{
	//		Go = meteors.Dequeue();
	//		Go.SetActive(true);
			
	//	}
	//	yield return new   WaitForSeconds(spawnDelay);
		
	//}

	void PrepareMeteors()
	{
		meteors = new Queue<GameObject>();
		int prefabsCount = meteorPrefabs.Length;
		for (int i = 0; i < meteorsCount; i++)
		{
			Go = Instantiate(meteorPrefabs[Random.Range(0, prefabsCount)], transform);
			Go.GetComponent<rock>().isResultOfFission = false;
			float scoreTest = Game.Instance.score;

            if ( 2 < scoreTest && scoreTest < 10)
			{
				Go.GetComponent<rock>().health = Go.GetComponent<rock>().health + 5;
			}
			if(5 < scoreTest && scoreTest < 10)
			{
				Go.GetComponent<rock>().health = Go.GetComponent<rock>().health + 10;
			}
			if(5 < scoreTest && scoreTest < 10)
			{
				Go.GetComponent<rock>().health = Go.GetComponent<rock>().health + 20;
			}
			Go.SetActive(false);
			meteors.Enqueue(Go);
			
		}
		
	}
}
