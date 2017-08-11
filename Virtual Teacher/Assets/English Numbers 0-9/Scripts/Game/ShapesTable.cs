﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

[DisallowMultipleComponent]
public class ShapesTable : MonoBehaviour
{
		/// <summary>
		/// Whether to create groups pointers or not.
		/// </summary>
		public bool createGroupsPointers = true;

		/// <summary>
		/// Whether to save the last selected group or not.
		/// </summary>
		public bool saveLastSelectedGroup = true;

		/// <summary>
		/// The shapes list.
		/// </summary>
		public static List<TableShape> shapes;

		/// <summary>
		/// The groups parent.
		/// </summary>
		public Transform groupsParent;

		/// <summary>
		/// The pointers parent.
		/// </summary>
		public Transform pointersParent;

		/// <summary>
		/// The collected stars text.
		/// </summary>
		public Text collectedStarsText;

		/// <summary>
		/// The shape bright.
		/// </summary>
		public Transform shapeBright;

		/// <summary>
		/// The star on sprite.
		/// </summary>
		public Sprite starOn;

		/// <summary>
		/// The star off sprite.
		/// </summary>
		public Sprite starOff;

		/// <summary>
		/// The shape prefab.
		/// </summary>
		public GameObject shapePrefab;
	
		/// <summary>
		/// The shapes group prefab.
		/// </summary>
		public GameObject shapesGroupPrefab;

		/// <summary>
		/// The pointer prefab.
		/// </summary>
		public GameObject pointerPrefab;

		/// <summary>
		/// temporary transform.
		/// </summary>
		private Transform tempTransform;

		/// <summary>
		/// The Number of shapes per group.
		/// </summary>
		[Range (1, 100)]
		public int shapesPerGroup = 12;

		/// <summary>
		/// Number of columns per group.
		/// </summary>
		[Range (1, 10)]
		public int columnsPerGroup = 3;

		/// <summary>
		/// Whether to enable group grid layout.
		/// </summary>
		public bool EnableGroupGridLayout = true;

		/// <summary>
		/// The last shape that user reached.
		/// </summary>
		private Transform lastShape;

		/// <summary>
		/// The last selected group.
		/// </summary>
		public static int lastSelectedGroup;

		/// <summary>
		/// The collected star.
		/// </summary>
		private int collectedStars;

		void Awake ()
		{
				collectedStars = 0;

				//define the shapes list
				shapes = new List<TableShape> ();

				//Create new shapes
				CreateShapes ();
	
				//Setup the last selected group index
				ScrollSlider scrollSlider = GameObject.FindObjectOfType<ScrollSlider> ();
				if (saveLastSelectedGroup) {
					scrollSlider.currentGroupIndex = lastSelectedGroup;
				}
		}

		void Update ()
		{
				if (lastShape != null) {
						//Set the bright postion to the last shape postion
						if (!Mathf.Approximately (lastShape.position.magnitude, shapeBright.position.magnitude)) {
								shapeBright.position = lastShape.position;
						}
				}
		}


		/// <summary>
		/// Creates the shapes in Groups.
		/// </summary>
		private void CreateShapes ()
		{
				//Clear current shapes list
				shapes.Clear ();

				//The ID of the shape
				int ID = 0;
			
				GameObject shapesGroup = null;
					
				//Create Shapes inside groups
				for (int i = 0; i < ShapesManager.instance.shapes.Count; i++) {

						if (i % shapesPerGroup == 0) {
								int groupIndex = (i / shapesPerGroup);
								shapesGroup = Group.CreateGroup (shapesGroupPrefab, groupsParent, groupIndex, columnsPerGroup);
								if(!EnableGroupGridLayout){
									shapesGroup.GetComponent<GridLayoutGroup>().enabled = false;
								}
				  				 if (createGroupsPointers) {
										Pointer.CreatePointer (groupIndex, shapesGroup, pointerPrefab, pointersParent);
								}
						}

						//Create Shape
						ID = (i + 1);//the id of the shape
						GameObject tableShapeGameObject = Instantiate (shapePrefab, Vector3.zero, Quaternion.identity) as GameObject;
						tableShapeGameObject.transform.SetParent (shapesGroup.transform);//setting up the shape's parent
						TableShape tableShapeComponent = tableShapeGameObject.GetComponent<TableShape> ();//get TableShape Component
						tableShapeComponent.ID = ID;//setting up shape ID
						tableShapeGameObject.name = "Shape-" + ID;//shape name
						tableShapeGameObject.transform.localScale = Vector3.one;
						tableShapeGameObject.GetComponent<RectTransform> ().offsetMax = Vector2.zero;
						tableShapeGameObject.GetComponent<RectTransform> ().offsetMin = Vector2.zero;

						GameObject content = Instantiate(ShapesManager.instance.shapes[i].gamePrefab,Vector3.zero,Quaternion.identity) as GameObject;

						content.transform.SetParent(tableShapeGameObject.transform.Find("Content"));

						RectTransform rectTransform = tableShapeGameObject.transform.Find("Content").GetComponent<RectTransform>();
		
						float ratio = Mathf.Max(Screen.width,Screen.height)/1000.0f;

						//set up the scale
						content.transform.localScale = new Vector3(ratio * 0.7f  ,ratio * 0.7f);

						//release unwanted resources
						content.GetComponent<Shape>().enabled = false;
						content.GetComponent<Animator>().enabled = false;
						content.transform.Find("TracingHand").gameObject.SetActive(false);
						content.transform.Find("Collider").gameObject.SetActive(false);

						Animator [] animators = content.transform.GetComponentsInChildren<Animator>();
						foreach(Animator a in animators){
							a.enabled = false;
						}
						
						int from, to;
						string [] slices;
						List <Transform> paths = CommonUtil.FindChildrenByTag (content.transform.Find ("Paths"), "Path");
						foreach (Transform p in paths) {
							slices = p.name.Split ('-');
							from = int.Parse (slices [1]);
							to = int.Parse (slices [2]);
			
							p.Find("Start").gameObject.SetActive(false);
							Image img = CommonUtil.FindChildByTag(p,"Fill").GetComponent<Image>();
							if(PlayerPrefs.HasKey("Shape-" + ID + "-Path-" + from + "-" + to)){
								List<Transform> numbers = CommonUtil.FindChildrenByTag(p.transform.Find("Numbers"),"Number");
								foreach(Transform n in numbers){
									n.gameObject.SetActive(false);
								}
								img.fillAmount = 1;
								img.color =	DataManager.GetShapePathColor (ID, from, to);
							}
						}
						tableShapeGameObject.GetComponent<Button> ().onClick.AddListener (() => GameObject.FindObjectOfType<UIEvents> ().AlbumShapeEvent (tableShapeGameObject.GetComponent<TableShape> ()));

						SettingUpShape (tableShapeComponent, ID);//setting up the shape contents (stars number ,islocked,...)
						shapes.Add (tableShapeComponent);//add table shape component to the list
				}


				collectedStarsText.text = collectedStars + "/" + (3 * ShapesManager.instance.shapes.Count);
				if (ShapesManager.instance.shapes.Count == 0) {
						Debug.Log ("There are no Shapes found");
				} else {
						Debug.Log ("New shapes have been created");
				}
		}

		/// <summary>
		/// Settings up the shape contents in the table.
		/// </summary>
		/// <param name="tableShape">Table shape.</param>
		/// <param name="ID">ID of the shape.</param>
		private void SettingUpShape (TableShape tableShape, int ID)
		{
				if (tableShape == null) {
						return;
				}

				tableShape.isLocked = DataManager.IsShapeLocked (ID);
				tableShape.starsNumber = DataManager.GetShapeStars (ID);

				if (tableShape.ID == 1) {
						tableShape.isLocked = false;
				}

				if (!tableShape.isLocked) {
						tableShape.transform.Find ("Cover").gameObject.SetActive (false);
						tableShape.transform.Find ("Lock").gameObject.SetActive (false);
				} else {
						tableShape.transform.Find ("Stars").gameObject.SetActive (false);
				}

				tempTransform = tableShape.transform.Find ("Stars");

				//Apply the current Stars Rating 
				if (tableShape.starsNumber == TableShape.StarsNumber.ONE) {//One Star
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
						collectedStars+=1;
				} else if (tableShape.starsNumber == TableShape.StarsNumber.TWO) {//Two Stars
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
						collectedStars+=2;
				} else if (tableShape.starsNumber == TableShape.StarsNumber.THREE) {//Three Stars
						tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
						tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOn;
						collectedStars+=3;
				} else {//Zero Stars
					tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOff;
					tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
					tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
				}
		}
		
		/// <summary>
		/// Raise the change group event.
		/// </summary>
		/// <param name="currentGroup">Current group.</param>
		public void OnChangeGroup (int currentGroup)
		{
				if (saveLastSelectedGroup) {
					lastSelectedGroup = currentGroup;
				}
		}
}