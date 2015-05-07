using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eForm
{
	/// <summary>
	/// Summary description for testFeedback.
	/// </summary>
	public class testFeedback : System.Web.UI.Page
	{
		protected Label SurveyIntro;

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Rygg/nacke
			bool Qback = true;
			bool Qneck = true;
			bool QbnSleep = true;
			bool QbnWork = true;
			bool QbnWorse = true;

			for(int i1=0; i1<2; i1++)
			{
				Qback = !Qback;
				for(int i2=0; i2<2; i2++)
				{
					Qneck = !Qneck;
					for(int i3=0; i3<2; i3++)
					{
						QbnSleep = !QbnSleep;
						for(int i4=0; i4<2; i4++)
						{
							QbnWork = !QbnWork;
							for(int i5=0; i5<2; i5++)
							{
								QbnWorse = !QbnWorse;

								if((Qback || Qneck) && (QbnSleep || QbnWork || QbnWorse))
								{
									SurveyIntro.Text += "<span style=\"color:#888888;\">";
									SurveyIntro.Text += "Besvär rygg: " + (Qback ? "<B>" : "") + Qback + (Qback ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Besvär nacke: " + (Qneck ? "<B>" : "") + Qneck + (Qneck ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Svårt sova pga " + (Qback ? "besvär rygg" + (Qneck ? " och/eller " : "") : "") + (Qneck ? (Qback ? "" : "besvär ") + "nacke" : "") + ": " + (QbnSleep ? "<B>" : "") + QbnSleep + (QbnSleep ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Svårt utföra arbete pga " + (Qback ? "besvär rygg" + (Qneck ? " och/eller " : "") : "") + (Qneck ? (Qback ? "" : "besvär ") + "nacke" : "") + ": " + (QbnWork ? "<B>" : "") + QbnWork + (QbnWork ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Besvär " + (Qback ? "rygg" + (Qneck ? " och/eller " : "") : "") + (Qneck ? "nacke" : "") + " förvärras av arbete: " + (QbnWorse ? "<B>" : "") + QbnWorse + (QbnWorse ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "</span>";

									SurveyIntro.Text += "<B STYLE=\"font-size:16px;\">";
									if(Qback)
									{
										SurveyIntro.Text += "Rygg";
										if(Qneck)
										{
											SurveyIntro.Text += " och ";
										}
									}
									if(Qneck)
									{
										SurveyIntro.Text += "Nacke";
									}
									SurveyIntro.Text += "</B><BR>Du har angivit att du har besvär från ";
									if(Qback)
									{
										SurveyIntro.Text += "ryggen";
										if(Qneck)
										{
											SurveyIntro.Text += " och ";
										}
									}
									if(Qneck)
									{
										SurveyIntro.Text += "nacken";
									}
									SurveyIntro.Text += " samt att dessa besvär ";
									if(QbnWorse)
									{
										SurveyIntro.Text += "förvärras av arbetet";
										if(QbnSleep || QbnWork)
										{
											SurveyIntro.Text += " och att besvären ";
										}
									}
									if(QbnSleep || QbnWork)
									{
										SurveyIntro.Text += "varit så pass svåra att du haft svårt ";
										if(QbnSleep)
										{
											SurveyIntro.Text += "att sova";
											if(QbnWork)
											{
												SurveyIntro.Text += " och ";
											}
										}
										if(QbnWork)
										{
											SurveyIntro.Text += "utföra ditt arbete";
										}
									}
									SurveyIntro.Text += ". Du rekommenderas att besöka personalsjukgymnast.";
									SurveyIntro.Text += "<BR><BR>";
								}
							}
						}
					}
				}
			}
			#endregion

			SurveyIntro.Text += "<BR><HR><BR>";

			#region pip/hosta
			bool Qhosta = true;
			bool Qpip = true;
			bool Qsmoke = true;

			for(int i1=0; i1<2; i1++)
			{
				Qsmoke = !Qsmoke;
				for(int i2=0; i2<2; i2++)
				{
					Qpip = !Qpip;

					for(int i3=0; i3<2; i3++)
					{
						Qhosta = !Qhosta;
						
						if(Qpip || Qhosta || Qsmoke)
						{
							SurveyIntro.Text += "<span style=\"color:#888888;\">";
							SurveyIntro.Text += "Röker: " + (Qsmoke ? "<B>" : "") + Qsmoke + (Qsmoke ? "</B>" : "") + "<br>";
							SurveyIntro.Text += "Pip: " + (Qpip ? "<B>" : "") + Qpip + (Qpip ? "</B>" : "") + "<br>";
							SurveyIntro.Text += "Hosta: " + (Qhosta ? "<B>" : "") + Qhosta + (Qhosta ? "</B>" : "") + "<br>";
							SurveyIntro.Text += "</span>";

							if(Qsmoke)
							{
								if(Qhosta || Qpip)
								{
									SurveyIntro.Text += "<B STYLE=\"font-size:16px;\">";
									if(Qhosta)
									{
										SurveyIntro.Text += "Långvarig hosta";
										if(Qpip)
										{
											SurveyIntro.Text += " och ";
										}
									}
									if(Qpip)
									{
										SurveyIntro.Text += "Pip i bröstet";
									}
									SurveyIntro.Text += "</B><BR>Dina svar visar att du har symtom som skulle kunna bero på din rökning. Om du inte redan har sökt läkarhjälp, rekommenderar vi att du gör det. Om du vill hjälp med att sluta röka, kan du få det genom arbetsplatsen. Ange om du är intresserad av något av dessa alternativ";
								}
								else
								{
									SurveyIntro.Text += "<B STYLE=\"font-size:16px;\">Rökning</B><BR>Som du säkert vet, är rökning riskabelt för hälsan. Om du vill hjälp med att sluta röka, kan du få det genom arbetsplatsen. Ange om du är intresserad av något av dessa alternativ";
								}
							}
							else if(Qpip || Qhosta)
							{
								SurveyIntro.Text += "<B STYLE=\"font-size:16px;\">";
								if(Qhosta)
								{
									SurveyIntro.Text += "Långvarig hosta";
									if(Qpip)
									{
										SurveyIntro.Text += " och ";
									}
								}
								if(Qpip)
								{
									SurveyIntro.Text += "Pip i bröstet";
								}
								SurveyIntro.Text += "</B><BR>Symtomen långvarig hosta och pip i bröstet kan vara symtom på astma. Om du inte du redan har behandling, rekommenderar vi att du söker din husläkare för en bedömning.";
							}
							SurveyIntro.Text += "<BR><BR>";
						}
					}
				}
			}
			#endregion
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
