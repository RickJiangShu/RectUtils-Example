/*
 * Author:  Rick
 * Create:  2017/6/29 16:15:17
 * Email:   rickjiangshu@gmail.com
 * Follow:  https://github.com/RickJiangShu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cell
/// </summary>
public class Cell : MonoBehaviour
{
    public System.Action<Cell> OnClick;
    public int x;
    public int y;
    public UnityEngine.UI.Image image;
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnClickThis);
    }

    public void OnClickThis()
    {
        OnClick(this);
    }

    
}
