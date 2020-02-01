using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using TMPro;

public class CombatScript : MonoBehaviour
{
    public GameObject onFightUI;
    public GameObject avatarPos;
    public Image slider;
    public Slider handleSlider;
    public PostProcessVolume volume;
    private ColorGrading colorGrading;

    public CanvasGroup newChallengerCanvasGroup;
    public Animator newChallengerAnimator;

    public TextMeshProUGUI challengerNameLifeText;
    public TextMeshProUGUI challengerNameText;
    public TextMeshProUGUI challengerFlavorText;

    public Image challengerImage;

    private void Start()
    {
        
        slider.gameObject.SetActive(false);
        onFightUI.gameObject.SetActive(false);
    }


    public void StartFight(string challengerName, string challengerFlavor, Sprite spriteToDisplay, bool changePosition)
    {
        slider.gameObject.SetActive(false);
        onFightUI.gameObject.SetActive(false);

        challengerNameLifeText.text = challengerName;
        challengerNameText.text = challengerName;
        challengerFlavorText.text = challengerFlavor;

        challengerImage.sprite = spriteToDisplay;

        volume.profile.TryGetSettings<ColorGrading>(out colorGrading);

        colorGrading.postExposure.value = 0;
        DOTween.To(() => colorGrading.postExposure.value, x => colorGrading.postExposure.value = x, 10, 1.5f)
            .OnComplete(() => FighterPresentation(changePosition));
    }

    private void FighterPresentation(bool changePosition)
    {
        newChallengerCanvasGroup.alpha = 0;
        newChallengerCanvasGroup.DOFade(1, 0.5f);
        newChallengerAnimator.Play(0);
        if (changePosition == true)
            avatarPos.transform.position += new Vector3(-5, 0, 0);
        DOVirtual.DelayedCall(5f, PlayFight);
    }

    private void PlayFight()
    {
        newChallengerCanvasGroup.DOFade(0, 0.3f);
        DOTween.To(() => colorGrading.postExposure.value, x => colorGrading.postExposure.value = x, 0, 0.3f).SetDelay(0.3f)
            .OnComplete(() =>
            {
                MovementCharacter.Instance.inFight = true;
                onFightUI.gameObject.SetActive(true);
                slider.gameObject.SetActive(true);
            });
    }
}
