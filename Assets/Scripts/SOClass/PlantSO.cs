using UnityEngine;

[CreateAssetMenu(fileName = "Plant_",menuName = "SOData/Plant",order = 2)]
public class PlantSO : ScriptableObject {
	public IngredientType type;
	public Sprite[] plantStages;
	public GameObject ingredientObject;

	[Header("Duration is in minutes")]
	public int GrowTime = 30;
}
