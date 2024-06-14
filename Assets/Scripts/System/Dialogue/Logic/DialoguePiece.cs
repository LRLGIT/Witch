using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePiece 
{
    public string ID;
    public Sprite imageL;
    public Sprite imageR;
    public string Name;
    public bool END = false;
    public int num=0;
    [TextArea]
    public string text;
    public QuestData_So quest;
    public ItemData_So item;
    public List<DialogueOption> options = new List<DialogueOption>();
}
