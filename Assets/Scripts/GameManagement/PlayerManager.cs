using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static PlayerTestScript lightPlayer;
    static PlayerTestScript darkPlayer;
    static int lightRespawnTimer = 0;
    static int darkRespawnTimer = 0;

    static bool isLightPlayerDead = false;
    static bool isDarkPlayerDead = false;

    static readonly Vector3 lightRespawnPoint = new(0, 0, 0);
    static readonly Vector3 darkRespawnPoint = new(0, 0, 0);

    public static void SetLightPlayer(PlayerTestScript lp)
    {
        lightPlayer = lp;
    }

    public static void SetDarkPlayer(PlayerTestScript dp)
    {
        darkPlayer = dp;
    }

    public static void OnLightPlayerDeath()
    {
        lightPlayer.gameObject.SetActive(false);
        lightRespawnTimer = 250;
        isLightPlayerDead = true;
        lightPlayer.Revive();
    }

    public static void OnDarkPlayerDeath()
    {
        darkPlayer.gameObject.SetActive(false);
        darkRespawnTimer = 250;
        isDarkPlayerDead = true;
        darkPlayer.Revive();
    }

    static void RespawnLightPlayer()
    {
        lightPlayer.transform.position = lightRespawnPoint;
        isLightPlayerDead = false;
        lightPlayer.gameObject.SetActive(true);
    }

    static void RespawnDarkPlayer()
    {
        darkPlayer.transform.position = darkRespawnPoint;
        isDarkPlayerDead = false;
        darkPlayer.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (isLightPlayerDead)
        {
            if (isDarkPlayerDead)
            {
                // TODO: actually implement this
                GameManager.SetYouWin();
            }
            lightRespawnTimer--;
            if (lightRespawnTimer <= 0)
                RespawnLightPlayer();
        }

        if (isDarkPlayerDead)
        {
            darkRespawnTimer--;
            if (darkRespawnTimer <= 0)
                RespawnDarkPlayer();
        }
    }
}
