using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostForce = 10f;
    public Vector3 boostDirection = Vector3.forward;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Boost(boostForce, boostDirection);
        }
    }
}
