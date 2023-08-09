using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    
    /// �{�{�C��
    public Color color = new Color(1, 0, 1, 1);

    /// �̧C�o���G�סA���Ƚd��[0,1]�A�ݤp��̰��o���G�סC
    [Range(0.0f, 1.0f)]
    public float minBrightness = 0.0f;

    /// �̰��o���G�סA���Ƚd��[0,1]�A�ݤj��̧C�o���G�סC
    [Range(0.0f, 1)]
    public float maxBrightness = 0.5f;

    /// �{�{�W�v�A���Ƚd��[0.2,30.0]�C
    [Range(0.2f, 30.0f)]
    public float rate = 1;

    [Tooltip("�Ŀ惡���h?�ʮɦ۰ʶ}�l�{�{")]
    [SerializeField]
    private bool _autoStart = false;

    private float _h, _s, _v;      // ��աA���M�סA�G��
    private float _deltaBrightness;   // �̧C�̰��G�׮t
    private Renderer _renderer;
    private Material _material;
    private readonly string _keyword = "_EMISSION";
    private readonly string _colorName = "_EmissionColor";

    private Coroutine _glinting;

    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _material = _renderer.material;

        if (_autoStart)
        {
            StartGlinting();
        }
    }

    /// <summary>
    /// ����ƾڡA�ëO�ҹB��ɪ��ק����o�����ΡC
    /// �Ӥ�k�u�b�s�边�Ҧ����ͮġI�I�I
    /// </summary>
    private void OnValidate()
    {
        // ����G�׽d��
        if (minBrightness < 0 || minBrightness > 1)
        {
            minBrightness = 0.0f;
            Debug.LogError("�̧C�G�׶W�X���Ƚd��[0, 1]�A�w���m?0�C");
        }
        if (maxBrightness < 0 || maxBrightness > 1)
        {
            maxBrightness = 1.0f;
            Debug.LogError("�̰��G�׶W�X���Ƚd��[0, 1]�A�w���m?1�C");
        }
        if (minBrightness >= maxBrightness)
        {
            minBrightness = 0.0f;
            maxBrightness = 1.0f;
            Debug.LogError("�̧C�G��[MinBrightness]�����C��̰��G��[MaxBrightness]�A�w���O���m?0/1�I");
        }

        // ����{�{�W�v
        if (rate < 0.2f || rate > 30.0f)
        {
            rate = 1;
            Debug.LogError("�{�{�W�v�W�X���Ƚd��[0.2, 30.0]�A�w���m?1.0�C");
        }

        // ��s�G�׮t
        _deltaBrightness = maxBrightness - minBrightness;

        // ��s�C��
        // �`�N����ϥ� _v �A�_�h�b�B��ɭק�ѼƷ|�ɭP�G�׬���
        float tempV = 0;
        Color.RGBToHSV(color, out _h, out _s, out tempV);
    }

    /// <summary>
    /// �}�l�{�{�C
    /// </summary>
    public void StartGlinting()
    {
        _material.EnableKeyword(_keyword);

        if (_glinting != null)
        {
            StopCoroutine(_glinting);
        }
        _glinting = StartCoroutine(IEGlinting());
    }

    /// <summary>
    /// ����{�{�C
    /// </summary>
    public void StopGlinting()
    {
        _material.DisableKeyword(_keyword);

        if (_glinting != null)
        {
            StopCoroutine(_glinting);
        }
    }

    /// <summary>
    /// ����۵o���j�סC
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEGlinting()
    {
        Color.RGBToHSV(color, out _h, out _s, out _v);
        _v = minBrightness;
        _deltaBrightness = maxBrightness - minBrightness;

        bool increase = true;
        while (true)
        {
            if (increase)
            {
                _v += _deltaBrightness * Time.deltaTime * rate;
                increase = _v <= maxBrightness;
            }
            else
            {
                _v -= _deltaBrightness * Time.deltaTime * rate;
                increase = _v <= minBrightness;
            }
            _material.SetColor(_colorName, Color.HSVToRGB(_h, _s, _v));
            //_renderer.UpdateGIMaterials();
            yield return null;
        }
    }
}
