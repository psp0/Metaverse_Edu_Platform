using UnityEngine;
using UnityEngine.UIElements;

namespace Demos
{
    // Derives from BaseField<bool> base class. Represents a container for its input part.
    [UxmlElement]
    public partial class SlideToggle : BaseField<bool>
    {
        // In keeping with the BEM standard, the SlideToggle has its own block class and two element classes. It also
        // has a class that represents the enabled state of the toggle.
        public static readonly new string ussClassName = "slide-toggle";
        public static readonly new string inputUssClassName = "slide-toggle__input";
        public static readonly string inputKnobUssClassName = "slide-toggle__input-knob";
        public static readonly string inputCheckedUssClassName = "slide-toggle__input--checked";

        VisualElement m_Input;
        VisualElement m_Knob;

        // UxmlAttributes for customizable "enabled" and "disabled" text
        [UxmlAttribute]
        public string EnabledText { get; set; } = "Enabled";

        [UxmlAttribute]
        public string DisabledText { get; set; } = "Disabled";
        
        // UxmlAttributes for background colors
        [UxmlAttribute] 
        public Color EnabledBackgroundColor { get; set; } = new Color(0f, 0.5f, 0.85f,1f);

        [UxmlAttribute]
        public Color DisabledBackgroundColor { get; set; } = Color.gray;

        
        // Custom controls need a default constructor. This default constructor calls the other constructor in this
        // class. Pass a non-null string to make sure the Label is created.
        public SlideToggle() : this(" ") { }

        // This constructor allows users to set the contents of the label.
        public SlideToggle(string label) : base(label, new VisualElement())
        {
            // Style the control overall.
            AddToClassList(ussClassName);

            // Get the BaseField's visual input element and use it as the background of the slide.
            m_Input = this.Q(className: BaseField<bool>.inputUssClassName);
            m_Input.AddToClassList(inputUssClassName);
            m_Input.name = "input";

            // Create a "knob" child element for the background to represent the actual slide of the toggle.
            m_Knob = new();
            m_Knob.AddToClassList(inputKnobUssClassName);
            m_Knob.name = "knob";
            m_Input.Add(m_Knob);
            
            labelElement.name = " label";
            
            labelElement.text = (value) ? "enabled" : "disabled";

            // There are three main ways to activate or deactivate the SlideToggle. All three event handlers use the
            // static function pattern described in the Custom control best practices.

            // ClickEvent fires when a sequence of pointer down and pointer up actions occurs.
            RegisterCallback<ClickEvent>(evt => OnClick(evt));
            // KeydownEvent fires when the field has focus and a user presses a key.
            RegisterCallback<KeyDownEvent>(evt => OnKeydownEvent(evt));
            // NavigationSubmitEvent detects input from keyboards, gamepads, or other devices at runtime.
            RegisterCallback<NavigationSubmitEvent>(evt => OnSubmit(evt));
            
            SetValueWithoutNotify(true);
        }

        static void OnClick(ClickEvent evt)
        {
            var slideToggle = evt.currentTarget as SlideToggle;
            slideToggle.ToggleValue();

            evt.StopPropagation();
        }

        static void OnSubmit(NavigationSubmitEvent evt)
        {
            var slideToggle = evt.currentTarget as SlideToggle;
            slideToggle.ToggleValue();

            evt.StopPropagation();
        }

        static void OnKeydownEvent(KeyDownEvent evt)
        {
            var slideToggle = evt.currentTarget as SlideToggle;

            // NavigationSubmitEvent event already covers keydown events at runtime, so this method shouldn't handle
            // them.
            if (slideToggle.panel?.contextType == ContextType.Player)
                return;

            // Toggle the value only when the user presses Enter, Return, or Space.
            if (evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space)
            {
                slideToggle.ToggleValue();
                evt.StopPropagation();
            }
        }

        // All three callbacks call this method.
        void ToggleValue()
        {
            value = !value;
            
        }

        // Because ToggleValue() sets the value property, the BaseField class dispatches a ChangeEvent. This results in a
        // call to SetValueWithoutNotify(). We can use this to change the visual state of the element without another ChangeEvent. 
        //
        // This example uses it to style the toggle based on whether it's currently
        // enabled.
        public override void SetValueWithoutNotify(bool newValue)
        {
            base.SetValueWithoutNotify(newValue);

            //This line of code styles the input element to look enabled or disabled.
            m_Input.EnableInClassList(inputCheckedUssClassName, newValue);
            
            m_Input.style.backgroundColor = newValue ? EnabledBackgroundColor : DisabledBackgroundColor;
            labelElement.text = (value) ? EnabledText : DisabledText;
        }
    }
}