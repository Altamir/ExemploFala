using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Fala
{
    public GameObject falaGO;
    Image falaImag;
    public string nomeFala;

    public void Init()
    {
        if (falaGO != null)
        {
            falaImag = falaGO.GetComponent<Image>();
            falaImag.fillAmount = 0;
            falaGO.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Falta referencia da fala!");
        }
    }

    internal void SetFill(float qt)
    {
        falaImag.fillAmount += qt;
    }

    internal float GetfillAmount()
    {
        return falaImag.fillAmount;
    }
}

[System.Serializable]
public class Dialogo
{
    public Fala[] falas;
    int cur;

    internal void Init()
    {

        for (int i = 0; i < falas.Length; i++)
        {
            falas[i].Init();
        }

    }

    internal Fala GetNextFala()
    {
        if (cur >= falas.Length)
        {
            cur = 0;
        }
        Fala retorno = falas[cur];
        cur++;
        return retorno;
    }
}


public class Dialogos : MonoBehaviour
{
    public Dialogo[] dialogos;
    int cur;
    [SerializeField]
    float velocidade = 1f;

    public void Start()
    {
        cur = 0;
        for (int i = 0; i < dialogos.Length; i++)
        {
            dialogos[i].Init();
        }
    }

    public void Next()
    {
        if (cur >= dialogos.Length)
        {
            cur = 0;
        }
        Fala fala = dialogos[cur].GetNextFala();
        cur++;
        StartCoroutine(PreencheFala(fala));
    }


    IEnumerator PreencheFala(Fala fala)
    {
        fala.falaGO.SetActive(true);
        while (fala.GetfillAmount() < 1f)
        {
            fala.SetFill(velocidade * Time.deltaTime);
            yield return null;
        }
    }
}

