using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Игровой контроллер
/// </summary>
public class GameController : MonoBehaviour
{
    public int Points { get; private set; } //
    public bool IsGameStarted { get; private set; }

    [SerializeField]
    private Field _field;
    [SerializeField]
    private Text _gameResult;
    [SerializeField]
    private Text _pointsText;

    public void Win()
    {
        IsGameStarted = false;
        _gameResult.text = "You win";
    }

    public void Lose()
    {
        //TODO
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _gameResult.text = string.Empty;

        SetPoints(0);
        IsGameStarted = true;

        _field.GenerateField();
    }

    public void AddPoints(int points) => SetPoints(Points + points); //

    private void SetPoints(int points)
    {
        Points = points;
        _pointsText.text = Points.ToString();
    }
}
