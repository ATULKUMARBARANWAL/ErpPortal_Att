<%@ Page Language="VB" AutoEventWireup="false" CodeFile="applyForLeave.aspx.vb" Inherits="UserPortal_applyForLeave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<script src="../assets/plugins/bootstrap/js/bootstrap.bundle.min.js" type="text/javascript"></script>
<link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />  
    <link href="../assets/css/styles.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <title>Apply for Leave</title>
    <style>
  body {
    font-family: Arial, sans-serif;
    background-color: #f4f4f9;
    margin: 0;
    padding: 0;
}
 body::-webkit-scrollbar 
 {
  width: 12px;               
 }

body::-webkit-scrollbar-track {
  background: #f2f3f5;     
}

body::-webkit-scrollbar-thumb {
  background-color: #787a7d;   
  border-radius: 18px;     
  border: 2px solid #f2f3f5; 
}
 .scrollbar {
  
  height: 328px;
  width: 98%;
  background: #fff;
  overflow-y: scroll;
 margin-left:4px;
}
.force-overflow {
  min-height: 50px;
}

.scrollbar-secondary::-webkit-scrollbar {
  width: 6px;
  background-color: #F5F5F5;
  }

.scrollbar-secondary::-webkit-scrollbar-thumb {
  border-radius: 10px;
  -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.1);
  background-color: #787a7d;
}

.scrollbar-secondary {
  scrollbar-color: #787a7d #F5F5F5;
}
.hoverCard:hover
{
    background:#daf5e9;
    
}
.ul {
  display: block;
  list-style-type: circle;
  margin-top: 1em;
  margin-bottom: 1em;
  margin-left: 0;
  margin-right: 0;
  padding-left: 25px;
  line-height:1.8;
  font-size:15px;
}
.form-container {
    background: white;
    padding: 30px;
    border-radius: 10px;
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
    width: 450px;
    margin: 50px auto;
    transition: box-shadow 0.3s ease-in-out;
}

.form-container:hover {
    box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
}

h2 {
    font-size: 22px;
    font-weight: 600;
    text-align: center;
    color: #525252;
    margin-bottom: 20px;
}

label {
    display: block;
    margin-bottom: 8px;
    font-weight: bold;
    font-size: 14px;
    color: #333;
}

.input-field, 
.dropdown, 
.textarea, 
.file-upload {
    width: 100%;
    padding: 12px;
    margin-bottom: 20px;
    border: 1px solid #ccc;
    border-radius: 8px;
    font-size: 14px;
    background-color: #fff;
    box-sizing: border-box;
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.input-field:focus, 
.dropdown:focus, 
.textarea:focus {
    border-color: #4CAF50;
    box-shadow: 0 0 6px rgba(76, 175, 80, 0.2);
    outline: none;
}

.file-upload {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 40px;
    border: 2px dashed #ccc;
    background: #f9f9f9;
    font-size: 14px;
    text-align: center;
    color: #888;
    cursor: pointer;
    transition: border-color 0.3s ease;
}

.file-upload:hover {
    border-color: #4CAF50;
}

.button {
    display: block;
    width: 100%;
    padding: 14px;
    background: rgb(27, 153, 94);
    color: white;
    border: none;
    font-size: 16px;
    font-weight: 600;
    border-radius: 8px;
    cursor: pointer;
    text-align: center;
    transition: background 0.3s ease-in-out;
}
.buttonn {
    display: block;
    width: 100%;
    padding: 14px;
    background: white;
    color: black;
    border: 1px solid rgb(21, 40, 60);
    font-size: 16px;
    font-weight: 600;
    border-radius: 8px;
    cursor: pointer;
    text-align: center;
    transition: background 0.3s ease-in-out;
}
.buttonn:hover
{
    background:  rgb(21, 40, 60);
    color:White;
}
.button:hover {
    background: rgb(21, 115, 71);
}

.success-label, 
.error-label {
    margin-top: 20px;
    display: block;
    font-size: 14px;
    text-align: center;
    font-weight: 600;
}

.success-label {
    color: #28a745;
}

.error-label {
    color: #dc3545;
}

/* Responsive Design */
@media (max-width: 480px) {
    .form-container {
        width: 90%;
        padding: 20px;
    }

    h2 {
        font-size: 18px;
    }

    .button {
        padding: 12px;
        font-size: 14px;
    }
}

/* Table Styling */
.table {
    width: 100%;
   
    border-collapse: collapse;
    font-size: 16px;
    text-align: left;
    background-color: white;
  border: 2px solid rgb(243, 247, 250);
}

.table th {
    
    color: rgb(42, 53, 71);
    text-transform: uppercase;
    padding: 16px;
    font-weight: bold;
    border: 1px solid #ddd;
}

.table td {
    padding: 16px;
    border: 1px solid #ddd;
    color: #333333;
}

.table tr:nth-child(even) {
    background-color: #f9f9f9;
}

.table tr:hover {
    background-color: #f1f1f1;
}

/* Responsive Design for Table */
@media screen and (max-width: 768px) {
    .table {
        font-size: 14px;
    }

    .table th, .table td {
        padding: 8px;
    }
}

/* Sticky Header (Optional) */
.table thead th {
    position: sticky;
    top: 0;
    z-index: 1;
}

/* Pagination Styling */
.pagination {
    display: flex;
    justify-content: center;
    margin: 20px 0;
}

.pagination a {
    color: #007BFF;
    padding: 8px 16px;
    text-decoration: none;
    border: 1px solid #ddd;
    margin: 0 4px;
    border-radius: 4px;
    transition: all 0.3s ease;
}

.pagination a:hover {
    background-color: #007BFF;
    color: #ffffff;
    border: 1px solid #007BFF;
}

</style>
<script>
    // JavaScript function to toggle the panel visibility
    function togglePanel() {
        // Use the correct ID of the panel
        var panel = document.getElementById('<%= pnlForLeave.ClientID %>');
        var formPanel = document.getElementById('<%= pnlApplyForLeave.ClientID %>')
        if (panel.style.display === "block") {

            panel.style.display = "none"; // Hide the panel
            formPanel.style.display = "block";

        } else {
            panel.style.display = "block"; // Show the panel
            formPanel.style.display = "none";

        }
    }
</script>
</head>
<body style="background:rgb(255, 255, 255);">
<form id="form1" runat="server" enctype="multipart/form-data">
 <nav class="navbar navbar-expand-lg navbar-light" style="border-bottom:1px solid #1ed085; background:#daf5e9;">
        <div class="container-fluid">
             <asp:LinkButton ID="btnCollgeName" runat="server" class="navbar-brand fw-semibold">GMS College, Gaziabad</asp:LinkButton>
            <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navbarCollapse">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarCollapse">
                <div class="navbar-nav">
                    <asp:LinkButton ID="btnHome" runat="server" class="nav-item nav-link active">
                    <i class="fa fa-home"></i>&nbsp;Home
                    </asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" class="nav-item nav-link">
                    <i class="fa fa-user-circle"></i>&nbsp;My Profile
                    </asp:LinkButton>
                      <asp:LinkButton ID="lbtngrievence" runat="server" class="nav-item nav-link">
                    <i class="fa fa-user-circle"></i>&nbsp;Grievence
                    </asp:LinkButton>
                    <asp:LinkButton ID="lblbtnApplyForLeave" runat="server" class="nav-item nav-link">
                    <i class="fa-solid fa-shop"></i>&nbsp;Apply For Leave
                    </asp:LinkButton>
                </div>
                <div class="navbar-nav ms-auto">
              <%--  <ul class="navbar-nav flex-row align-items-start justify-content-start">
                <li class="nav-item dropdown">
                <asp:LinkButton ID="btnNotification" runat="server" class="nav-link nav-icon-hover" data-bs-toggle="dropdown" aria-expanded="false">
                <i class="far fa-bell"></i>&nbsp;
                <asp:Label ID="lblNotificataion" runat="server" CssClass="fs-2"><div class="notification bg-primary rounded-circle"></div></asp:Label>  
                </asp:LinkButton>
          
           </li></ul> --%>         
                <asp:LinkButton ID="LinkButton2" runat="server" class="nav-item nav-link"><i class="fa fa-cog"></i>&nbsp;</asp:LinkButton>
                <asp:LinkButton ID="btnLogout" runat="server" class="nav-item nav-link"><i class="fa-solid fa-power-off"></i>&nbsp;Logout</asp:LinkButton>
                </div>
            </div>
        </div>
    </nav>
    <div style="background-color:rgb(255, 255, 255);padding:12px;border-radius:10px;width:94%;margin-left:3%;margin-top:2%;">
    <asp:Panel ID="pnlApplyForLeave" runat="server" style="display:none;">
    <div class="form-container"style="width:75%;">
    <div style="background-color:rgb(21, 40, 55);height:4.5rem;width:106%;padding:10px;margin-top:-28px;margin-left:-1.8rem; border-radius: 10px 10px 0px 0px;display:flex;text-align:center;margin-bottom:10px;">
        <h2 style="color:White;margin-top:12px;">Apply For Leave</h2>
        </div>
        <asp:Label ID="lblApplyDate" runat="server" Text="Apply Date" AssociatedControlID="txtApplyDate"></asp:Label>
        <span style="color: red;">*</span>
        <asp:TextBox ID="txtApplyDate" runat="server" CssClass="input-field" TextMode="Date"></asp:TextBox>

        <asp:Label ID="lblFromDate" runat="server" Text="From Date" AssociatedControlID="txtFromDate"></asp:Label>
        <span style="color: red;">*</span>
        <asp:TextBox ID="txtFromDate" runat="server" CssClass="input-field" TextMode="Date"></asp:TextBox>

        <asp:Label ID="lblToDate" runat="server" Text="To Date" AssociatedControlID="txtToDate"></asp:Label>
        <span style="color: red;">*</span>
        <asp:TextBox ID="txtToDate" runat="server" CssClass="input-field" TextMode="Date"></asp:TextBox>

        <asp:Label ID="lblLeaveType" runat="server" Text="Leave Type" AssociatedControlID="ddlLeaveType"></asp:Label>
        <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="dropdown">
            <asp:ListItem Text="Select a reason" Value="" Selected="True" Enabled="False"></asp:ListItem>
            <asp:ListItem Text="Medical" Value="Medical"></asp:ListItem>
            <asp:ListItem Text="Function" Value="Function"></asp:ListItem>
            <asp:ListItem Text="Personal" Value="Personal"></asp:ListItem>
            <asp:ListItem Text="Vacation" Value="Vacation"></asp:ListItem>
        </asp:DropDownList>

        <asp:Label ID="lblReason" runat="server" Text="Reason" AssociatedControlID="txtReason"></asp:Label>
        <asp:TextBox ID="txtReason" runat="server" CssClass="textarea" TextMode="MultiLine" Placeholder="Provide additional details (optional)"></asp:TextBox>

        <asp:Label ID="lblAttachDocument" runat="server" Text="Attach Document"></asp:Label>
        <asp:FileUpload ID="fileUploadDocument" runat="server" CssClass="file-upload" />
        <div style="display:flex;justify-content:space-between;">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" style="width:15rem;height:3rem;"/>
   
        </div>
        <asp:Label ID="lblSuccess" runat="server" CssClass="success-label" Visible="false"></asp:Label>
        <asp:Label ID="lblError" runat="server" CssClass="error-label" Visible="false"></asp:Label>
    </div>
    </asp:Panel>
    <asp:Panel ID="pnlForLeave" runat="server" >
    <div class="grid-container" style="box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);padding:15px;border:1px solid rgb(243, 247, 250);">
        
         <div style="display:flex;  align-items:center;margin-bottom:1rem;justify-content:space-between;margin-top:1rem;">
         <h3>Leave Applications</h3>
   <asp:LinkButton ID="Button1" runat="server" CssClass="buttonn" 
    OnClick="btnSave_Click" OnClientClick="togglePanel(); return false;"
    style="width:12rem; height:2.5rem;">
 <i class="fa-solid fa-plus"></i> Add New Holiday
</asp:LinkButton>
    </div>
    <hr />
       <asp:GridView ID="gvLeaveApplications" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
            <asp:BoundField DataField="Sno" HeaderText="S.No" />
            <asp:BoundField DataField="applyDate" HeaderText="Apply Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="fromDate" HeaderText="From Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="toDate" HeaderText="To Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="leaveType" HeaderText="Leave Type" />
            <asp:BoundField DataField="status" HeaderText="Status" />
        </Columns>
    </asp:GridView>
    </div>
   
    </asp:Panel>
    </div>
   
</form>

</body>
</html>
