using UnityEngine;
using System.Collections;
using System;

///Developed by Indie Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268
///www.indiestd.com
///info@indiestd.com
///copyright © 2016 IGS. All rights reserved.

[DisallowMultipleComponent]
public class TableShape : MonoBehaviour
{
		/// <summary>
		/// The selected shape.
		/// </summary>
		public static TableShape selectedShape;

		/// <summary>
		/// Table Shape ID.
		/// </summary>
		public int ID = -1;

		/// <summary>
		/// The stars number(Rating).
		/// </summary>
		public StarsNumber starsNumber = StarsNumber.ZERO;

		/// <summary>
		/// Whether the shape is locked or not.
		/// </summary>
		[HideInInspector]
		public bool isLocked = true;

		// Use this for initialization
		void Start ()
		{
				///Setting up the ID for Table Shape
				if (ID == -1) {
						string [] tokens = gameObject.name.Split ('-');
						if (tokens != null) {
								ID = int.Parse (tokens [1]);
						}
				}
		}

		public enum StarsNumber
		{
				ZERO,
				ONE,
				TWO,
				THREE
		}
}