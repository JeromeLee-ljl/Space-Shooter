using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSkill : Skill
{
    public float jumpDistance = 2f;
    public Transform plane;

    private float getMouseDistance()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePosition - transform.position;
        float mouseDistance = dir.magnitude;
        return mouseDistance < jumpDistance ? mouseDistance : jumpDistance;
    }

    protected override void Effect()
    {
        plane.transform.position += transform.up * getMouseDistance();
    }
}