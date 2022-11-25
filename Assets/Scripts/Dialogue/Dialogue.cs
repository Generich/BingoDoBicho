using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Dialogo", menuName = "Dialogo/Conversa")]

public class Dialogue : ScriptableObject{
    public DialogueTexts[] Text;
}

[System.Serializable]
public class DialogueTexts{   

    public Character character;
    
    [TextArea]
    public string[] DialogueText;  
}
