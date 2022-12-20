using UnityEngine;

public class LineGanBogan : MonoBehaviour
{
    float temp = 0;
    void Update()
    {
        temp += Time.deltaTime;
        transform.position = BezierCurve(temp, new Vector3(0, 5, 0), new Vector3(0, -1, 0));
        if(temp >= 2)
        {
            Destroy(this.gameObject);
        }
    }

    Vector3 BezierCurve(float t, Vector3 p0, Vector3 p1)
    {
        return ((1 - t) * p0) + ((t) * p1);
    }
}
