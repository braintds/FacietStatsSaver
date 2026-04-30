using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FacietStatsSaver.Model
{
    public class getPlayerByNameResponse{
        public Account account;
        public getPlayerByNameResponse(Account Account) => account = Account;
    }



    public class Infractions
    {
    }



   

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
  
    public class MatchesStatsDTO
    {
        public List<Stats> items { get; set; }
        public int start { get; set; }
        public int end { get; set; }
    }
    }