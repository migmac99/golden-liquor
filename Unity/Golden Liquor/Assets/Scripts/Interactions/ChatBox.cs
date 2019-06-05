using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour {

    public Text TextField;
    [Space (10)]
    public string TextContent;

    private Animator ChatAnimator;

    void Start () {
        ChatAnimator = TextField.transform.parent.GetComponent<Animator> ();
    }

    public void Show (bool show = true) {
        TextField.text = TextContent;
        ChatAnimator.SetBool ("Open", show);
    }

}