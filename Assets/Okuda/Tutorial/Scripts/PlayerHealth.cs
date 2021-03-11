using UnityEngine;

public class PlayerHealth : Bolt.EntityBehaviour<ICubeState>
{
    public int localHealth;

    // void Start
    public override void Attached()
    {
        if (entity.IsOwner)
        { 
            state.Health = localHealth; 
        }

        state.AddCallback("Health", HealthCallback);
    }
    private void HealthCallback()
    {
        localHealth = state.Health;

        if (localHealth <= 0)
        {
            BoltNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            state.Health -= 1;
        }
    }

}
