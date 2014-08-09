<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Report.aspx.cs" Inherits="MITD.PMS.Interface.Presentation.Host.Report" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="width: auto">
    <form id="form1" runat="server" >
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" >
        </asp:ScriptManager>
    </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" SizeToReportContent="true" 
            ZoomMode="PageWidth" >
        </rsweb:ReportViewer>
    </form>
</body>
</html>
