using System;
using System.Collections.Generic;

namespace FacietStatsSaver.Entity;

public partial class Account
{
    public int Accountid { get; set; }

    public string Accountname { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public virtual ICollection<GameSession> Gamesessions { get; set; } = new List<GameSession>();
}
