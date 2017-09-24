using UnityEngine;

public enum PlayerClass
{
	Rock = 0,
	Paper = 1,
	Scissors = 2
}

public class Player {
	//private members
	private int _health;
	private float _moveSpeed;
	private PlayerClass _class;

	//public members
	Player()
	{
		_health = 100;
		_moveSpeed = 7.5f;
		_class = PlayerClass.Rock;
	}

	Player(int health, float speed, PlayerClass player_class)
	{
		_health = health;
		_moveSpeed = speed;
		_class = player_class;
	}

	Player(int health, float speed, string player_class)
	{
		_health = health;
		_moveSpeed = speed;

		PlayerClass theClass;

		switch (player_class) 
		{
		case "Rock":
		case "rock":
			theClass = PlayerClass.Rock;
			break;
		case "Paper":
		case "paper":
			theClass = PlayerClass.Paper;
			break;
		case "Scissors":
		case "scissors":
			theClass = PlayerClass.Scissors;
			break;
		default:
			Debug.Log ("Not a proper class! Defaulting to rock.");
			theClass = PlayerClass.Rock;
			break;
		}
		_class = theClass;
	}
}
