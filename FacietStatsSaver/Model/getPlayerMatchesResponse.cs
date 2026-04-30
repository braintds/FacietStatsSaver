using System;
using System.Collections.Generic;
using System.Text;

namespace FacietStatsSaver.Model
{
    public class getPlayerMatchesResponse
    {
        public MatchesStats matches { get; set; }
    }

    public class MatchesStats
    {
        public List<Item> items { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }

    public class Item
    {
        public Stats stats { get; set; }
    }


}
