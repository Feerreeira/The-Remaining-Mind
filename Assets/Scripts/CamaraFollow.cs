using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float suavidade = 5f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 destino = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, destino, suavidade * Time.deltaTime);
        transform.LookAt(target);
    }
}
