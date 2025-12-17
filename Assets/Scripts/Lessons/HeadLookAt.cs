using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;
    public float maxAngle = 60f;

    private Quaternion initialLocalRotation;

    void Start()
    {
        initialLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // переводим в локальное пространство
        Quaternion targetLocalRotation =
            Quaternion.Inverse(transform.parent.rotation) * lookRotation;

        // ограничение угла
        Quaternion limitedRotation = Quaternion.RotateTowards(
            initialLocalRotation,
            targetLocalRotation,
            maxAngle
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            limitedRotation,
            Time.deltaTime * rotationSpeed
        );
    }
}
