using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;

public class LW_AmmoDisplay : MonoBehaviour
{
    public GameObject ammoIndicator;

    private List<GameObject> _MainContainer = new List<GameObject>();


    private void Awake()
    {
        LW_Weapon.OnFire += UpdateDisplay;

        //TODO (skn): Get a reference from current weapon and pass the
        //            magazine size
        _MainContainer = CreateContainer(4);
    }

    private void OnDestroy()
    {
        LW_Weapon.OnFire += UpdateDisplay;
    }

    private List<GameObject> CreateContainer(int magazineSize)
    {
        List<GameObject> ammoContainer = new List<GameObject>();

        float xStart = magazineSize / 2;
        xStart *= -1;

        for(int i = 0; i < magazineSize; i++)
        {
            GameObject na = Instantiate(ammoIndicator, this.transform);

            float xFinal = (xStart + i) * na.transform.localScale.x;
            na.transform.localPosition = new Vector3(xFinal, 0, 0);

            ammoContainer.Add(na);
        }

        return ammoContainer;
    }

    private void UpdateDisplay(int bulletsAmount)
    {
        int i = _MainContainer.Count - bulletsAmount;

        _MainContainer[i].SetActive(false);
    }
}
