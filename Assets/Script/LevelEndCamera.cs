using System.Collections;
using UnityEngine;

public class LevelEndCamera : MonoBehaviour
{
    public float duration = 1f;
    public AnimationCurve curve;
    bool isFinish = false;

    private void Update()
    {
        if(isFinish == false)
        {
            PlaneController.instance.planeSound.volume += 0.01f;
        }
        else
        {
            PlaneController.instance.planeSound.volume -= 0.01f;
        }


    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isFinish = true;
            StartCoroutine(Shaking());
        }
    }

    private void OnEnable()
    {
        PlaneController.instance.planeSound.volume = 0.1f;
    }

}
