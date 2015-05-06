using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class insertSponsorInviteBQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable ht = new Hashtable();

        ht.Add("Celina.Pesko-Persson@Unionen.se", "5802279389");
        ht.Add("ronnie.nilsson@unionen.se", "6306145050");
        ht.Add("Asa.Mars@Unionen.se", "7203204644");
        ht.Add("Carola.Andreasson@Unionen.se", "7308137806");
        ht.Add("Ulrika.Hektor@Unionen.se", "7007290047");
        ht.Add("Agneta.Funseth@Unionen.se", "4808058244");
        ht.Add("Carin.Hallerstrom@Unionen.se", "7702140349");
        ht.Add("Emma.Tjarnback@Unionen.se", "7711218946");
        ht.Add("Gosta.Karlsson@Unionen.se", "4907194650");
        ht.Add("Jon.Tillegard@Unionen.se", "7608280298");
        ht.Add("Julia.Tchibrikova@Unionen.se", "6407256665");
        ht.Add("Lena.GeorgssonWirkkala@Unionen.se", "6809089409");
        ht.Add("Mikael.Dubois@Unionen.se", "7401130054");
        ht.Add("Tobias.Brannemo@Unionen.se", "8206150412");
        ht.Add("Flemming.Kristensen@Unionen.se", "7703164298");
        ht.Add("Jennie.Zetterstrom@Unionen.se", "7701057106");
        ht.Add("Maria.Mirsch@Unionen.se", "5710301465");
        ht.Add("paola.felicetti@unionen.se", "6409290100");
        ht.Add("Seher.Yilmaz@Unionen.se", "8602203526");
        ht.Add("Annika.Soderdahl@Unionen.se", "7011033201");
        ht.Add("Camilla.Sandell@Unionen.se", "6708131286");
        ht.Add("Frida.Olsson@Unionen.se", "8308112708");
        ht.Add("Hanna.Noren@Unionen.se", "8710022446");
        ht.Add("Jan.Jegercrona@Unionen.se", "7109228952");
        ht.Add("Jonas.Haggblom@Unionen.se", "7104010553");
        ht.Add("Kanogo.Njuru@Unionen.se", "7303120435");
        ht.Add("Lena.Isenstam@Unionen.se", "7610062924");
        ht.Add("Malin.Wulkan@Unionen.se", "7205142925");
        ht.Add("Peter.Hellberg@Unionen.se", "6609064339");


        foreach (string s in ht.Keys)
        {
            SqlDataReader rs = Db.rs("SELECT si.Email, si.SponsorInviteID, w.SponsorInviteBQID, w.ValueText FROM SponsorInvite si LEFT OUTER JOIN SponsorInviteBQ w ON si.SponsorInviteID = w.SponsorInviteID AND w.BQID = 28 WHERE si.Email = '" + s.Replace("'","") + "'");
            if (rs.Read())
            {
                do
                {
                    if (rs.IsDBNull(2))
                    {
                        res.Text += "<b>" + s + " inserted</b><br/>";
                        Db.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueText) VALUES (" + rs.GetInt32(1) + ",28,'" + ht[s].ToString().Replace("'", "") + "')");
                    }
                    else if (rs.IsDBNull(3))
                    {
                        res.Text += "<b>" + s + " is blank</b><br/>";
                    }
                    else if (rs.GetString(3) != ht[s].ToString())
                    {
                        res.Text += s + " differs (" + rs.GetString(3) + " -> " + ht[s].ToString() + ")<br/>";
                    }
                    else
                    {
                        res.Text += s + " already OK!<br/>";
                    }
                }
                while (rs.Read());
            }
            else
            {
                res.Text += s + " not found!<br/>";
            }
            rs.Close();
        }
    }
}