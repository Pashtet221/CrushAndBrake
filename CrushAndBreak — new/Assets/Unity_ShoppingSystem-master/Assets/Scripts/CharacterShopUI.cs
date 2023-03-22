using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterShopUI : MonoBehaviour
{
	public static CharacterShopUI instance {get; private set;}

	[Header ("Layout Settings")]
	[SerializeField] float itemSpacing = .5f;
	float itemHeight;

	[Header ("UI elements")]
	[SerializeField] GameObject[] skins ;
	[SerializeField] Image selectedCharacterIcon;
	[SerializeField] Transform ShopMenu;
	[SerializeField] Transform ShopItemsContainer;
	[SerializeField] GameObject itemPrefab;
	[Space (20)]
	[SerializeField] CharacterShopDatabase characterDB;

	[Space (20)]
	[Header ("Shop Events")]
	[SerializeField] GameObject shopUI;
	[SerializeField] Button openShopButton;

	int newSelectedItemIndex = 0;
	int previousSelectedItemIndex = 0;


	void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		//Fill the shop's UI list with items
		GenerateShopItemsUI ();

		//Set selected character in the playerDataManager .
		SetSelectedCharacter ();

		//Select UI item
		SelectItemUI (GameDataManager.GetSelectedCharacterIndex ());

		//update player skin (Main menu)
		ChangePlayerSkin ();
	}

	void SetSelectedCharacter ()
	{
		//Get saved index
		int index = GameDataManager.GetSelectedCharacterIndex ();

		//Set selected character
		GameDataManager.SetSelectedCharacter (characterDB.GetCharacter (index), index);
	}

	void GenerateShopItemsUI ()
	{	
		//Loop throw save purchased items and make them as purchased in the Database array
		for (int i = 0; i < GameDataManager.GetAllPurchasedCharacter ().Count; i++) {
			int purchasedCharacterIndex = GameDataManager.GetPurchasedCharacter (i);
			characterDB.PurchaseCharacter (purchasedCharacterIndex);
		}
		
		//Delete itemTemplate after calculating item's Height :
		itemHeight = ShopItemsContainer.GetChild (0).GetComponent <RectTransform> ().sizeDelta.y;
		Destroy (ShopItemsContainer.GetChild (0).gameObject);
		//DetachChildren () will make sure to delete it from the hierarchy, otherwise if you 
		//write ShopItemsContainer.ChildCount you w'll get "1"
		ShopItemsContainer.DetachChildren ();

		//Generate Items
		for (int i = 0; i < characterDB.CharactersCount; i++) {
			//Create a Character and its corresponding UI element (uiItem)
			Character character = characterDB.GetCharacter (i);
			CharacterItemUI uiItem = Instantiate (itemPrefab, ShopItemsContainer).GetComponent <CharacterItemUI> ();

			//Move item to its position
			uiItem.SetItemPosition (Vector2.down * i * (itemHeight + itemSpacing));

			//Set Item name in Hierarchy (Not required)
			uiItem.gameObject.name = "Item" + i + "-" + character.weaponName;

			uiItem.SetCharacterImage (character.image);
			uiItem.SetCharacterPrice (character.price);

			if (character.isPurchased) {
				//Character is Purchased
				uiItem.SetCharacterAsPurchased ();
				uiItem.OnItemSelect (i, OnItemSelected);
			} else {
				//Character is not Purchased yet
				uiItem.OnItemPurchase (i, OnItemPurchased);
			}

			//Resize Items Container
			ShopItemsContainer.GetComponent <RectTransform> ().sizeDelta = 
				Vector2.up * ((itemHeight + itemSpacing) * characterDB.CharactersCount + itemSpacing);

			//you can use VerticalLayoutGroup with ContentSizeFitter to skip all of this :
			//(moving items & resizing the container)
		}

	}

	void ChangePlayerSkin ()
	{
		Character character = GameDataManager.GetSelectedCharacter ();
		if (character.image != null) {

			 int selectedSkin = GameDataManager.GetSelectedCharacterIndex () ;

         // show selected skin's gameobject:
         skins [ selectedSkin ].SetActive (true) ;

         // hide other skins (except selectedSkin) :
         for (int i = 0; i < skins.Length; i++)
            if (i != selectedSkin)
               skins [ i ].SetActive (false) ;
		}
	}

	void OnItemSelected (int index)
	{
		// Select item in the UI
		SelectItemUI (index);

		//Save Data
		GameDataManager.SetSelectedCharacter (characterDB.GetCharacter (index), index);

		//Change Player Skin
		ChangePlayerSkin ();
	}

	void SelectItemUI (int itemIndex)
	{
		previousSelectedItemIndex = newSelectedItemIndex;
		newSelectedItemIndex = itemIndex;

		CharacterItemUI prevUiItem = GetItemUI (previousSelectedItemIndex);
		CharacterItemUI newUiItem = GetItemUI (newSelectedItemIndex);

		prevUiItem.DeselectItem ();
		newUiItem.SelectItem ();

	}

	CharacterItemUI GetItemUI (int index)
	{
		return ShopItemsContainer.GetChild (index).GetComponent <CharacterItemUI> ();
	}

	void OnItemPurchased (int index)
	{
		Character character = characterDB.GetCharacter (index);
		CharacterItemUI uiItem = GetItemUI (index);

		if (GameDataManager.CanSpendCoins (character.price)) {
			//Proceed with the purchase operation
			GameDataManager.SpendCoins (character.price);
			
			GameSharedUI.Instance.UpdateCoinsUIText ();

			//Update DB's Data
			characterDB.PurchaseCharacter (index);

			uiItem.SetCharacterAsPurchased ();
			uiItem.OnItemSelect (index, OnItemSelected);

			//Add purchased item to Shop Data
			GameDataManager.AddPurchasedCharacter (index);

		} else {
			uiItem.AnimateShakeItem ();
		}
	}

	public void OpenShop ()
	{
		shopUI.SetActive (true);
	}

	public void CloseShop ()
	{
		shopUI.SetActive (false);
	}
}
