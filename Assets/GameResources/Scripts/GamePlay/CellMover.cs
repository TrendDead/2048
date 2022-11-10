using UnityEngine;

/// <summary>
/// Передвижение клеток
/// </summary>
public class CellMover : MonoBehaviour
{
    [SerializeField]
    private int _maxValue = 11;

    private int _fieldSize;
    private Cell[,] _field;

    public void Move(Vector2 direction, int fieldSize, Cell[,] field)
    {
        _fieldSize = fieldSize;
        _field = field;
        int startXY = direction.x > 0 || direction.y < 0 ? _fieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = startXY; j >= 0 && j < _fieldSize; j -= dir)
            {
                var cell = direction.x != 0 ? _field[j, i] : _field[i, j];

                if (cell.IsEmpty)
                    continue;

                var cellToMerge = FindCellToMerge(cell, direction);
                if (cellToMerge != null)
                {
                    cell.MergeWithCell(cellToMerge);
                    //_anyCellMoved = true;

                    continue;
                }

                var emptyCell = FindEmtyCell(cell, direction);
                if (emptyCell != null)
                {
                    cell.MoveToCell(emptyCell);
                   // _anyCellMoved = true;
                }
            }
        }
    }

    private Cell FindCellToMerge(Cell cell, Vector2 direction)
    {
        int startX = (int)cell.CellPosition.x + (int)direction.x;
        int startY = (int)cell.CellPosition.y - (int)direction.y;

        for (int x = startX, y = startY;
            x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if (_field[x, y].IsEmpty)
                continue;

            if (_field[x, y].Value == cell.Value && !_field[x, y].HasMerged)
                return _field[x, y];

            break;
        }

        return null;
    }

    private Cell FindEmtyCell(Cell cell, Vector2 direction)
    {
        Cell emptyCell = null;

        int startX = (int)cell.CellPosition.x + (int)direction.x;
        int startY = (int)cell.CellPosition.y - (int)direction.y;

        for (int x = startX, y = startY;
            x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize;
            x += (int)direction.x, y -= (int)direction.y)
        {
            if (_field[x, y].IsEmpty)
                emptyCell = _field[x, y];
            else
                break;
        }

        return emptyCell;
    }


    public void CheckGameResult()
    {
        bool lose = true;

        //FIXME: Можно при объединении получать значение, а не проверять все плитки
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                if (_field[i, j].Value == _maxValue)
                {
                    FindObjectOfType<GameController>().Win(); // фу
                    return;
                }

                if (lose && _field[i, j].IsEmpty ||
                    FindCellToMerge(_field[i, j], Vector2.left) ||
                    FindCellToMerge(_field[i, j], Vector2.right) ||
                    FindCellToMerge(_field[i, j], Vector2.up) ||
                    FindCellToMerge(_field[i, j], Vector2.down))
                {
                    lose = false;
                }
            }

            //TODO: Доделать проигрыш
        }
    }
}
