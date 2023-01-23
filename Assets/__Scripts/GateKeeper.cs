using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour {
    private const int               lockedR = 95;
    private const int               lockedUR = 81;
    private const int               lockedUL = 80;
    private const int               lockedL = 100;
    private const int               lockedDL = 101;
    private const int               lockedDR = 102;

    private const int               openR = 48;
    private const int               openUR = 93;
    private const int               openUL = 92;
    private const int               openL = 51;
    private const int               openDL = 26;
    private const int               openDR = 27;

    private IKeyMaster      keys;

    private void Awake() {
        keys = GetComponent<IKeyMaster>();
    }

    private void OnCollisionStay(Collision other) {
        if ( keys.keyCount < 1) return;

        Tile ti = other.gameObject.GetComponent<Tile>();
        if ( ti == null ) return;

        int facing = keys.GetFacing();
        Tile ti2;
        switch (ti.tileNum) {
            case lockedR:
                if (facing != 0) return;
                ti.SetTile( ti.x, ti.y, openR );
                break;
            
            case lockedUR:
                if (facing != 1) return;
                ti.SetTile( ti.x, ti.y, openUR );
                ti2 = TileCamera.TILES[ ti.x - 1, ti.y ];
                ti2.SetTile( ti2.x, ti2.y, openUL );
                break;

            case lockedUL:
                if (facing != 1) return;
                ti.SetTile( ti.x, ti.y, openUL );
                ti2 = TileCamera.TILES[ ti.x + 1, ti.y ];
                ti2.SetTile( ti2.x, ti2.y, openUR );
                break;

            case lockedL:
                if (facing != 2) return;
                ti.SetTile( ti.x, ti.y, openL );
                break;
            
            case lockedDL:
                if (facing != 3) return;
                ti.SetTile( ti.x, ti.y, openDL );
                ti2 = TileCamera.TILES[ ti.x + 1, ti.y ];
                ti2.SetTile( ti2.x, ti2.y, openDR );
                break;

            case lockedDR:
                if (facing != 3) return;
                ti.SetTile( ti.x, ti.y, openDR );
                ti2 = TileCamera.TILES[ ti.x - 1, ti.y ];
                ti2.SetTile( ti2.x, ti2.y, openDL );
                break;
            
            default:
                return;
        }
        keys.keyCount--;
    }
}
