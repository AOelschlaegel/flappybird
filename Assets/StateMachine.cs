using UnityEngine;
using FlappyBird;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
	public string State;
	private int _clicks;

	[SerializeField] private GameObject _tubeSpawner;
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _bestScore;
	[SerializeField] private GameObject _tapInstructions;
	[SerializeField] private GameObject _gameOverOverlay;

	[SerializeField] private GameObject _medal;

	[SerializeField] private PlayerController _playerController;



	private Color _bronze;
	private Color _silver;
	private Color _gold;

	private void Awake()
	{
		_clicks = 0;

		_bronze = new Color(1, 0.6f, 0.18f, 1);
		_silver = new Color(0.91f, 0.97f, 1, 1);
		_gold = new Color(1, 0.9f, 0.25f, 1);
	}

	private void Start()
	{
		State = "IntroMode";
	}

	private void Update()
	{
		if(State == "IntroMode")
		{
			if(Input.GetMouseButtonDown(0))
			{
				_clicks++;
				_tapInstructions.SetActive(false);
			}

			if(_clicks == 1)
			{
				State = "GameMode";
			}
		}

		_bestScore.text = PlayerPrefs.GetInt("Score").ToString();



		_scoreText.gameObject.SetActive(false);

		switch (State)
		{
			case "GameMode":
				_tubeSpawner.SetActive(true);
				_scoreText.gameObject.SetActive(true);
				_playerController.GetComponent<Rigidbody2D>().simulated = true;
				_clicks = 0;

				break;

			case "IntroMode":
				_tubeSpawner.SetActive(false);
				_scoreText.text = null;
				_tapInstructions.SetActive(true);
				_playerController.GetComponent<Rigidbody2D>().simulated = false;

				break;

			case "GameOver":
				_gameOverOverlay.SetActive(true);

				

				if(_playerController.Score >= 0)
				{
					_medal.GetComponent<SpriteRenderer>().material.color = _bronze;
				}

				if (_playerController.Score > 100)
				{
					_medal.GetComponent<SpriteRenderer>().material.color = _silver;
				}

				if (_playerController.Score > 500)
				{
					_medal.GetComponent<SpriteRenderer>().material.color = _gold;
				}

				if(PlayerPrefs.GetInt("Score") < _playerController.Score)
				{
					PlayerPrefs.SetInt("Score", _playerController.Score);
				}
				break;
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene("GameScene");
		State = "IntroMode";
	}
}
