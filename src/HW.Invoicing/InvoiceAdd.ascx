<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceAdd.ascx.cs" Inherits="HW.Invoicing.InvoiceAdd1" %>
<style>
.hw-invoice-items {
    width:100%;
    margin-bottom:40px;
    padding:5px;
}
.hw-border-left {
    border-top:1px solid;
    border-left:1px solid;
}
.hw-border-bottom {
    border-bottom:1px solid;
}
.hw-border-top {
    border-top:1px solid;
}
.hw-border-last {
    border-top:1px solid;
    border-left:1px solid;
    border-right:1px solid;
}
.hw-footer 
{
    font-weight:bold;
}
.hw-invoice-header {
    text-transform: uppercase;
    font-size:x-small;
    font-weight:bold;
}
</style>

<div class="modal-dialog" style="width:80%">
	<div class="modal-content">
		<div class="modal-header">
			<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
			<h4 class="modal-title" id="H1">Create an invoice</h4>
		</div>
		<div class="modal-body">
            <table width="100%" cellpadding="5px">
                <tr>
                    <td rowspan="4" valign="bottom">
                        <img src="http://s16.postimg.org/vkxh59d5h/ihg.png"><br />
                    </td>
                    <td></td>
                    <td></td>
                    <td><h3>INVOICE</h3></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="hw-border-top">Customer Number</td>
                    <td class="hw-border-top"><strong>812000-5650</strong></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="hw-border-top">Invoice Number</td>
                    <td class="hw-border-top"><strong>IHGF-134</strong></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="hw-border-top">Invoice Date</td>
                    <td class="hw-border-top"><strong>2012-05-31</strong></td>
                </tr>
                <tr>
                    <td>
                        Customer/Postal Address/Invoice Address
                    </td>
                    <td></td>
                    <td class="hw-border-top">Maturity Date</td>
                    <td class="hw-border-top"><strong>2012-06-30</strong></td>
                </tr>
                <tr>
                    <td>
                        <strong>Company Address<br />
                        P.O. Box 1234<br />
                        111 35 Stockholm</strong>
                    </td>
                    <td></td>
                    <td colspan="2">
                        <small>
                            Your Reference: First Name + Surname<br />
                            Our Reference: Dan Hasson
                        </small>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Purchase order number: XX6279292</strong>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <br />
            <p>Payment terms: 30 days net. At the settlement after the due date will be charged interest of 2% per month.</p>
            <table class="hw-invoice-items" cellpadding="5px">
                <tr class="hw-invoice-header">
                    <td class="hw-border-left"><strong>Item</strong></td>
                    <td class="hw-border-left" style="width:5%"><strong>Qty</strong></td>
                    <td class="hw-border-left" style="width:10%"><strong>Unit</strong></td>
                    <td class="hw-border-left" style="width:10%"><strong>Price/Unit</strong></td>
                    <td class="hw-border-last" style="width:10%"><strong>Amount</strong></td>
                </tr>
                <tr>
                    <td>Usage HealthWatch 2012.01.01 - 2012.06.30</td>
                    <td>10</td>
                    <td>month</td>
                    <td class="text-right">100,00</td>
                    <td class="text-right">1.000,00</td>
                </tr>
                <tr>
                    <td>2015-05-20 Preparations of reports and questionaire surveys. (Dan)</td>
                    <td>2</td>
                    <td>hours</td>
                    <td class="text-right">400,00</td>
                    <td class="text-right">800,00</td>
                </tr>
                <tr>
                    <td>2015-05-20 Preparations of reports and questionaire surveys. (Dan)</td>
                    <td>2</td>
                    <td>hours</td>
                    <td class="text-right">400,00</td>
                    <td class="text-right">800,00</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr class="hw-invoice-header">
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="hw-border-last">SubTotal</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="hw-border-last">3.200,00</td>
                </tr>
                <tr class="hw-invoice-header">
                    <td></td>
                    <td></td>
                    <td class="hw-border-left">Moms %</td>
                    <td class="hw-border-left">Moms</td>
                    <td class="hw-border-last">Total Amount</td>
                </tr>
                <tr class="hw-border-bottom">
                    <td></td>
                    <td></td>
                    <td class="hw-border-left">25,00</td>
                    <td class="hw-border-left">800,00</td>
                    <td class="hw-border-last">4.000,00</td>
                </tr>
            </table>
            <small class="hw-footer">
                <table style="width:100%">
                    <tr>
                        <td colspan="4">Interactive Health Group in Stockholm AB</td>
                        <td>Bankgiro</td>
                        <td>5091 – 8853</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Phone</td>
                        <td>+46-70-7284298</td>
                        <td></td>
                        <td></td>
                        <td>VAT/Momsreg.nr</td>
                        <td>SE556712369901</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Postal Address</td>
                        <td colspan="2">Rörstrandsgatan 36, 113 40 Stockholm, Sweden</td>
                        <td></td>
                        <td>F-skattebevis</td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </small>
		</div>
		<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button> <button type="button" class="btn btn-primary">Save changes</button>
		</div>
	</div>
					
</div>