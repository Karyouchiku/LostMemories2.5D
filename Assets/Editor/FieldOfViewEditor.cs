using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
        
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.minRadius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle/2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle/2);
        
        Vector3 viewAngle11 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle2/2);
        Vector3 viewAngle12 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle2/2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position , fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position , fov.transform.position + viewAngle02 * fov.radius);
        
        Handles.color = Color.blue;
        Handles.DrawLine(fov.transform.position , fov.transform.position + viewAngle11 * fov.minRadius);
        Handles.DrawLine(fov.transform.position , fov.transform.position + viewAngle12 * fov.minRadius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }

    Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)) ;
    }
}
