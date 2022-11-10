using UnityEngine;

/// <summary>
/// SO �������� ����� ������ ��� ������
/// </summary>
[CreateAssetMenu(fileName = "NewColorManager", menuName = "2048/ColorManager", order = 51)]
public class ColorManager : ScriptableObject
{
    /// <summary>
    /// ������ ������ ������ 
    /// </summary>
    public Color[] CellColors;

    public Color PointsDarkColor;
    public Color PointsLightColor;
}
