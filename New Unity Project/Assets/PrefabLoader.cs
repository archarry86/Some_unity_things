using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var prefab = Resources.Load("Prefabs/Cube_1", typeof(GameObject));

        var go =   Instantiate(prefab) as GameObject;

        go.transform.position = new Vector3(1,1,1)* Random.Range(1, 5);

        var initobject = go;

        prefab = Resources.Load("Prefabs/Cube_2", typeof(GameObject));

        go = Instantiate(prefab) as GameObject;

        go.transform.position = (initobject.transform.position * -2) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
