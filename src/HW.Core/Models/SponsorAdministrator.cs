using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
    /// <summary>
    /// Class data for the Sponsor inheriting class BaseModel
    /// </summary>
    public class Sponsors : BaseModel
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Class data for the Sponsor Admin inheriting class BaseModel
    /// </summary>
    public class SponsorAdminstrator : BaseModel
    {
        public int Status { get; set; }
        //public int SponsorAdminID { get; set; }
        public Sponsors Sponsor { get; set; }
        public string Name { get; set; }
        public bool ReadOnly { get; set; }
        public bool SeeUsers { get; set; }
        public bool Anonymized { get; set; }
        public int SuperAdminId { get; set; }
        public string Token { get; set; }
        public DateTime tokenExpires;
    }
}
