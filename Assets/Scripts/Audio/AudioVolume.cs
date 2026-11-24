using UnityEngine;
using UnityEngine.Audio;

public static class AudioVolume
{
    // Suggestion: Constantes en camelCase; en VolumeStepper y PlayerAnimator usás PascalCase (Step, ArmedOffset). Unificá el criterio.
    private const float defaultVolume = .1f;
    private const float minVolume = .0001f;
    
    public static float GetSaved(string parameter)
    {
        return PlayerPrefs.GetFloat(parameter, defaultVolume);
    }

    public static void Apply(AudioMixer mixer, string parameter, float linearVolume)
    {
        float safeVolume = Mathf.Max(linearVolume, minVolume);
        mixer.SetFloat(parameter, Mathf.Log10(safeVolume) * 20f);
    }

    public static void Save(string parameter, float linearVolume)
    {
        PlayerPrefs.SetFloat(parameter, linearVolume);
    }
}
