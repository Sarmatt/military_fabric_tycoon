using UnityEngine;

public class PlacingObject : MonoBehaviour
{
    public int PrefId;
    public Vector2Int Size = Vector2Int.one;
    public bool Placed = false;
    public Equipment Equipment;
    [SerializeField] private BuildingObjectData _objectData;
    public int PlaceX;
    public int PlaceZ;
    [HideInInspector] public int RotY;
    public PointerClickHandler PointerClickHandler;

    [HideInInspector] public Vector3 StartCoords;
    [HideInInspector] public Quaternion StartRotation;
    [HideInInspector] public bool ChangingPlace = false;

    private void Start()
    {
        if (_objectData == null)
            _objectData = transform.GetChild(0).GetComponent<BuildingObjectData>();
    }

    private void OnDrawGizmos()
    {
        for(int x = 0; x < Size.x; x++)
        {
            for(int z = 0; z < Size.y; z++)
            {
                if((x+z)%2 == 0) Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
                else Gizmos.color = new Color(0f, 5f, 5f, 0.4f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1f, 0.1f, 1f));
            }
        }
    }

    private void SetMaterial(Color color)
    {
        foreach (var child in _objectData.Renderers)
            child.material.color = color;
    }

    public void SetTransparent(bool avaliabe)
    {
        if (!avaliabe) SetMaterial(Color.red);
        else SetMaterial(Color.green);
    }

    public void PlaceObject()
    {
        ChangingPlace = false;
        StartRotation = transform.rotation;
        StartCoords = transform.position;
        SetMaterial(Color.white);
    }

    public void ResetCoords()
    {
        ChangingPlace = false;
        transform.position = StartCoords;
        transform.rotation = StartRotation;
        SetMaterial(Color.white);
    }
}
