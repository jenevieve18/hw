using System;
	
namespace HW.EForm.Core.Models
{
	public class UserNote
	{
		public UserNote()
		{
		}
		
		public int UserNoteID { get; set; }
		public int UserID { get; set; }
		public DateTime? DT { get; set; }
		public int SponsorAdminID { get; set; }
		public string Note { get; set; }
		public int EditSponsorAdminID { get; set; }
		public DateTime? EditDT { get; set; }
		public string NoteJapaneseUnicode { get; set; }

	}
}
