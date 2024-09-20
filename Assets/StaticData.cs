using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StaticData : MonoBehaviour
{
    public static StaticData ins;
    void Awake()
    {
        ins = this;
    }
    public ObservedColorPalette ObservedActiveColorPalette;
    public ColorPalette ActiveColorPalette;
    [ReorderableList]
    public OffScreenLine[] OffScreenLines;
    public RectTransform[] OffScreenCorners;
    [SerializeField] LineRenderer OffScreenLineRenderer;
    public float SharedDelayDelta;
    public float CurrentDelayDelta;
    void Update()
    {
        if (ObservedActiveColorPalette.GetReference() != ActiveColorPalette)
        {
            ObservedActiveColorPalette.SetReference(ActiveColorPalette);
        }
        for (int i = 0; i < OffScreenCorners.Length; i++)
        {
            OffScreenLineRenderer.SetPosition(i, OffScreenCorners[i].position);
        }
    }
    public Vector3 OffScreenPoint(Vector3 elementPos, OffScreenSide offScreenSide)
    {
        Transform[] points = new Transform[2];
        float lerpParameter = 0;
        switch (offScreenSide)
        {
            case OffScreenSide.Left:
                //inv lerp on y
                points = OffScreenLines[(int)OffScreenSide.Left].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.y, points[1].position.y, elementPos.y);
                break;
            case OffScreenSide.Right:
                //inv lerp on y
                points = OffScreenLines[(int)OffScreenSide.Right].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.y, points[1].position.y, elementPos.y);
                break;
            case OffScreenSide.Top:
                //inv lerp on x
                points = OffScreenLines[(int)OffScreenSide.Top].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.x, points[1].position.x, elementPos.x);
                break;
            case OffScreenSide.Bottom:
                //inv lerp on x
                points = OffScreenLines[(int)OffScreenSide.Bottom].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.x, points[1].position.x, elementPos.x);
                break;
        }
        return Vector3.Lerp(points[0].position, points[1].position, lerpParameter);
    }
    public Vector3 OffScreenPointWithSizeShift(RectTransform elementRectTransform, OffScreenSide offScreenSide)
    {
        Vector3 elementPos = elementRectTransform.localPosition;
        Vector3 elementShift = Vector3.zero;
        switch (offScreenSide)
        {
            case OffScreenSide.Left:
                //only need to shift by width
                elementShift = Vector3.left;
                break;
            case OffScreenSide.Right:
                //only need to shift by width
                elementShift = Vector3.right;
                break;
            case OffScreenSide.Top:
                //only need to shift by height
                elementShift = Vector3.up;
                break;
            case OffScreenSide.Bottom:
                //only need to shift by height
                elementShift = Vector3.down;
                break;
            default:
                break;
        }
        elementShift.Scale(elementRectTransform.sizeDelta/2f);
        return OffScreenPoint(elementPos, offScreenSide) + elementShift;
    }
    public float GetDelay(Vector3 elementPos, OffScreenSide offScreenSide)
    {
        Transform[] points = new Transform[2];
        float lerpParameter = 0;
        switch (offScreenSide)
        {
            case OffScreenSide.Left:
                //inv lerp on y
                points = OffScreenLines[(int)OffScreenSide.Left].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.y, points[1].position.y, elementPos.y);
                break;
            case OffScreenSide.Right:
                //inv lerp on y
                points = OffScreenLines[(int)OffScreenSide.Right].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.y, points[1].position.y, elementPos.y);
                break;
            case OffScreenSide.Top:
                //inv lerp on x
                points = OffScreenLines[(int)OffScreenSide.Top].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.x, points[1].position.x, elementPos.x);
                break;
            case OffScreenSide.Bottom:
                //inv lerp on x
                points = OffScreenLines[(int)OffScreenSide.Bottom].points;
                lerpParameter = Mathf.InverseLerp(points[0].position.x, points[1].position.x, elementPos.x);
                break;
        }
        return OffScreenLines[(int)offScreenSide].GetDelay(lerpParameter);
    }
}
public enum OffScreenSide
{
    Left,Right,Top,Bottom
}
[System.Serializable]
public class OffScreenLine
{
    public Transform[] points = new Transform[2];
    public Vector2 delayBounds;
    public float GetDelay(float lerpParameter)
    {
        return Mathf.Lerp(delayBounds.x, delayBounds.y, lerpParameter);
    }
}
public enum MouseState
{
    Enter, Over, Exit
}
public enum MotionType
{
    Position, Size, Rotation
}