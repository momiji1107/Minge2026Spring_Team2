using UnityEngine;

public class SelectedPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData[] playerDatas;
    private PlayerData _playerData;
    
    public PlayerData PlayerData => _playerData;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManagement.GameState = GAMESTATE.INGAME;
        
        switch (PlayerSelection.selectedCharacter)
        {
            case CharacterName.PLAYER_ONE:
                Debug.Log("Your choice is " + PlayerSelection.selectedCharacter);
                _playerData = playerDatas[0];
                break;
            case CharacterName.PLAYER_TWO:
                Debug.Log("Your choice is " + PlayerSelection.selectedCharacter);
                _playerData = playerDatas[1];
                break;
            case CharacterName.PLAYER_THREE:
                Debug.Log("Your choice is " + PlayerSelection.selectedCharacter);
                _playerData = playerDatas[2];
                break;
        }
    }
}
