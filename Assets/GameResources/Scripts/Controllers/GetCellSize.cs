using UnityEngine;

/// <summary>
/// Получение нового размера поля
/// </summary>
public class GetCellSize : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    public float GewtNewCellSize(float spacing, int fieldSize)
    {
        float width = _mainCamera.pixelWidth;
        width -= spacing * (fieldSize + 1);
        return (width / fieldSize);
    }
}
