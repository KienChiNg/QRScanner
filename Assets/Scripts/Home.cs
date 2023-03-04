using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Home : MonoBehaviour
{
    public static Home Instance;
    [SerializeField] private TMP_Text noticeTxt;
    [SerializeField] private Canvas canvas;

    public TMP_Text NoticeTxt { get => noticeTxt; set => noticeTxt = value; }
    public Canvas Canvas { get => canvas; set => canvas = value; }

    void Awake()
    {
        if(Instance){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextSceneQR(){
        canvas.gameObject.SetActive(false);
        noticeTxt.text = "QR";
        SceneManager.LoadScene("QR");
    }
}
