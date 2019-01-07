using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleWarrior.Weapon;

public class LW_AmmoDisplay : MonoBehaviour
{
    public GameObject ammoIndicator;

    //TODO (skn): Find a way to detect the weapon automatically
    public LW_Weapon _CurrentWeapon = null;
    private List<GameObject> _MainContainer = new List<GameObject>();


    private void Awake()
    {
        LW_Weapon.OnFire += UpdateDisplay;
        
        _MainContainer = CreateContainer(_CurrentWeapon.bulletsPerMag);
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
