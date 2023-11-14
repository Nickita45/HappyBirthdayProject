using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicalGame : MonoBehaviour
{
    public enum Key
    {
        A,
        W,
        S,
        D
    }

    private bool _running = false;

    [SerializeField]
    private GameObject _arrowLeft, _arrowUp, _arrowDown, _arrowRight;

    // cringe
    GameObject GetArrow(int index)
    {
        switch (index)
        {
            case 0:
                return _arrowLeft;
            case 1:
                return _arrowUp;
            case 2:
                return _arrowDown;
            case 3:
                return _arrowRight;
            default:
                return null;
        }
    }

    private List<GameObject> _incomingArrows = new();
    private float _t = 0f;
    private float _delayTimer = 2f;

    public GameObject fallingArrowPrefab;

    private float _fallingSpeed = 10f;
    private float _fallTimer = 4f;
    private float _difficultyIncrease = 0.1f;

    private int amount = 1;

    void Update()
    {
        if (_running)
        {
            _t += Time.deltaTime;
            if (_t >= _fallTimer)
            {
                _t = 0;

                for (int i = 0; i < amount; i++)
                {
                    int index = Random.Range(0, 4);
                    GameObject arrow = GetArrow(index);
                    Vector3 position = arrow.transform.position - Vector3.down * 150f;

                    GameObject _arrow = Instantiate(fallingArrowPrefab, transform);
                    _arrow.transform.position = position;
                    _arrow.transform.rotation = arrow.transform.rotation;

                    _incomingArrows.Add(_arrow);
                }

                _fallingSpeed += _difficultyIncrease;
                _fallTimer -= _difficultyIncrease * 0.05f;
                if (_fallTimer < 0.2f) _fallTimer = 0.2f;
            }

            _delayTimer += Time.deltaTime;

            List<GameObject> toRemove = new();
            foreach (GameObject _arrow in _incomingArrows)
            {
                _arrow.transform.Translate(Vector3.down * Time.deltaTime * _fallingSpeed, Space.World);
                if (_arrowLeft.transform.position.y - _arrow.transform.position.y > 10f)
                {
                    toRemove.Add(_arrow);
                }
            }
            foreach (GameObject _arrow in toRemove)
            {
                StartCoroutine(AnimateArrowFailure(_arrow));
                _total++;
                _incomingArrows.Remove(_arrow);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_delayTimer > _delayTimerMax) ActivateArrow(_arrowUp);
                else _buffer = _arrowUp;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (_delayTimer > _delayTimerMax) ActivateArrow(_arrowDown);
                else _buffer = _arrowDown;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_delayTimer > _delayTimerMax) ActivateArrow(_arrowLeft);
                else _buffer = _arrowLeft;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_delayTimer > _delayTimerMax) ActivateArrow(_arrowRight);
                else _buffer = _arrowRight;
            }

            if (_delayTimer > _delayTimerMax && _buffer != null) ActivateArrow(_buffer);
        }
    }

    private float _animMaxScale = 1.2f;
    private float _animSpeed = 1f;

    private int _points;
    public int Points { get { return _points; } set { _points = value; _pointsText.text = _points.ToString(); } }
    public TextMeshProUGUI _pointsText;

    private int _total = 0;
    private int _correct = 0;
    public float Accuracy { get { return _correct / ((float)_total); } }

    private GameObject _buffer = null;

    private int _pointsForCorrectArrow = 100;
    private int _pointsForPerfectArrow = 500;

    public void Start()
    {
        Points = 0;
    }

    public void ActivateArrow(GameObject arrow)
    {
        List<GameObject> toActivate = new();
        foreach (GameObject fallingArrow in _incomingArrows)
        {
            float dist = Vector3.Distance(arrow.transform.position, fallingArrow.transform.position);
            if (dist < 10f)
            {
                if (QuaternionsEqual(arrow.transform.rotation, fallingArrow.transform.rotation))
                {
                    toActivate.Add(fallingArrow);
                }
            }
        }

        foreach (GameObject fallingArrow in toActivate)
        {
            _incomingArrows.Remove(fallingArrow);

            float dist = Vector3.Distance(arrow.transform.position, fallingArrow.transform.position);
            if (dist < 2.5f)
            {
                StartCoroutine(AnimateArrowCorrect(fallingArrow, _textPerfect));
                Points += _pointsForPerfectArrow;
            }
            else
            {
                StartCoroutine(AnimateArrowCorrect(fallingArrow, _textCorrect));
                Points += _pointsForCorrectArrow;
            }

            _correct++;
            _total++;

            Destroy(fallingArrow);
            _pointsText.text = _points.ToString();
        }

        _delayTimer = 0f;
        _buffer = null;
        StartCoroutine(AnimateArrow(arrow));
    }

    public bool QuaternionsEqual(Quaternion q1, Quaternion q2)
    {
        return q1.Equals(q2) || (q1 == q2);
    }

    public GameObject _textCorrect, _textPerfect;

    public GameObject _failureEffect;

    private float _delayTimerMax = 0.25f;

    public IEnumerator AnimateArrowCorrect(GameObject fallingArrow, GameObject _textPrefab)
    {
        GameObject _text = Instantiate(_textPrefab, fallingArrow.transform.position, Quaternion.identity, transform);
        if (_character == Character.Rengoku) _attackEffectRengoku.Play();
        else _attackEffectLiza.Play();

        TextMeshProUGUI _textUGUI = _text.GetComponent<TextMeshProUGUI>();
        _textUGUI.CrossFadeAlpha(0f, 2f, false);

        Destroy(fallingArrow);

        float t = 0;
        while (t < 2.1f)
        {
            _text.transform.localScale += Time.deltaTime * Vector3.one * _animSpeed / 2;
            _text.transform.Translate(Vector3.right * 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(_text);
        Destroy(_textUGUI);
    }

    public GameObject _textFailure;
    public ParticleSystem _attackEffectRengoku;
    public ParticleSystem _attackEffectLiza;

    public IEnumerator AnimateArrowFailure(GameObject fallingArrow)
    {
        GameObject _text = Instantiate(_textFailure, fallingArrow.transform.position, Quaternion.identity, transform);
        GameObject _effect = Instantiate(_failureEffect, fallingArrow.transform.position, Quaternion.identity, transform);
        _effect.transform.localScale = Vector3.one * 150f;

        TextMeshProUGUI _textUGUI = _text.GetComponent<TextMeshProUGUI>();
        _textUGUI.CrossFadeAlpha(0f, 2f, false);

        Destroy(fallingArrow);

        float t = 0;
        while (t < 2.1f)
        {
            _text.transform.localScale += Time.deltaTime * Vector3.one * _animSpeed / 2;
            _text.transform.Translate(Vector3.right * 5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(_text);
        Destroy(_textUGUI);
    }

    public IEnumerator AnimateArrow(GameObject arrow)
    {
        float scale = 1.0f;
        while (true)
        {
            scale += Time.deltaTime * _animSpeed;
            arrow.transform.localScale = Vector3.one * scale;
            if (scale >= _animMaxScale)
            {
                scale = _animMaxScale;
                arrow.transform.localScale = Vector3.one * scale;
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        while (true)
        {
            scale -= Time.deltaTime * _animSpeed;
            arrow.transform.localScale = Vector3.one * scale;
            if (scale < 1f)
            {
                scale = 1f;
                arrow.transform.localScale = Vector3.one * scale;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public AudioClip phase1, phase2, phase3, phase4;

    public void StartPhase1()
    {
        _fallingSpeed = 10f;
        _fallTimer = 4f;
        _difficultyIncrease = 0.2f;
        NextPhase();
        GameManager.Instance.textPlayers.PlayAudio(phase1);
        StartCoroutine(FinishPhaseAfterSeconds(phase1.length));
    }

    enum Character
    {
        Rengoku,
        Liza
    }

    private Character _character = Character.Rengoku;

    Color HexToColor(string hex)
    {
        Color color = new();
        UnityEngine.ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public void StartPhase2()
    {
        _character = Character.Liza;

        // Iterate over all children of the given GameObject
        foreach (Transform child in _arrowLeft.transform.parent)
        {
            // Get the Image component of the current child
            Image childImage = child.GetComponent<Image>();

            // Set the Color of the current child to #2342FF
            if (childImage != null)
            {
                childImage.color = HexToColor("#2342FF");

                // For the first child of C, get its Image component and set the Color to #FE70FF
                if (child.childCount > 0)
                {
                    Transform firstGrandchild = child.GetChild(0);
                    Image firstGrandchildImage = firstGrandchild.GetComponent<Image>();

                    if (firstGrandchildImage != null)
                    {
                        firstGrandchildImage.color = HexToColor("#FE70FF");
                    }
                }
            }
        }

        foreach (Image image in _toMakePink)
        {
            image.color = HexToColor("#2342FF");
        }

        foreach (TextMeshProUGUI text in _toMakePinkText)
        {
            text.color = HexToColor("#2342FF");
        }

        _akaza.GetComponent<Image>().color = HexToColor("5879FF");

        _attackEffectLiza.gameObject.SetActive(true);

        _fallingSpeed = 15f;
        _fallTimer = 3f;
        _difficultyIncrease = 0.25f;
        NextPhase();
        GameManager.Instance.textPlayers.PlayAudio(phase2);
        StartCoroutine(FinishPhaseAfterSeconds(phase2.length));
    }

    public Image[] _toMakePink;
    public TextMeshProUGUI[] _toMakePinkText;

    public void StartPhase3()
    {
        _fallingSpeed = 15f;
        _fallTimer = 2.5f;
        _difficultyIncrease = 0.35f;
        NextPhase();
        GameManager.Instance.textPlayers.PlayAudio(phase3);
        StartCoroutine(FinishPhaseAfterSeconds(phase3.length));
    }

    public void StartPhase4()
    {
        _fallingSpeed = 4f;
        _fallTimer = 4f;
        _difficultyIncrease = 0.0f;
        amount = 3;
        NextPhase();
        GameManager.Instance.textPlayers.PlayAudio(phase4);
        StartCoroutine(EndPhase4());
        StartCoroutine(FinishPhaseAfterSeconds(phase4.length));
    }

    public IEnumerator EndPhase4()
    {
        yield return new WaitForSeconds(1f);
        _fallTimer = 0.5f;
        _fallingSpeed = 50f;
        _delayTimer = 0f;
        _delayTimerMax = -1f;
    }

    public AkazaMove _akaza;
    public void NextPhase()
    {
        _running = true;
        GameManager.Instance.dialogue.SetActive(false);
        _akaza.StartMove();
        gameObject.SetActive(true);
    }

    public IEnumerator FinishPhaseAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        FinishPhase();
    }

    public void FinishPhase()
    {
        _running = false;
        foreach (GameObject arrow in _incomingArrows)
        {
            Destroy(arrow);
        }
        gameObject.SetActive(false);
        _incomingArrows.Clear();
        GameManager.Instance.dialogue.SetActive(true);
        GameManager.Instance.textPlayers.PlayNextText();
    }
}
