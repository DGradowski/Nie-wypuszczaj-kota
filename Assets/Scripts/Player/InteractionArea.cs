using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
	[SerializeField] PlayerInteraction playerInteraction;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Interactive")
		{
			playerInteraction.possibleInteractions.Add(collision.gameObject.GetComponent<InteractiveObject>());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Interactive")
		{
			playerInteraction.possibleInteractions.Remove(collision.gameObject.GetComponent<InteractiveObject>());
		}
	}

}
