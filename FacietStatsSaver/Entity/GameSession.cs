using System;
using System.Collections.Generic;

namespace FacietStatsSaver.Entity;

public partial class GameSession
{
    public int Sessionid { get; set; }

    public int Accountid { get; set; }

    public DateTime Intervalstartdate { get; set; }

    public DateTime Intervalenddate { get; set; }

    public DateTime Sessionstartdate { get; set; }

    public DateTime? Sessionenddate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
