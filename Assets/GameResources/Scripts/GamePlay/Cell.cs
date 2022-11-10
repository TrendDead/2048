using UnityEngine;
using UnityEngine.UI;

//TODO: Стоит не забыть перевести в privat если не понаобяться свойства

/// <summary>
/// Компонент клетки поля
/// </summary>
public class Cell : MonoBehaviour
{

    /// <summary>
    /// Позиция клетки в матрице
    /// </summary>
    public Vector2 CellPosition { get; private set; }
    /// <summary>
    /// Значение клетки
    /// </summary>
    public int Value { get; private set; }
    /// <summary>
    /// Пустая ли плитка
    /// </summary>
    public bool IsEmpty => Value == 0;

    public bool HasMerged { get; private set; }

    [SerializeField]
    private Image _backCell;
    [SerializeField]
    private Text _textCell;
    [SerializeField]
    private ColorManager _colorManager;

    private GameController _gameController;
    private int _valueView => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);

    public void Init(GameController gameController)
    {
        _gameController = gameController;
    }

    /// <summary>
    /// Задать значение клетки
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(int value)
    {
        Value = value;
        UpdateCellView();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        FindObjectOfType<GameController>().AddPoints(_valueView);

        UpdateCellView();
    }

    public void ResetFlags()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell otherCell)
    {
        otherCell.IncreaseValue();
        SetValue(0);

        UpdateCellView();
    }

    public void MoveToCell(Cell target)
    {
        target.SetValue(Value);
        SetValue(0);

        UpdateCellView();
    }

    /// <summary>
    /// Задать позицию клетки
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPosition(int x, int y) => CellPosition = new Vector2(x, y);

    private void UpdateCellView()
    {
        _textCell.text = IsEmpty ? string.Empty : _valueView.ToString();
        _textCell.color = Value <= 2 ? _colorManager.PointsDarkColor : _colorManager.PointsLightColor;

        _backCell.color = _colorManager.CellColors[Value];
    }
}
