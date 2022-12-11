using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System.Linq;
using TMPro;

public class Graph : MonoBehaviour
{
    [SerializeField] private List<int> _plusList = new List<int>();
    [SerializeField] private List<int> _minusList = new List<int>();
    [SerializeField] private RectTransform _graphContainer;
    [SerializeField] private Sprite _circleSprite;
    [SerializeField] private Color _plusColor;
    [SerializeField] private Color _minusColor;

    [Header("Main graph settings")]
    [SerializeField] private List<TMP_Text> _vertivalPoints;
    [SerializeField] private List<TMP_Text> _horizontalPoints;
    [SerializeField] private float _xSize;
    [SerializeField] private float _lineThickness;
    [SerializeField] private int _maxY;
    [SerializeField] private float _scale;

    private void Start()
    {
        DisplayGraphs();
    }

    private GameObject GetCreatedCircle(Vector2 anchoredPos)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(_graphContainer, false);
        gameObject.GetComponent<Image>().sprite = _circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPos;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void CreateLine(Vector2 dotPosA, Vector2 dotPosB, Color lineColor)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(_graphContainer, false);
        gameObject.GetComponent<Image>().color = lineColor;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosB - dotPosA).normalized;
        float distance = Vector2.Distance(dotPosA, dotPosB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, _lineThickness);
        rectTransform.anchoredPosition = dotPosA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private void ClearGraph()
    {
        foreach (Transform child in _graphContainer.transform)
            Destroy(child.gameObject);
    }

    private int GetMaxVertexValue(List<int> posValues, List<int> negValues)
    {
        List<int> posList = posValues.OrderBy(x => x).ToList();
        List<int> negList = negValues.OrderBy(x => x).ToList();
        if (posList.LastOrDefault() > negList.LastOrDefault())
            return posList.LastOrDefault();
        return negList.LastOrDefault();
    }

    private void ShowGraph(List<int> values, int maxValue, Color lineColor)
    {
        float graphHeight = _graphContainer.sizeDelta.y;
        IEnumerable<int> maxYList = values;
        
        GameObject lastCircle = null;
        for (int i = 0; i < values.Count; i++)
        {
            float xPos = _xSize + i * _xSize;
            float yPos = ((values[i] * _scale / maxValue) * graphHeight);
            GameObject circle = GetCreatedCircle(new Vector2(xPos, yPos));
            if(lastCircle != null)
                CreateLine(lastCircle.GetComponent<RectTransform>().anchoredPosition, circle.GetComponent<RectTransform>().anchoredPosition, lineColor);
            lastCircle = circle;
        }
    }

    private string GetFormatedText(int gettedInt)
    {
        string res = "";
        if (gettedInt > 1000)
        {
            res += gettedInt / 1000;
            res += "k";
        }
        else if (gettedInt > 1000000)
        {
            res += gettedInt / 1000000;
               res += "m";
        }
        return res;
    }

    private void DisplayY(int maxYValue)
    {
        for(int i = 0; i < _vertivalPoints.Count; i++)
        {
            float res1 = (100 / (float)_vertivalPoints.Count * (i + 1)) / 100;
            int res = (int)(maxYValue * res1);
            _vertivalPoints[i].text = GetFormatedText(res);
        }
    }
          

    private void DisplayGraphs()
    {
        ClearGraph();
        int maxInt = GetMaxVertexValue(_plusList, _minusList);
        DisplayY(maxInt);
        ShowGraph(_plusList, maxInt, _plusColor);
        ShowGraph(_minusList, maxInt, _minusColor);
    }
}
