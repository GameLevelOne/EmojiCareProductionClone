using UnityEngine;

[CreateAssetMenu(fileName = "Hat_",menuName = "SOData/Hat",order = 3)]
public class HatSO : ScriptableObject {
	public string ID = "Hat_";
	public string name = "";
	public int price = 0;
	public GameObject hatObject;
}
