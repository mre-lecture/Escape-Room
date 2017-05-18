using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMenu : MonoBehaviour {
    
    public Transform cubeSpawn;

    public void SpawnCube()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        go.transform.position = cubeSpawn.position;
        go.transform.rotation = cubeSpawn.rotation;
        go.AddComponent<Rigidbody>();
    }
}
