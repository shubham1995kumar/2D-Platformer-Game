using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startpoint;
    public Transform endpoint;
    public float speed;
    int direction = 1;

    void FixedUpdate()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.fixedDeltaTime);
        float distance = Vector2.Distance(platform.position, target);
        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return endpoint.position;
        }
        else
        {
            return startpoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (platform != null && startpoint != null && endpoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startpoint.position);
            Gizmos.DrawLine(platform.transform.position, endpoint.position);
        }
    }
}
