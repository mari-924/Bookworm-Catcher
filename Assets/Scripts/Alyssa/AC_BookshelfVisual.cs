using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AC_BookshelfVisual : MonoBehaviour
{
    [SerializeField] private AC_BookshelfListSO bookshelfListSO;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private int minRange;
    [SerializeField] private int maxRange;
    private List<Sprite> bookshelfEatenState;
    private State state;
    private int wormTouchCount;


    private enum State
    {
        Full,
        Half,
        Quarter,
        Empty
    }

    
    
    public void Awake()
    {
        AC_Bookshelf.OnCollisionWormBookshelf += OnCollisionWormBookshelf;
    }
    

    public void Start()
    {
        SelectRandomBookshelf();
        wormTouchCount = 0;
    }
    

    private void SelectRandomBookshelf()
    {
        //randomizes bookshelf base for middle section
        AC_BookshelfSO selectedBookshelf = bookshelfListSO.bookshelfSOList[UnityEngine.Random.Range(minRange, maxRange)];

        //gets sprite base from SO
        selectedSprite = selectedBookshelf.spriteBase;
        gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;

        bookshelfEatenState = selectedBookshelf.bookshelfEatenSprites;

        // Debug.Log(selectedSprite);
    }


    public void OnCollisionWormBookshelf(SpriteRenderer spriteRender)
    {
        //makes sure only the prefab touching the worm is changed 
        if (spriteRender != this.gameObject.GetComponent<SpriteRenderer>())
            return;
        
        // Debug.Log("change sprite");
        //changes sprite after a certain amount of collisions with worm
        //sprites should change to be slowly eaten books; should match with randomized selected sprite from start 
        switch (state)
        {
            case State.Full:
                if (wormTouchCount > UnityEngine.Random.Range(3, 6))
                {
                    wormTouchCount = 0;
                    state = State.Half;
                }
                wormTouchCount++;
                break;
            case State.Half:
                selectedSprite = bookshelfEatenState[0];
                if (wormTouchCount > UnityEngine.Random.Range(3, 6))
                {
                    wormTouchCount = 0;
                    state = State.Quarter;
                }
                wormTouchCount++;
                break;
            case State.Quarter:
                selectedSprite = bookshelfEatenState[1];
                if (wormTouchCount > UnityEngine.Random.Range(3, 6))
                {
                    wormTouchCount = 0;
                    state = State.Empty;
                }
                wormTouchCount++;
                break;
            case State.Empty:
                selectedSprite = bookshelfEatenState[2];
                break;
        }

        spriteRender.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        // Debug.Log(selectedSprite);
    }
}
