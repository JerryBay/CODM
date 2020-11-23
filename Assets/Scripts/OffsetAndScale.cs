using UnityEngine;

public class OffsetAndScale : MonoBehaviour
{
    public static Vector3 Correct(Vector3 vertex)
    {
        Vector3 newVertex = new Vector3(vertex.x, vertex.z, vertex.y);
        newVertex /= 100;
        float angle = Mathf.PI / 2*(90.00001f/90f);
        Matrix3x3 mtr = new Matrix3x3(new Vector3(1, 0, 0), new Vector3(0, Mathf.Cos(angle), -Mathf.Sin(angle)), new Vector3(0, Mathf.Sin(angle), Mathf.Cos(angle)));

        newVertex = mtr * newVertex;
        newVertex = new Vector3(newVertex.x, newVertex.y - 0.08f, newVertex.z - 0.045f);
        return newVertex;
    }
}
