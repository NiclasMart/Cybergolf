using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class addButtonSounds : MonoBehaviour
{
    // adds all sound files to UI
    void Start()
    {
        AudioManager manager = AudioManager.instance;
        Button button = GetComponent<Button>();

        button.onClick.AddListener(delegate () { manager.Play("btnClick"); });
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null) {
            trigger = gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventDate) => { manager.Play("btnHover"); });

        trigger.triggers.Add(entry);
    }

    
}
