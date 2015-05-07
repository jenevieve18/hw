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
									SurveyIntro.Text += "Besv�r rygg: " + (Qback ? "<B>" : "") + Qback + (Qback ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Besv�r nacke: " + (Qneck ? "<B>" : "") + Qneck + (Qneck ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Sv�rt sova pga " + (Qback ? "besv�r rygg" + (Qneck ? " och/eller " : "") : "") + (Qneck ? (Qback ? "" : "besv�r ") + "nacke" : "") + ": " + (QbnSleep ? "<B>" : "") + QbnSleep + (QbnSleep ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Sv�rt utf�ra arbete pga " + (Qback ? "besv�r rygg" + (Qneck ? " och/eller " : "") : "") + (Qneck ? (Qback ? "" : "besv�r ") + "nacke" : "") + ": " + (QbnWork ? "<B>" : "") + QbnWork + (QbnWork ? "</B>" : "") + "<br>";
									SurveyIntro.Text += "Besv�r " + (Qback ? "rygg" + (Qneck ? " och/eller " : "") : "") + (Qneck ? "nacke" : "") + " f�rv�rras av arbete: " + (QbnWorse ? "<B>" : "") + QbnWorse + (QbnWorse ? "</B>" : "") + "<br>";
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
									SurveyIntro.Text += "</B><BR>Du har angivit att du har besv�r fr�n ";
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
									SurveyIntro.Text += " samt att dessa besv�r ";
									if(QbnWorse)
									{
										SurveyIntro.Text += "f�rv�rras av arbetet";
										if(QbnSleep || QbnWork)
										{
											SurveyIntro.Text += " och att besv�ren ";
										}
									}
									if(QbnSleep || QbnWork)
									{
										SurveyIntro.Text += "varit s� pass sv�ra att du haft sv�rt ";
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
											SurveyIntro.Text += "utf�ra ditt arbete";
										}
									}
									SurveyIntro.Text += ". Du rekommenderas att bes�ka personalsjukgymnast.";
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
							SurveyIntro.Text += "R�ker: " + (Qsmoke ? "<B>" : "") + Qsmoke + (Qsmoke ? "</B>" : "") + "<br>";
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
										SurveyIntro.Text += "L�ngvarig hosta";
										if(Qpip)
										{
											SurveyIntro.Text += " och ";
										}
									}
									if(Qpip)
									{
										SurveyIntro.Text += "Pip i br�stet";
									}
									SurveyIntro.Text += "</B><BR>Dina svar visar att du har symtom som skulle kunna bero p� din r�kning. Om du inte redan har s�kt l�karhj�lp, rekommenderar vi att du g�r det. Om du vill hj�lp med att sluta r�ka, kan du f� det genom arbetsplatsen. Ange om du �r intresserad av n�got av dessa alternativ";
								}
								else
								{
									SurveyIntro.Text += "<B STYLE=\"font-size:16px;\">R�kning</B><BR>Som du s�kert vet, �r r�kning riskabelt f�r h�lsan. Om du vill hj�lp med att sluta r�ka, kan du f� det genom arbetsplatsen. Ange om du �r intresserad av n�got av dessa alternativ";
								}
							}
							else if(Qpip || Qhosta)
							{
								SurveyIntro.Text += "<B STYLE=\"font-size:16px;\">";
								if(Qhosta)
								{
									SurveyIntro.Text += "L�ngvarig hosta";
									if(Qpip)
									{
										SurveyIntro.Text += " och ";
									}
								}
								if(Qpip)
								{
									SurveyIntro.Text += "Pip i br�stet";
								}
								SurveyIntro.Text += "</B><BR>Symtomen l�ngvarig hosta och pip i br�stet kan vara symtom p� astma. Om du inte du redan har behandling, rekommenderar vi att du s�ker din husl�kare f�r en bed�mning.";
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
