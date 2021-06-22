using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[System.Serializable]
public class SaveTurret
{
    public float[] Position { get; set; }
    public Turret.Levels Level { get; set; }

    public Turret.TypeTurret Type { get; set; }
}

