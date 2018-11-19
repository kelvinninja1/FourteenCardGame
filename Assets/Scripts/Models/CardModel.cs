using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardModel {
	public GameObject Instance { get; private set; }
	private string nameVal, typeVal, costTypeVal, textVal;
	private int strikeVal, lifeVal, costVal;
	
	public Position PosType { get; private set; }

	private Vector3 lastStaticPos;
	private Position lastPosType;


	public string Name { 
		get{
			return nameVal;
		}
		set{
			nameVal = value;
			if(Instance != null){
				Instance.transform.Find ("CardName").gameObject.GetComponent<Text> ().text = value;
			}
		}
	}
	public string Type { 
		get {
			return typeVal;
		}
		set {
			typeVal = value;
			if (Instance != null) {
				Instance.transform.Find ("Type").gameObject.GetComponent<Text> ().text = value;
			}
		}
	}
	public string CostType { 
		get {
			return costTypeVal;
		}
		set {
			costTypeVal = value;
			if (Instance != null) {
				Instance.transform.Find ("CostLabel").gameObject.GetComponent<Text> ().text = value;
			}
		}
	}
	public string Text { 
		get {
			return textVal;
		}
		set {
			textVal = value;
			if (Instance != null) {
				Instance.transform.Find ("Text").gameObject.GetComponent<Text> ().text = value;
			}
		}
	}
	public int Strike { 
		get {
			return strikeVal;
		}
		set {
			strikeVal = value;
			if (Instance != null) {
				Instance.transform.Find ("StrikeLife").gameObject.GetComponent<Text> ().text = value + " / " + Life;
			}
		}
	}
	public int Life { 
		get {
			return lifeVal;
		}
		set {
			lifeVal = value;
			if (Instance != null) {
				Instance.transform.Find ("StrikeLife").gameObject.GetComponent<Text> ().text = Strike + " / " + value;
			}
		}
	}
	public int Cost { 
		get {
			return costVal;
		}
		set {
			costVal = value;
			if (Instance != null) {
				Instance.transform.Find ("CostLabel").gameObject.transform.Find("Cost").GetComponent<Text> ().text = value.ToString();
			}
		}
	}
	
	public void CreateCardInstance(string owner){
		Instance = GameObject.Instantiate(Resources.Load ("Card")) as GameObject;
		Name = Name;
		CostType = CostType;
		Cost = Cost;
		Strike = Strike;
		Life = Life;
		Text = Text;
		Type = Type;
		Instance.GetComponent<CardController> ().Owner = owner;

		GameModel.Instance.CardCreated (Instance, this);
	}

	public void MoveCardInstance(Vector3 newPosition, Position? type){
		Instance.transform.position = newPosition;
		lastStaticPos = newPosition;

		if (type != null) {
			PosType = type.GetValueOrDefault();
			lastPosType = PosType;
		}
	}

	public void MoveCardInstance(float? x, float? y, float? z, Position? type = null){
		var curPos = Instance.transform.position;
		float xPos = x.HasValue ? x.Value : curPos.x;
		float yPos = y.HasValue ? y.Value : curPos.y;
		float zPos = z.HasValue ? z.Value : curPos.z;

		MoveCardInstance(new Vector3(xPos, yPos, zPos), type);
	}

	public void ResetInstancePosition(){
		Instance.transform.position = lastStaticPos;
		PosType = lastPosType;
	}
}
