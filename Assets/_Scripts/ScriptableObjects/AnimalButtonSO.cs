using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Button", menuName = "Animal Button")]
public class AnimalButtonSO : ScriptableObject
{
    public string animalName;
    public Sprite animalSprite;
    [TextArea] public string animalDescription;
    public bool isFlier;
    public bool isInsect;
    public bool isOmnivore;
    public bool isSoloLiver;
    public bool isMammal;
}