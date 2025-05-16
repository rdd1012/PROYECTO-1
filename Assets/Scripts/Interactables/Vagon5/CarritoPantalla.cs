using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarritoPantalla : MonoBehaviour {
    [SerializeField] ObjetosCarrito[] objects;
    [SerializeField] SlotsCarrito[] slots;
    [SerializeField] Image sp;
    [SerializeField] Sprite cortina;
    [SerializeField] Image comidaLoroImage;
    public bool IsPuzzleComplete { get; set; }
    void Start()
    {
        List<Transform> slotTransforms = new List<Transform>();
        foreach (SlotsCarrito slot in slots) slotTransforms.Add(slot.transform);

        do
        {
            Shuffle(slotTransforms);
        } while (!IsValidArrangement(slotTransforms));

        AssignObjects(slotTransforms);
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }
    public void CompletarPuzzle()
    {
        StartCoroutine(PuzzleCompleto());
    }
    public IEnumerator PuzzleCompleto() 
    {
        foreach (ObjetosCarrito _objects in objects) 
        {
            _objects.gameObject.SetActive(false);
        }
        
        foreach (SlotsCarrito _slots in slots)
        {
            _slots.gameObject.SetActive(false);
        }
        
        Sprite normal = sp.sprite;
        sp.sprite = cortina;
        yield return new WaitForSecondsRealtime(1f);
        sp.sprite = normal;
        comidaLoroImage.gameObject.SetActive(true);
    }
    bool IsValidArrangement(List<Transform> shuffledSlots)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (shuffledSlots[i].GetComponent<SlotsCarrito>().CorrectObject == objects[i].gameObject)
                return false;
        }
        return true;
    }

    void AssignObjects(List<Transform> slotTransforms)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].ParentToReturnTo = slotTransforms[i];
            objects[i].transform.SetParent(slotTransforms[i]);
            objects[i].transform.localPosition = Vector3.zero;
        }
    }
}
