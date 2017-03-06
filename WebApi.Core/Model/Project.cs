using System;
using System.Collections.Generic;
using System.Linq;


namespace WebApi.Core
{
    public class Project
    {

        public int id { get; set; }
        public string projectNumber { get; set; }
        public DateTime projectDate { get; set; }
        public string declarer { get; set; }
        public int waterCorpId { get; set; }
        public int areaId { get; set; }
        public int gardenId { get; set; }
        public string waterSupplyType { get; set; }
        public string simcardOwner { get; set; }
        public string builderTyp { get; set; }
        public string isRenewal { get; set; }
        public string content { get; set; }

    }
}