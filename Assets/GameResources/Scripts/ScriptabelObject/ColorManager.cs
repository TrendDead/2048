using UnityEngine;

/// <summary>
/// SO хранящий набор цветов для клеток
/// </summary>
[CreateAssetMenu(fileName = "NewColorManager", menuName = "2048/ColorManager", order = 51)]
public class ColorManager : ScriptableObject
{
    /// <summary>
    /// Массив цветов клеток 
    /// </summary>
    public Color[] CellColors;

    public Color PointsDarkColor;
    public Color PointsLightColor;
}
