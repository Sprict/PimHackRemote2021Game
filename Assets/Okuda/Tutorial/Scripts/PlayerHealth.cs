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
            Transform gun = GetComponent<PlayerBehaviour>().Center.transform.GetChild(0);
            gun.gameObject.SetActive(false);
            BoltNetwork.Destroy(gameObject);
        }
    }

    public void Damage(bool damage1)
    {
        if (entity.IsOwner)
        {
            state.Health -= damage1 ? 1 : 3;
        }
    }

}
