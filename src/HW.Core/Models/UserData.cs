using System;


namespace HW.Core.Models
{
    /// <summary>
    /// Class data for the user
    /// </summary>
    public struct UserData 
    {
        public String Token;
        public DateTime TokenExpires;
        public int LanguageID;
    }
}
