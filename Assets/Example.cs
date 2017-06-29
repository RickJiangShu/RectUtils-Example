using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Example : MonoBehaviour {

    private const int Width = 5;
    private const int Height = 5;
    private static Vector2 Pixel = new Vector2(20f, 20f);


    public Canvas canvas;
    public GameObject linerPref;
    public GameObject cellPref;

    public Transform visibleContainer;
    public Transform invisibleContainer;
    public Transform cellContainer;


    private List<RectTransform> invisibleLiners = new List<RectTransform>();
    private List<RectTransform> visibleLiners = new List<RectTransform>();//当前显示

    //map
    private Rectangle mapRect;
    private int[,] map;

	// Use this for initialization
	void Start () {

        DrawMap();

        Refresh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DrawMap()
    {
        mapRect = new Rectangle(0, 0, Width, Height);
        map = new int[Height, Width];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                GameObject go = GameObject.Instantiate(cellPref);
                go.transform.SetParent(cellContainer.transform, false);
                Cell cell = go.AddComponent<Cell>();
                cell.x = x;
                cell.y = y;
                cell.OnClick += OnClickCell;
                RectTransform tr = go.transform as RectTransform;
                tr.anchoredPosition = new Vector2(x * Pixel.x, -y * Pixel.y);
            }
        }
    }

    private void OnClickCell(Cell cell)
    {
        if (map[cell.x, cell.y] == 0)
        {
            cell.image.color = Color.black;
            map[cell.x, cell.y] = 1;
        }
        else
        {
            cell.image.color = Color.white;
            map[cell.x, cell.y] = 0;
        }
        Refresh();
    }

    public void DrawRect(Rectangle rect)
    {
        print(rect);

        RectTransform liner;
        if (invisibleLiners.Count > 0)
        {
            liner = invisibleLiners[0];
            invisibleLiners.RemoveAt(0);
        }
        else
            liner = CreateLiner();

        liner.anchoredPosition = new Vector2(rect.x * Pixel.x, -rect.y * Pixel.y);
        liner.sizeDelta = new Vector2(rect.width * Pixel.x, rect.height * Pixel.y);
        liner.transform.SetParent(visibleContainer.transform, false);
        visibleLiners.Add(liner);
    }

    private void Clear()
    {
        foreach (RectTransform liner in visibleLiners)
        {
            liner.SetParent(invisibleContainer);
            invisibleLiners.Add(liner);
        }
        visibleLiners.Clear();
    }
    private RectTransform CreateLiner()
    {
        GameObject go = GameObject.Instantiate(linerPref);
        return go.transform as RectTransform;
    }

    /// <summary>
    ///  刷新
    /// </summary>
    private void Refresh()
    {
        Clear();

        List<Rectangle> rects = RectUtils.Split(CheckIsBlock, mapRect);
        foreach (Rectangle rect in rects)
            DrawRect(rect);
    }

    private bool CheckIsBlock(int x, int y)
    {
        return map[x, y] == 1;
    }


}
