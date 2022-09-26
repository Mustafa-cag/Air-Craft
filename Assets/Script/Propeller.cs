using UnityEngine;

public class Propeller : MonoBehaviour
{
    public float velocity;
    public float getToVelocity;

    void Update()
    {
        getToVelocity = PlaneController.instance.gear;

        float ProppellerSpeed(Propeller benimLevel, float deneme)
            => benimLevel switch
            {
                { getToVelocity: 0 } => velocity = 0f,
                { getToVelocity: 1 } => velocity = 1f,
                { getToVelocity: 2 } => velocity = 2f,
                { getToVelocity: 3 } => velocity = 3f,
                { getToVelocity: 4 } => velocity = 4f,
                { getToVelocity: 5 } => velocity = 5f,
                { getToVelocity: 6 } => velocity = 6f,
                { getToVelocity: 7 } => velocity = 7f,
                { getToVelocity: 8 } => velocity = 8f,
                { getToVelocity: 9 } => velocity = 9f,
                { getToVelocity: 10 } => velocity = 10f,
                _ => getToVelocity
            };
        ProppellerSpeed(this, PlaneController.instance.gear);


        transform.Rotate(0, 0, velocity * 2f);
    }
}
