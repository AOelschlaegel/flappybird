using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird;

public class TubeController : MonoBehaviour
{
	private PlayerController _playerController;
	private TubeSpawner _tubeSpawner;

	private void Start()
	{
		_playerController = FindObjectOfType<PlayerController>();
		_tubeSpawner = FindObjectOfType<TubeSpawner>();
	}

	void Update()
    {
		if (!_playerController.GameOver)
		{
			transform.Translate(new Vector2(-0.01f*_tubeSpawner.Speed, 0));
		}
		else CancelInvoke();

		if(transform.position.x < -1.5f)
		{
			_tubeSpawner.Tubes.Remove(this.gameObject);

			if(_tubeSpawner.Tubes.Count < 3)
			{
				_tubeSpawner.SpawnTube();
			}
			Destroy(this.gameObject);
		}
    }

}
