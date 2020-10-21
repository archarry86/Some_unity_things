using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class BrezierCurvePathScript : MonoBehaviour
{

    [SerializeField]
    private Transform[] Routes;

    private Vector2 forward;

    private int RouteIndex = 0;

    private float tparam = 0;

    private Vector2 playerPosition;

    [SerializeField]
    private float speedmodifier;

    private bool corutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        forward = this.transform.up.normalized;
        RouteIndex = 0;
        corutineAllowed = true;
        // speedmodifier = 0.5f;
        tparam = 0;

        Debug.DrawLine(this.transform.position, this.transform.position + Vector3.right * 10, Color.red, 10000);



    }

    // Update is called once per frame
    void Update()
    {

        if (Routes == null || Routes.Contains(null))
            return;

        if (corutineAllowed)
        {
            StartCoroutine(goBytheRouthe(RouteIndex));
        }
    }




    private IEnumerator goBytheRouthe(int routenumber)
    {
        corutineAllowed = false;

        Transform currentRoute = Routes[routenumber];


        Vector2 postion_1 = currentRoute.GetChild(0).position;
        Vector2 postion_2 = currentRoute.GetChild(1).position;
        Vector2 postion_3 = currentRoute.GetChild(2).position;
        Vector2 postion_4 = currentRoute.GetChild(3).position;

        this.transform.position = postion_1;
        //  this.transform.LookAt(postion_1 + forward * 2);

        var resultangle = Vector2.Angle(Vector2.right, this.transform.position);


        var euler = this.transform.rotation.eulerAngles;


        euler.z = resultangle;


        Debug.Log(euler);

        var rotation = this.transform.rotation;
        rotation.eulerAngles = euler;

        this.transform.rotation = rotation;

        while (tparam < 1)
        {

            Vector2 lastposition = this.transform.position;

            tparam += Time.deltaTime * speedmodifier;

            playerPosition = Mathf.Pow(1 - tparam, 3) * postion_1 +
             3 * Mathf.Pow(1 - tparam, 2) * tparam * postion_2 +
             3 * (1 - tparam) * Mathf.Pow(tparam, 2) * postion_3 +
             Mathf.Pow(tparam, 3) * postion_4;

            this.transform.position = playerPosition;

            Vector3 diff = playerPosition - lastposition;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);





            yield return new WaitForEndOfFrame();
        }

        tparam = 0;

        RouteIndex++;

        if (RouteIndex > Routes.Length - 1)
        {
            RouteIndex = 0;
        }


        corutineAllowed = true;
    }
}
