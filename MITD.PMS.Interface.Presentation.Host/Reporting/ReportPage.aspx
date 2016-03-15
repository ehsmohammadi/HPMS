<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReportPage.aspx.cs" Inherits="MITD.PMS.Interface.Presentation.Host.ReportPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="width: auto">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" SizeToReportContent="true"
            ZoomMode="PageWidth" ShowPrintButton="False">
        </rsweb:ReportViewer>
        <div id="container" class=" " style="display: inline-block; font-family: Verdana; font-size: 8pt; vertical-align: top;">
            <table cellpadding="0" cellspacing="0" style="display: inline;">
                <tbody>
                    <tr>
                        <td height="28px">
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <script src="../Scripts/jquery-2.2.1.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $($('#ReportViewer1_fixedTable').find('td[colspan=3] div[id^="ReportViewer1"]')[1])
                        .find('div')
                        .first()
                        .append($('#container').detach());
            });
        </script>
    </form>
</body>
</html>
