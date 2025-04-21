using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float smooth = 10.0f;     

    private Vector3 dollyDir;
    private float dollyDistance;

    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        dollyDistance = transform.localPosition.magnitude;
    }

    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * Mathf.Clamp(dollyDistance, minDistance, 1000f));
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            float hitDistance = Vector3.Distance(transform.parent.position, hit.point);
            transform.localPosition = dollyDir * Mathf.Clamp(hitDistance * 0.8f, minDistance, dollyDistance); // Ajusta el factor 0.8 para separar un poco más
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * dollyDistance, Time.deltaTime * smooth);
        }
    }
}