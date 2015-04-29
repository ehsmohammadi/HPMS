<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SilverlightLogin.aspx.cs" Inherits="MITD.PMS.Interface.Presentation.Host.SilverlightLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="setTimeout('window.parent.hideDivLogin()', 2000)">
    <form id="form1" runat="server">
    <div style="text-align:center">
        <p>
            تا چند لحظه دیگر به سایت باز خواهید گشت...
        </p>
        <p>
            در صورت طولانی شدن عمل بازگشت لطفا کلید ذیل را فشار دهید.
        </p>
        <p>
            <input type="button" value="بازگشت" onclick="window.parent.hideDivLogin()" />
        </p>
    </div>
    </form>
</body>
</html>