<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bqSetup.aspx.cs" Inherits="HW.Adm.bqSetup" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Background Questions</td></tr>
		</table>
		<table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td valign=top>
					<table border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td>Question&nbsp;</td>
							<td><asp:TextBox ID=Internal Width=300 runat=server /></td>
						</tr>
						<tr>
							<td>Variable&nbsp;name&nbsp;</td>
							<td><asp:TextBox ID=Variable Width=50 runat=server /></td>
						</tr>
						<tr>
							<td>Type</td>
							<td>
								<asp:DropDownList ID=Type Width=300 runat=server AutoPostBack=true>
									<asp:ListItem Text="Select one, radio-style" Value="1"/>
									<asp:ListItem Text="Select one, drop-down-style" Value="7"/>
									<asp:ListItem Text="Text" Value="2"/>
									<asp:ListItem Text="Numeric" Value="4"/>
									<asp:ListItem Text="Date" Value="3"/>
									<asp:ListItem Text="VAS" Value="9"/>
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td>Comparison&nbsp;</td>
							<td>
								<asp:RadioButtonList ID=Comparison runat=server RepeatDirection=Horizontal RepeatLayout=table CellPadding=0 CellSpacing=0 ><asp:ListItem Value=NULL Text="No"/><asp:ListItem Value=1 Text="Yes" /></asp:RadioButtonList>
							</td>
						</tr>
						<asp:PlaceHolder ID=FreeParams runat=server>
						<tr>
							<td>Default value&nbsp;</td>
							<td>
								<asp:TextBox ID=DefaultVal Width=50 runat=server />
								&nbsp;
								Max length
								&nbsp;
								<asp:TextBox ID=MaxLength Width=35 runat=server />
								Required length
								&nbsp;
								<asp:TextBox ID=ReqLength Width=35 runat=server />
								Measurement unit
								&nbsp;
								<asp:TextBox ID=MeasurementUnit Width=35 runat=server />
							</td>
						</tr>
						</asp:PlaceHolder>
						<asp:PlaceHolder ID=RadioParams runat=server >
						<tr>
							<td>Layout&nbsp;</td>
							<td>
								<asp:RadioButtonList ID=Layout runat=server RepeatDirection=Horizontal RepeatLayout=table CellPadding=0 CellSpacing=0>
								<asp:ListItem Value="NULL" Text="Horizontal" Selected />
								<asp:ListItem Value="1" Text="Vertical" />
								</asp:RadioButtonList>
							</td>
						</tr>
						</asp:PlaceHolder>
						<tr><td colspan=2>&nbsp;</td></tr>
						<tr><td colspan=2>
							<asp:Button ID=Back Text="Back" runat=server />&nbsp;<asp:Button ID=Save Text="Save" runat=server />&nbsp;<asp:Button ID=AddAnswer Text="Add answer" runat=server />&nbsp;<asp:Button ID=AddCond Text="Add cond" runat=server /></td></tr>
						<tr><td colspan=2>&nbsp;</td></tr>
						<tr>
							<td colspan=2>
								<asp:PlaceHolder Visible=false ID=NewAnswer runat=server>
									Answer&nbsp;<asp:TextBox ID=Answer Width=300 runat=server /><br />
								</asp:PlaceHolder>
								<asp:PlaceHolder Visible=false ID=Condition runat=server>
									Condition&nbsp;<asp:DropDownList ID=BQIDBAID runat=server /><br />
								</asp:PlaceHolder>
							</td>
						</tr>
					</table>
				</td>
				<td>&nbsp;</td>
				<td valign=top><asp:Label ID=Answers runat=server /></td>
				<td>&nbsp;</td>
				<td valign=top><asp:Label ID=Visibility runat=server /></td>
			</tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
