using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField]
    TextMeshProUGUI _text;

    private void Awake()
    {
        if (UIManager._instance != null)
        {
            Destroy(this.gameObject);
        }

        UIManager._instance = this;


        this.Init();
    }

    private void OnValidate()
    {
        _text = Util.FindChildWithPath<TextMeshProUGUI>("@Canvas/Text");
    }

    private void Reset()
    {
        _text = Util.FindChildWithPath<TextMeshProUGUI>("@Canvas/Text");
    }

    void Init()
    {
        _text = Util.FindChildWithPath<TextMeshProUGUI>("@Canvas/Text");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
