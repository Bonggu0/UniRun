using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    [SerializeField]
    TextMeshProUGUI timeUI;

    [SerializeField]
    TextMeshProUGUI dieUI;

    private float playerTime;
    float addTime;

    void Update()
    {
        addTime += Time.deltaTime;
        playerTime = Mathf.Round(addTime);
        timeUI.text = $"Time : {playerTime}";

        if (Input.GetKeyDown(KeyCode.R))
        {
            dieUI.gameObject.SetActive(false);
            addTime = 0;
        }

        if (_player.isDie)
        {
            dieUI.gameObject.SetActive(true);
            addTime = 0;
        }

        if (!_player.isDie)
        {
            dieUI.gameObject.SetActive(false);
        }
    }
}
