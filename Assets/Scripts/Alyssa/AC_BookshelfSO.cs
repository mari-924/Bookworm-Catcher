using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu()]
public class AC_BookshelfSO : ScriptableObject
{
    [SerializeField] public Sprite spriteBase;
    [SerializeField] public List<Sprite> bookshelfEatenSprites;
}
