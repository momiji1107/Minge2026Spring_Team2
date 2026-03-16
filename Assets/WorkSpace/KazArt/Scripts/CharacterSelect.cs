using UnityEngine;

public enum CharacterName{
    PLAYER_ONE,
    PLAYER_TWO,
    PLAYER_THREE
};

public static class PlayerSelection
{
    public static CharacterName selectedCharacter;
}

public class CharacterSelect : MonoBehaviour
{
    public CharacterName characterName;
    public void ChangeCharacter()
    {
        PlayerSelection.selectedCharacter = characterName;
    }
}
