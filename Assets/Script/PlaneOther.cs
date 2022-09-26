using UnityEngine;

public class PlaneOther : MonoBehaviour
{
    public float speed = 10;
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
