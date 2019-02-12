using UnityEngine;
using FlappyBird;
using System.Collections.Generic;

public class TubeSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _tube;
	public List<GameObject> Tubes;
	private PlayerController _playerController;

	[Range(0f, 5f)]
	public float Speed;
	[Range(0f, 1f)]
	public float SpeedIncreaseAmount;

	private float _distanceBetweenTubes = 2.5f;

	void Start()
	{
		Tubes = new List<GameObject>();
		_playerController = FindObjectOfType<PlayerController>();

		InvokeRepeating("SpeedIncrease", 0f, 5f);

		SpawnTube();
	}

	void Update()
	{
		if (_playerController.GameOver == true)
		{
			CancelInvoke();
		}
	}

	public void SpawnTube()
	{
		for (int i = 0; i < 3; i++)
		{
			var randPos = Random.Range(-0.3f, 1.1f);
			var pos = new Vector2(1.5f + i, randPos);
			var instance = Instantiate(_tube, pos, Quaternion.identity);
			Tubes.Add(instance);
			instance.transform.SetParent(transform);
		}
	}


	void SpeedIncrease()
	{
		Speed += SpeedIncreaseAmount;
	}
}
