using UnityEngine;

public class PlayerPaper : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem playerPaper;

    private ParticleSystem.EmissionModule emission;

    private void Awake()
    {
        emission = playerPaper.emission;
    }

    private void Update()
    {
        if(player.GetState() == Player.State.SingleJump || player.GetState() == Player.State.DoubleJump)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
    }
}
