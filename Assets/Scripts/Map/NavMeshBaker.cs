using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Script to bake the NavMesh
 * at runtime  
 * 
 * Attach this script to the object
 * that has a NavMeshSurface component
 * 
 */
[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        // Get the NavMeshSurface directly from the current GameObject
        navMeshSurface = GetComponent<NavMeshSurface>();

        // Check if NavMeshSurface exists and build the NavMesh
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface component is missing.");
        }
    }

    public void BakeNavigationMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
