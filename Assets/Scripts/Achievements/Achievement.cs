using UnityEngine;

[CreateAssetMenu(menuName = "Achievement/Create new")]
public class Achievement : ScriptableObject
{
    public string Title;//Hello Hello 2
    [TextArea]
    public string Text;
    public Sprite Image;
}