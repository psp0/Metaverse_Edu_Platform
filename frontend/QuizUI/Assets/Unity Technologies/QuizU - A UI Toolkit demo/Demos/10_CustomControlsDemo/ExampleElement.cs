using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace Demos
{
    /// <summary>
    /// To create a custom control:
    ///
    /// -Add the UxmlElement attribute to the custom control class definition.
    /// -Declare the custom control class as a partial class.
    /// -Inherit it from VisualElement or one of its derived classes.
    ///
    /// Custom controls are usable in UXML and UI Builder after you create them. By default, the custom control appears in
    /// the Library tab in UI Builder. If you want to hide it from the Library tab, add the HideInInspector attribute.
    /// 
    /// The following example creates a custom control with multiple custom attributes.
    /// </summary>
    [UxmlElement]
    public partial class ExampleElement: VisualElement
    {
        [UxmlAttribute(name:"my-text")]
        public string myStringValue { get; set; }

        [UxmlAttribute]
        public int myIntValue { get; set; }

        [UxmlAttribute]
        public float myFloatValue { get; set; }

        [UxmlAttribute]
        public List<int> myListOfInts { get; set; }

        // A Type attribute that can be set via UXML. It references a specific type derived from VisualElement.
        // Example usage in UXML: myType="UnityEngine.UIElements.Button"
        [UxmlAttribute, UxmlTypeReference(typeof(VisualElement))]
        public Type myType { get; set; }

        [UxmlAttribute]
        public Texture2D myTexture { get; set; }

        [UxmlAttribute]
        public Color myColor { get; set; }

        // Constructor
        public ExampleElement()
        {
            
        }
    }
}

