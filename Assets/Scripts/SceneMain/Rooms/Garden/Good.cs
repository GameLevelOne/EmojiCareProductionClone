using System.Collections;
using UnityEngine;

public class Good : MonoBehaviour {
	public delegate void GoodHarvested(int index);
	public event GoodHarvested OnGoodHarvested;
	#region attributes
	[Header("Reference")]
	public Rigidbody2D thisRigidBody;
	public Collider2D thisCollider;
	public SpriteRenderer thisSprite;
	public Animator thisAnim;

	public IngredientType type;
	[Header("Do Not Modify This!")]
	public int GoodsIndex;

	Vector3 startPos;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(int index)
	{
		startPos = transform.position;
		GoodsIndex = index;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.BASKET){
			StopAllCoroutines();
			PlayerData.Instance.inventory.ModIngredientValue(type,1);

			if(OnGoodHarvested != null) OnGoodHarvested(GoodsIndex);

			Destroy(this.gameObject);
		}
	}

	public void BeginDrag()
	{
		thisSprite.sortingLayerName = SortingLayers.HELD;
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
		thisRigidBody.simulated = false;
		thisCollider.enabled = false;
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(Return());
	}
	#endregion
	IEnumerator Return()
	{
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
		thisRigidBody.simulated = true;
		thisCollider.enabled = true;

		yield return null;

		float t = 0;
		Vector3 currentPos = transform.position;
		while (t < 1){
			transform.position = Vector3.Lerp(currentPos,startPos,t);
			t+= Time.deltaTime;
			yield return null;
		}
		transform.position = startPos;
	}
}
