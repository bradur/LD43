// Date   : 02.12.2018 15:18
// Project: LD43
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPopup : MonoBehaviour {

    [SerializeField]
    private Text txtTitle;


    [SerializeField]
    private Text txtMessage;

    [SerializeField]
    private Text txtInfo;


    public void Init(string title, string message, string info) {
        txtTitle.text = title;
        txtMessage.text = message;
        txtInfo.text = info;
    }

    public void Kill() {
        Destroy(gameObject);
    }

}
