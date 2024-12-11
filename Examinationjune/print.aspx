<%@ Page Language="VB" AutoEventWireup="false" CodeFile="print.aspx.vb" Inherits="Reports_print" %>
<%--'Design And Developed By Avaneesh Yadav--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        window.print();
        window.onafterprint = back;

        function back() {
            window.history.back();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
           </form>
</body>
</html>