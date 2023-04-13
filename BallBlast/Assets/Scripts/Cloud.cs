using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [System.Serializable]
    class Clouds
    {
        public MeshRenderer meshRenderer = null;
        public float speed = 0f;
        [HideInInspector] public Vector2 offset;
        [HideInInspector] public Material mat;
    }

    [SerializeField] Clouds[] allClouds;
    int count;

	void Start()
	{
		count = allClouds.Length;
		for (int i = 0; i < count; i++)
		{
			allClouds[i].offset = Vector2.zero;
			allClouds[i].mat = allClouds[i].meshRenderer.material;
		}
	}

	void Update()
	{
		for (int i = 0; i < count; i++)
		{
			allClouds[i].offset.x += allClouds[i].speed * Time.deltaTime;
			allClouds[i].mat.mainTextureOffset = allClouds[i].offset;
		}
	}
}
