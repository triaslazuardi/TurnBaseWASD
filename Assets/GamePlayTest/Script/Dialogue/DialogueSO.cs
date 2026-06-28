using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nitzz.Dialogue
{
    [CreateAssetMenu(menuName = "SO/Dialogue")]
    public class DialogueSO : ScriptableObject
    {
        public DialogueLine[] lines;
    }

    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;

        [TextArea(2, 5)]
        public string text;
    }
}