using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{

    public Text input;

    public Button button;

    public Text result;

    [DllImport("NATIVECPPLIBRARY", EntryPoint = "displayNumber")]
    public static extern int displayNumber();
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "getRandom")]
    public static extern int getRandom();
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "displaySum")]
    public static extern int displaySum();





    [DllImport("NATIVECPPLIBRARY", EntryPoint = "reverseConstString", CallingConvention = CallingConvention.StdCall)]
    public static extern string reverseConstString(string str);
    [DllImport("NATIVECPPLIBRARY", EntryPoint = "reverseConstString", CallingConvention = CallingConvention.StdCall)]
    public static extern string reverseConstString2(int[] str);

    [DllImport("NATIVECPPLIBRARY", EntryPoint = "reverseConstString", CallingConvention = CallingConvention.StdCall)]
    public static extern string reverseConstString3(char[] str);



    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(reverse_event);


        print(displayNumber());
        print(getRandom());
        print(displaySum());


        var input = "Holi!";
        Debug.Log(input);
        Debug.Log(reverseConstString(input));


         input = "Holi!ñee";

        Encoding ascii = Encoding.ASCII;
        Encoding unicode = Encoding.Unicode;


        Debug.Log(input);
       
       // string result = reverseConstString2(array2);
        string result2 = reverseConstString3(input.ToCharArray());


     
        Debug.Log(result2);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void reverse_event()
    {
        if (!string.IsNullOrEmpty(input.text))
        result.text =  reverseConstString(  input.text);
        else{
            result.text = "";
        }
    }
}
