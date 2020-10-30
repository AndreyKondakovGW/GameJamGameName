using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    //TODO: THIS WHOLE SCRIPT IS A MESS
    float _HPtoScreenCoordsRatio = 2.0f;
    int _sides_length = 32;
    int _segment_length = 18;
    Transform _emptyBar;
    RectTransform _emptyBar_left;
    RectTransform _emptyBar_right;
    Transform _fullBar;
    RectTransform _fullBar_left;
    RectTransform _fullBar_right;
    RectTransform _mask;

    float _lastHP = 0f;
    float _lastMaxHP = 0f;


    int numberOfSegments = 1;

    private void Start()
    {
        _emptyBar = gameObject.transform.Find("EmptyBar");
        _emptyBar_left = _emptyBar.Find("Left").GetComponent<RectTransform>();
        _emptyBar_right = _emptyBar.Find("Right").GetComponent<RectTransform>();
        _mask = gameObject.transform.Find("Mask").GetComponent<RectTransform>();
        _fullBar = gameObject.transform.Find("Mask").Find("FullBar");
        _fullBar_left = _fullBar.Find("Left").GetComponent<RectTransform>(); ;
        _fullBar_right = _fullBar.Find("Right").GetComponent<RectTransform>(); ;
    }

    public void UpdateMaxHP(float maxHP, float HP)
    {
        if (maxHP != _lastMaxHP)
        {
            _lastMaxHP = maxHP;
            foreach (Transform child in _emptyBar)
            {
                if (child.name.Contains("Segment") && child.name != "Segment")
                {
                    Destroy(child.gameObject);
                    Debug.Log("DESTROYED " + maxHP);
                }
            }

            foreach (Transform child in _fullBar)
            {
                if (child.name.Contains("Segment") && child.name != "Segment")
                {
                    Destroy(child.gameObject);
                }
            }

            if (_sides_length * 2 > maxHP * _HPtoScreenCoordsRatio)
            {
                _fullBar.Find("Segment").gameObject.SetActive(false);
                _emptyBar.Find("Segment").gameObject.SetActive(false);

                float rightPos = (float)_sides_length * 1.5f;
                SetRight(rightPos);
            }
            else
            {
                float toFill = maxHP * _HPtoScreenCoordsRatio;
                float leftToFill = toFill - _sides_length * 2;
                float rightPos = toFill - (float)_sides_length / 2;
                SetRight(rightPos);
                GameObject fullSegment = _fullBar.Find("Segment").gameObject;
                fullSegment.SetActive(true);
                GameObject emptySegment = _emptyBar.Find("Segment").gameObject;
                emptySegment.SetActive(true);

                while (leftToFill > _segment_length)
                {
                    RectTransform rt = fullSegment.GetComponent<RectTransform>();
                    Vector3 pos = new Vector3(rt.anchoredPosition3D.x + _segment_length, rt.anchoredPosition3D.y, rt.anchoredPosition3D.z);
                    fullSegment = GameObject.Instantiate(fullSegment, fullSegment.transform.parent.transform);
                    emptySegment = GameObject.Instantiate(emptySegment, emptySegment.transform.parent.transform);
                    fullSegment.GetComponent<RectTransform>().anchoredPosition = pos;
                    emptySegment.GetComponent<RectTransform>().anchoredPosition = pos;
                    leftToFill = leftToFill - _segment_length;
                    Debug.Log("SPAWNED " + leftToFill + " " + maxHP);
                }
            }

            UpdateHP(HP);
        }
    }

    public void UpdateHP(float HP)
    {
        _mask.sizeDelta = new Vector2(HP*_HPtoScreenCoordsRatio, _mask.sizeDelta.y);
        _lastHP = HP;
    }

    void SetRight(float rightPos)
    {
        Vector3 pos = new Vector3(rightPos, _emptyBar_right.anchoredPosition3D.y, _emptyBar_right.anchoredPosition3D.z);
        _emptyBar_right.anchoredPosition3D = pos;
        _fullBar_right.anchoredPosition3D = pos;
    }
}
