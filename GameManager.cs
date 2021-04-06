using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ISetupObject _player;
    private PlatformsManager _platformManager;

    private void Awake()
    {
        var platformManagerGO = GameObject.FindGameObjectWithTag("PlatformsContainer");
        if (platformManagerGO)
            _platformManager = platformManagerGO.GetComponent<PlatformsManager>();

        var playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO)
            _player = playerGO.GetComponent<ISetupObject>();

    }

    void Start()
    {
        if (_platformManager != null)
            _platformManager.SetupObject();

        if (_player != null)
           _player.SetupObject();
    }
}
