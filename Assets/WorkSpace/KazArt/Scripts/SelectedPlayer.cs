using UnityEngine;

public class SelectedPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (PlayerSelection.selectedCharacter)
        {
            case CharacterName.PLAYER_ONE:
                Debug.Log("Your choice is " + PlayerSelection.selectedCharacter);
                break;
            case CharacterName.PLAYER_TWO:
                Debug.Log("Your choice is " + PlayerSelection.selectedCharacter);
                break;
            case CharacterName.PLAYER_THREE:
                Debug.Log("Your choice is " + PlayerSelection.selectedCharacter);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
