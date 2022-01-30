using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        switch (tag)
        {
            case "volslider":
                slider = this.GetComponent<Slider>();
                slider.value = PlayerPrefs.GetFloat("Volume");
                break;
            case "mouseslider":
                slider = this.GetComponent<Slider>();
                slider.value = PlayerPrefs.GetFloat("mouseSens");
                break;
        }
        
    }

    public void MouseSensChange(float value)
    {
        PlayerPrefs.SetFloat("mouseSens", value);
        PlayerPrefs.Save();
    }

    public void SoundChange(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
}

