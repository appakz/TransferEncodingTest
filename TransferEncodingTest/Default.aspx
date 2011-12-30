<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TransferEncodingTest.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <legend>Options</legend>
            <p>
                <asp:CheckBox runat="server" ID="chkUseFilter" Text="Use Filter?" /></p>
                <p>
            <asp:CheckBox runat="server" ID="chkSendChunked" Text="Send In Chunks?" /></p>
        </fieldset>
        <asp:Button runat="server" ID="btnCreatePdf" Text="Create PDF" OnClick="btnCreatePdf_Click" />
    </div>
    </form>
</body>
</html>
