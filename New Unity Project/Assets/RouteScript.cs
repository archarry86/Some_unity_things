using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteScript : MonoBehaviour
{


    [SerializeField]
    private Transform[] controlPositions;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private Vector2 Gizmoposition;
    public void OnDrawGizmos()
    {
        if (controlPositions == null || controlPositions.Length == 0)
            return;

        Gizmos.color = Color.green;

        for (float t = 0; t <= 1.0f; t += 0.010f)
        {

            Gizmoposition = Mathf.Pow(1 - t, 3) * controlPositions[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * controlPositions[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPositions[2].position +
                Mathf.Pow(t, 3) * controlPositions[3].position;

            Gizmos.DrawSphere(Gizmoposition, 0.25F);


        }


        Gizmos.color = Color.white;

        Gizmos.DrawLine(new Vector2(controlPositions[0].position.x, controlPositions[0].position.y), new Vector2(controlPositions[1].position.x, controlPositions[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPositions[2].position.x, controlPositions[2].position.y), new Vector2(controlPositions[3].position.x, controlPositions[3].position.y));


    }
}
