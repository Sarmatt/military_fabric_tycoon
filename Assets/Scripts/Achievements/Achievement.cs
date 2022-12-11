using UnityEngine;

[CreateAssetMenu(menuName = "Achievement/Create new")]
public class Achievement : ScriptableObject
{
    public string Title;
    [TextArea]
    public string Text;
    public Sprite Image;
}
