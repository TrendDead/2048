using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Компонент игрового поля
/// </summary>
public class Field : MonoBehaviour
{
    [Header("Field Propertirs")]
    [SerializeField]
    private float _spacing;
    [SerializeField]
    private int _fieldSize;
    [SerializeField]
    private int _initCellsCount = 2;

    [Space(10)]
    [SerializeField]
    private Cell _cellPref;
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private SwipeDetection _swipeDetection;
    [SerializeField]
    private GetCellSize _getCellSize;
    [SerializeField]
    private CellMover _cellMover;
    [SerializeField]
    private GameController _gameController;

    private Cell[,] _field;
    private float _сellSize;
    //private bool _anyCellMoved;

    private void OnEnable()
    {
        _swipeDetection.OnSwipe += OnInput;
        _сellSize = _getCellSize.GewtNewCellSize(_spacing, _fieldSize);
    }

    private void OnDisable()
    {
        _swipeDetection.OnSwipe -= OnInput;
    }

    private void OnInput(Vector2 direction)
    {
        if (!_gameController.IsGameStarted)
            return;

        //_anyCellMoved = false;
        ResetCellsFlags();

        _cellMover.Move(ConvertVector(direction), _fieldSize, _field);

        //if (_anyCellMoved)
        if (true)
        {
            GenerateRandomCells();
            _cellMover.CheckGameResult();
        }
    }

    private Vector2 ConvertVector(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                return Vector2.right;
            else
                return Vector2.left;
        }
        else
        {
            if (direction.y > 0)
                return Vector2.up;
            else
                return Vector2.down;
        }
    }


    private void CreateField()
    {
        _field = new Cell[_fieldSize, _fieldSize];

        float fieldWith = _fieldSize * (_сellSize + _spacing) + _spacing;
        _rectTransform.sizeDelta = new Vector2(fieldWith, fieldWith);

        float startX = -(fieldWith / 2) + (_сellSize / 2) + _spacing;
        float startY = (fieldWith / 2) - (_сellSize / 2) - _spacing;

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                var cell = Instantiate(_cellPref, transform, false);
                var position = new Vector2(startX + (i * (_сellSize + _spacing)), startY - (j * (_сellSize + _spacing)));
                cell.transform.localPosition = position;
                cell.GetComponent<RectTransform>().sizeDelta = new Vector2(_сellSize, _сellSize);
                cell.Init(_gameController);

                _field[i, j] = cell;

                cell.SetPosition(i, j);
                cell.SetValue(0);
            }
        }
    }

    /// <summary>
    /// генерация игрового поля
    /// </summary>
    public void GenerateField()
    {
        if (_field == null)
            CreateField();

        ResetValue();

        for (int i = 0; i < _initCellsCount; i++)
            GenerateRandomCells();
    }

    private void ResetValue()
    {
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                _field[i, j].SetValue(0);
            }
        }
    }

    private void GenerateRandomCells()
    {
        var emptyCells = new List<Cell>();

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                if (_field[i, j].IsEmpty)
                    emptyCells.Add(_field[i, j]);
            }
        }

        if (emptyCells.Count == 0)
            throw new System.Exception("There is no any empty cell!");

        int value = Random.Range(0, 10) == 0 ? 2 : 1;

        var cell = emptyCells[Random.Range(0, emptyCells.Count)];
        cell.SetValue(value);
    }

    private void ResetCellsFlags()
    {
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                _field[i, j].ResetFlags();
            }
        }
    }
}
