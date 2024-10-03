using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Niantic.Lightship.AR.ARFoundation;

public class SemanticsManager : MonoBehaviour
{

    public Niantic.Lightship.AR.Semantics.ARSemanticSegmentationManager _semanticMan;
    public TMP_Text _text;
    public UnityEngine.UI.RawImage _image;

    bool _semanticsFound = false;
    private Matrix4x4 material;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_semanticMan.subsystem.running)
        {
            return;
        }

        var listSemantics = _semanticMan.GetChannelNamesAt(
            Screen.width / 2, 
            Screen.height / 2);
        _text.text = ""; // clear the text
        foreach (var semantic in listSemantics)
            _text.text += semantic; // set the text if a semantic was found

        // highlight the class, if found
        if (listSemantics.Count > 0)
        {
            // just show the first one
            _image.texture = _semanticMan.GetSemanticChannelTexture(listSemantics[0], out material);
        }
    }
}
