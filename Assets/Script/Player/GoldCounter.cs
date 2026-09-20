using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    public PlayerControls player;
    public TextMeshProUGUI goldText; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + player.gold;
    }
}
