using UnityEngine;

public enum TempCardEnum
{
    normal1,
    normal2,
    normal3
}


[CreateAssetMenu(fileName = "TempCardSO", menuName = "Scriptable Objects/TempCardSO")]
public class TempCardSO : ScriptableObject
{
    public int cost;
    public string name, description;
    public TempCardEnum typeEnum;

}
