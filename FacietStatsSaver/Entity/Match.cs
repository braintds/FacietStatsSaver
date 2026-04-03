using System;
using System.Collections.Generic;

namespace FacietStatsSaver.Entity;

public partial class Match
{
    public int Matchid { get; set; }

    public int Sessionid { get; set; }

    public string Mapname { get; set; } = null!;

    public DateTime Playedat { get; set; }

    public int Kills { get; set; }

    public int Assists { get; set; }

    public int Deaths { get; set; }

    public int Doublekills { get; set; }

    public int Triplekills { get; set; }

    public int Quadrokills { get; set; }

    public int Mvps { get; set; }

    public bool Iswin { get; set; }

    public virtual GameSession Session { get; set; } = null!;
}
