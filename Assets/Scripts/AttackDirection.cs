using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization;
using UnityEngine;

public enum AttackDirection
{
    left = 0,
    right,
    up,
    down, 
    none
}

public static class AttackDirections
{
    static Vector2[] compass = new Vector2[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    public static AttackDirection ClosestDirection(Vector2 movementDirection)
    {
        float maxDot = float.MinValue;
        AttackDirection result = AttackDirection.none;

        for (int i = 0; i < compass.Length; i++)
        {
            float t = Vector2.Dot(compass[i], movementDirection);
            if (t > maxDot)
            {
                maxDot = t;
                result = (AttackDirection)i;
            }
        }

        return result;
    }
}
