<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RevlutionDash.aspx.vb" Inherits="UserPortal_RevlutionDash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">   
<title>Revolution Dashboard</title>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<script src="../assets/plugins/bootstrap/js/bootstrap.bundle.min.js" type="text/javascript"></script>
<link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />  
    <link href="../assets/css/styles.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
<style>

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
 .addsubject
        {
            color: #15283c;
            font-size: 18px !important;
            cursor: pointer;
            font-weight: 500;
            border: 1px solid #15283c;
            background-color: #fff;
            text-decoration: none;
            text-align: center;
            border-radius: 4px;
            padding: 2px 10px;
        }
        .addsubject:hover
        {
            color: #fff;
            background-color: #15283c;
            text-decoration: none;
        }
          .form-container {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            width:40%;
          
        }
        h2 {
            margin-bottom: 20px;
            font-size: 18px;
            text-align: center;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input[type="text"],
        input[type="date"],
        textarea,
        select {
            width: 100%;
            padding: 8px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        textarea {
            resize: none;
            height: 80px;
        }
        .file-upload {
            display: flex;
            align-items: center;
            padding: 8px;
            border: 1px dashed #ccc;
            border-radius: 4px;
            background: #f9f9f9;
            margin-bottom: 15px;
            cursor: pointer;
            text-align: center;
        }
        button {
            background: rgb(82, 82, 82);
            color: white;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
            width: 40%;
            
        }
        button:hover {
            background: rgb(31, 31, 31);
        }
</style>
</head>
<body>
    <form id="form1" runat="server"> 
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
                    
                </div>
                <div class="navbar-nav ms-auto">
                <ul class="navbar-nav flex-row align-items-start justify-content-start">
                <li class="nav-item dropdown">
                <asp:LinkButton ID="btnNotification" runat="server" class="nav-link nav-icon-hover" data-bs-toggle="dropdown" aria-expanded="false">
                <i class="far fa-bell"></i>&nbsp;
                <asp:Label ID="btnNitificataion" runat="server" CssClass="fs-2"><div class="notification bg-primary rounded-circle"></div></asp:Label>  
                </asp:LinkButton>
          <div class="dropdown-menu content-dd dropdown-menu-end dropdown-menu-animate-up" aria-labelledby="drop2">
            <div class="d-flex align-items-start justify-content-between py-3 px-7">
            <h5 class="mb-0 fs-4 fw-semibold">Notifications</h5>
            <span class="badge text-bg-primary rounded-4 px-3 py-1">
            <asp:Label ID="lblNotification" runat="server">5</asp:Label> new
            </span>
            </div>
            <div class="message-body scrollbar  scrollbar-secondary" data-simplebar>
            <div class="force-overflow">
                <asp:LinkButton ID="btnProfile" runat="server" class="py-6 px-7 d-flex align-items-center dropdown-item">
                <span class="me-3">
                    <img src="../assets/images/users/user-1.jpg" alt="user" class="rounded-circle" width="48" height="48"/>
                </span>
                <div class="w-75 d-inline-block v-middle">
                    <h6 class="mb-1 fw-semibold lh-base">Roman Joined the Team!</h6>
                    <span class="fs-2 d-block text-body-secondary">Congratulate him</span>
                </div>
                </asp:LinkButton>
                <a href="javascript:void(0)" class="py-6 px-7 d-flex align-items-center dropdown-item">
                <span class="me-3">
                    <img src="../assets/images/users/user-1.jpg" alt="user" class="rounded-circle" width="48" height="48"/>
                </span>
                <div class="w-75 d-inline-block v-middle">
                    <h6 class="mb-1 fw-semibold lh-base">New message</h6>
                    <span class="fs-2 d-block text-body-secondary">Salma sent you new message</span>
                </div>
                </a>
                <a href="javascript:void(0)" class="py-6 px-7 d-flex align-items-center dropdown-item">
                <span class="me-3">
                    <img src="../assets/images/users/user-1.jpg" alt="user" class="rounded-circle" width="48" height="48"/>
                </span>
                <div class="w-75 d-inline-block v-middle">
                    <h6 class="mb-1 fw-semibold lh-base">Bianca sent payment</h6>
                    <span class="fs-2 d-block text-body-secondary">Check your earnings</span>
                </div>
                </a>
                <a href="javascript:void(0)" class="py-6 px-7 d-flex align-items-center dropdown-item">
                <span class="me-3">
                    <img src="../assets/images/users/user-1.jpg" alt="user" class="rounded-circle" width="48" height="48"/>
                </span>
                <div class="w-75 d-inline-block v-middle">
                    <h6 class="mb-1 fw-semibold lh-base">Jolly completed tasks</h6>
                    <span class="fs-2 d-block text-body-secondary">Assign her new tasks</span>
                </div>
                </a>
                <a href="javascript:void(0)" class="py-6 px-7 d-flex align-items-center dropdown-item">
                <span class="me-3">
                    <img src="../assets/images/users/user-1.jpg" alt="user" class="rounded-circle" width="48" height="48"/>
                </span>
                <div class="w-75 d-inline-block v-middle">
                    <h6 class="mb-1 fw-semibold lh-base">John received payment</h6>
                    <span class="fs-2 d-block text-body-secondary">$230 deducted from account</span>
                </div>
                </a>
                <a href="javascript:void(0)" class="py-6 px-7 d-flex align-items-center dropdown-item">
                <span class="me-3">
                    <img src="../assets/images/users/user-1.jpg" alt="user" class="rounded-circle" width="48" height="48"/>
                </span>
                <div class="w-75 d-inline-block v-middle">
                    <h6 class="mb-1 fw-semibold lh-base">Roman Joined the Team!</h6>
                    <span class="fs-2 d-block text-body-secondary">Congratulate him</span>
                </div>
                </a>
            </div>
            </div>
            <div class="py-6 px-7 mb-1">
            <button class="btn btn-outline-primary w-100">See All Notifications</button>
           </div>

          </div>
           </li></ul>          
                <asp:LinkButton ID="LinkButton2" runat="server" class="nav-item nav-link"><i class="fa fa-cog"></i>&nbsp;</asp:LinkButton>
                <asp:LinkButton ID="btnLogout" runat="server" class="nav-item nav-link"><i class="fa-solid fa-power-off"></i>&nbsp;Logout</asp:LinkButton>
                </div>
            </div>
        </div>
    </nav>
    <div class="container-fluid p-3">
    <div class="row">
    <div class="col-md-12">
     <asp:Panel ID="PnlTimeTable" runat="server" Visible="true"> 
    <div class="row">

   <div class="col-md-12">
   <div class="card crdcardmain bordercrd" >
   <div class="card-body p-2">
    <div class="row">
    <div class="col-md-6">
    <table width="100%">
    <tr width="100%">
    <td width="6%">
    <asp:LinkButton ID="backbotton" CssClass="fw-bold text-black p-2" runat="server" ><i class="fa-solid fa-arrow-left fs-7"></i> 
    </asp:LinkButton>
    </td>
    <td width="94%">
    <h5 class="enquiry">&nbsp;&nbsp;Re-Evaluation Dashboard</h5>
    </td>
    </tr>
    </table>    
    </div>
    <div class="col-md-6 text-end">
        <asp:LinkButton ID="btnViewAttend" runat="server" CssClass="addsubject"><i class="fa fa-eye"></i> View Attendance</asp:LinkButton>
       
    </div>
    </div>
    <div class="row mt-1">
    <div class="col-md-12">
    <div class="line border-top"></div>
    </div>
    </div>

    <h5 class="text-muted mt-2" style="font-size:17px;">Time Table</h5>
    <div class="row">
    <div class="col-md-12">
    <asp:Panel ID="PanelGritimetable" runat="server" Visible="true" ScrollBars="Auto">
    <asp:GridView ID="GridExam" GridLines="Both" Width="100%" DataKeyNames="Timetableid"
    AutoGenerateColumns="False" runat="server" CellPadding="0" ShowHeaderWhenEmpty="true"
    CssClass="table table-bordered table-sm">
    <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <%#Container.DataItemIndex+1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Teacher" DataField="Teacher"></asp:BoundField>
        <asp:BoundField HeaderText="Period" DataField="prd"></asp:BoundField>
        <asp:BoundField HeaderText="Program" DataField="course"></asp:BoundField>
        <asp:BoundField HeaderText="Classes" DataField="Classes"></asp:BoundField>
        <asp:BoundField HeaderText="ClassRoom" DataField="ClassRoom"></asp:BoundField>
        <asp:BoundField HeaderText="Group" DataField="Grp"></asp:BoundField>
        <asp:BoundField HeaderText="Subject" DataField="Subject"></asp:BoundField>
        <asp:BoundField HeaderText="Teacher Type" DataField="Teach_Type"></asp:BoundField>
    </Columns>
    </asp:GridView>
    </asp:Panel>
    </div>
    </div>

   </div>
   </div>
  </div>
 

   
</div>
</asp:Panel>
 <asp:Panel ID="PnlViewAttendance" runat="server" Visible="false"> 

 <div class="row">

   <div class="col-md-12">
   <div class="card crdcardmain bordercrd">
   <div class="card-body p-2">
   <div class="row">
    <div class="col-md-6">
    <table width="100%">
    <tr width="100%">
    <td width="6%">
    <asp:LinkButton ID="LinkButton3" CssClass="fw-bold p-2 text-black" runat="server"><i class="fa-solid fa-arrow-left fs-7"></i> 
    </asp:LinkButton>
    </td>
    <td width="94%">
    <h5 class="enquiry">&nbsp;&nbsp;Re-Evaluation Dashboard</h5>
    </td>
    </tr>
    </table>    
    </div>
    <div class="col-md-6 text-end">
       <asp:LinkButton ID="btnTimeTable" runat="server" CssClass="addsubject"><i class="fa fa-eye"></i> Time Table</asp:LinkButton>
    </div>
    </div>

    <div class="row mt-1">
    <div class="col-md-12">
    <div class="line border-top"></div>
    </div>
    </div>
    <div class="row mt-2">
    <div class="col-md-6">
    <asp:Label ID="AttendanceCountLabel" runat="server" Text="Total Attendance: 0, Present Attendance: 0, Absent Attendance: 0" CssClass="attendance-label" style="font-size:larger;font-weight:bolder;"></asp:Label>
</div>
       
    <div class="col-md-6 text-end">
      <table width="100%">
      <tr width="100%">
      <td width="28%"></td>
      <td width="18%" align="center">From</td>
      <td width="12%"><asp:TextBox ID="TxtFromDate" runat="server" Font-Size="14px" CssClass="form-control" TextMode="Date"></asp:TextBox></td>
      <td width="10%" align="center">To </td>
      <td width="12%"><asp:TextBox ID="TextBox1" runat="server" Font-Size="14px" CssClass="form-control" TextMode="Date"></asp:TextBox></td>
      <td width="15%" align="center">
      <asp:LinkButton ID="btnAdvanceFilter" runat="server" CssClass="text-black"><i class="fa fa-filter fs-7"></i></asp:LinkButton>
            
      </td>
      </tr>
      </table>
    </div>
    </div>
    
    <div class="row">
    <div class="col-md-12">
    <asp:Panel ID="Panel2" runat="server" Visible="true" ScrollBars="Auto">
<asp:GridView ID="grdPrograms" runat="server" Width="100%" CellPadding="0" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CssClass="table table-bordered">
    <Columns>
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        
        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
    </Columns>
</asp:GridView>




    </asp:Panel>
    </div>
    </div>

   </div>
   </div>
  </div>
 

   
</div>
</asp:Panel>
<asp:Panel ID="pnlViewApplyForLeave" runat="server" Visible="false">
    <div class="form-container">
        <h2>Edit Leave</h2>
        <asp:Label ID="lblApplyDate" runat="server" Text="Apply Date" AssociatedControlID="txtApplyDate"></asp:Label>
        <span style="color: red;">*</span>
        <asp:TextBox ID="txtApplyDate" runat="server" CssClass="date-input" TextMode="Date"></asp:TextBox>

        <asp:Label ID="lblFromDate" runat="server" Text="From Date" AssociatedControlID="txtFromDate"></asp:Label>
        <span style="color: red;">*</span>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="date-input" TextMode="Date"></asp:TextBox>

        <asp:Label ID="lblToDate" runat="server" Text="To Date" AssociatedControlID="txtToDate"></asp:Label>
        <span style="color: red;">*</span>
        <asp:TextBox ID="txtToDate" runat="server" CssClass="date-input" TextMode="Date"></asp:TextBox>

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

        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-button" OnClick="btnSave_Click" />
    </div>
</asp:Panel>
    </div>
    </div>
    </div>
   
    </form>
    
</body>
</html>
