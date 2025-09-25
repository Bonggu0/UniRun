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

    [SerializeField]
    InputReader _inputReader;

    private float playerTime;
    float addTime;

    private void Start()
    {
        _inputReader.OnClickReset += Restart;
    }
    void Update()
    {
        addTime += Time.deltaTime;
        playerTime = Mathf.Round(addTime);
        timeUI.text = $"Time : {playerTime}";

        

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
    private void Restart()
    {
        dieUI.gameObject.SetActive(false);
        addTime = 0;
    }
}
