using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _slide;
    [SerializeField] private TextMeshProUGUI _txtbullet;
    [SerializeField] private TextMeshProUGUI _txtHp;
    [SerializeField] private TextMeshProUGUI _txtReload;
    [SerializeField] private TextMeshProUGUI _txtEndGame;
    [SerializeField] private GameObject UIEndGame;
    [SerializeField] private GameObject UiStatGame;
    private PlayerController _player;
    private SpawnBlullet _spawnBlullet;
    private EnemyPhase1 _phase1;
    private DropAndPickup _imgBullet;
    private SpawnBlullet _reloadBullet;
    private Boss _boss;
    private void Start()
    {
        _player = GamaManager.Instance.Player;
        _spawnBlullet = GamaManager.Instance.SpawnBlullet;
        _phase1 = GamaManager.Instance.Phase1;
        _imgBullet = GamaManager.Instance.ScripPickUpGun;
        _reloadBullet = GamaManager.Instance.SpawnBlullet;
        _boss = GamaManager.Instance.Boss;
        UIEndGame.SetActive(false);
        UiStatGame.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    private void FixedUpdate()
    {
        _UpdateHP();
        _UpdateGun();
        GamaManager.Instance.SetActiveImgKey(_phase1.IsKey);
        GamaManager.Instance.SetActiveImgBullet(_imgBullet.isEpuip);
        if (!_reloadBullet._fire)
        {
            _txtReload.enabled = true;
        }
        else
        {
            _txtReload.enabled = false;
        }
        /*if (_player._isWin == 1)
        {
            //showPopup
            _txtEndGame.SetText("You Win");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            UIEndGame.SetActive(true);
        }
        else if (_player._isWin == 0)
        {
            _txtEndGame.SetText("You Lose");
            Cursor.visible = false; 
            Cursor.lockState = CursorLockMode.None;
            UIEndGame.SetActive(true);
        }*/

        if (GamaManager.Instance.IsEndGame)
        {
            if (_player._isWin) _txtEndGame.SetText("You Win");
            else _txtEndGame.SetText("You Lose");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            UIEndGame.SetActive(true);
        }
    }
    public void _UpdateGun()
    {
        _txtbullet.SetText(_spawnBlullet.QualityBullet + " / " + _player.QualityBullet); 
    }
    public void _UpdateHP()
    {
        if (_slide == null) return;
        _slide.value = _player.HP/GamaManager.Instance.HpMax * 100;
        _txtHp.SetText(_player.HP +" / " + GamaManager.Instance.HpMax);
    }


    public void _btnResetGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void _btnQuit()
    {
        Application.Quit();
    }

    public void _btnStartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        UiStatGame.SetActive(false);
    }
}
