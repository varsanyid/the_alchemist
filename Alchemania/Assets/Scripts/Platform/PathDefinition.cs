using UnityEngine;
using System.Collections.Generic;

public class PathDefinition : MonoBehaviour {

    public Transform[] PathPoints;

    public IEnumerator<Transform> PathIterator()
    {
        if(PathPoints == null || PathPoints.Length < 1)
        {
            yield break;
        }
        var direction = 1;
        var index = 0;
        while(true)
        {
            yield return PathPoints[index];
            if(PathPoints.Length == 1)
            {
                continue;
            }
            if(index <= 0)
            {
                direction = 1;
            } else if(index >= PathPoints.Length -1)
            {
                direction = -1;
            }
            index = index + direction;
        }
    }
    
    void OnDrawGizmos()
    {
        if(PathPoints.Length < 2)
        {
            return;
        }
        for(int i = 1; i < PathPoints.Length; i++)
        {
            Gizmos.DrawLine(PathPoints[i - 1].position, PathPoints[i].position);
        }
    }

}
