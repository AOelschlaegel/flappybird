using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird {
	public class PlayerController : MonoBehaviour
	{
		Rigidbody2D rb;
		public bool GameOver;
		public bool IgnoreCollision;
		public int Score = 0;
		[SerializeField] private StateMachine _stateMachine;

		[SerializeField] private Text _scoreText;
		[SerializeField] private Text _gameOverScoreText;

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				rb.AddForce(new Vector2(0, 2f), ForceMode2D.Impulse);
			}

			rb.velocity = Vector3.ClampMagnitude(rb.velocity, 2.2f);

			if(rb.velocity.y <= 0)
			{
				Quaternion rot = Quaternion.Euler(0, 0, -30);
				transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.4f);
			}

			if (rb.velocity.y >= 0)
			{
				Quaternion rot = Quaternion.Euler(0, 0, 30);
				transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.4f);
			}

			if (_scoreText != null)
			{
				_scoreText.text = Score.ToString();
			}

			if (_gameOverScoreText != null)
			{
				_gameOverScoreText.text = Score.ToString();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Obstacle")
			{
				if(IgnoreCollision == false)
				{
					GameOver = true;
					_stateMachine.State = "GameOver";
					rb.simulated = false;
				}
			}

			if(collision.gameObject.tag == "Scorer")
			{
				Score++;
			}
		}
	}
}
