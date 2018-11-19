using UnityEngine;
using System.Collections;

public class DeckController : MonoBehaviour {
	private static int count = 1;

	//UI controls
	void OnMouseUp() {
		GameModel.Instance.PlayerHand.AddCard (new CardModel (){
			Name = "Test" + count++,
			Cost = 1,
			CostType = "Sinner",
			Text = "Does something",
			Strike = 1,
			Life = 1,
			Type = "Monster"
		});
	}


}
