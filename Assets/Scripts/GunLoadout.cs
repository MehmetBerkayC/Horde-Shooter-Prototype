using UnityEngine;

public class GunLoadout : MonoBehaviour
{
    private GunDataSO[] _equippedGunsList;

    public GunLoadout(GunDataSO defaultGun)
    {
        _equippedGunsList[0] = defaultGun;
    }

    public GunDataSO[] GetEquippedGuns()
    {
        return _equippedGunsList;
    }

    // Need another addgun method to switch weapons
    public void AddGunToList(GunDataSO gunToEquip)
    {
        for (int i = 0; i < _equippedGunsList.Length; i++)
        {
            if (_equippedGunsList[i] == null)
            {
                _equippedGunsList[i] = gunToEquip;
                return;
            }
        }
        Debug.Log("All slots are full: " + _equippedGunsList.ToString());
    }

    // Make it so that removing based on index is possible
    public void RemoveGunFromList(GunDataSO gunToRemove)
    {
        for (int i = 0; i < _equippedGunsList.Length; i++)
        {
            if (gunToRemove == _equippedGunsList[i])
            {
                _equippedGunsList[i] = null;
            }
        }
    }

    public void SwitchGunsOnIndex(int index, GunDataSO gunToSwitch)
    {
        _equippedGunsList.SetValue(gunToSwitch, index);
    }
}
