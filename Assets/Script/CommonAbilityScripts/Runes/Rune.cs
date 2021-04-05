using UnityEngine;

[CreateAssetMenu]
public class Rune : ScriptableObject
{
    public string runeName;
    public string description;
    public Sprite baseRuneSprite;
    public Sprite runeIconSprite;


    public IRuneScriptContainer _IruneContainer = null;
}
