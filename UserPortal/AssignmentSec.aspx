<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssignmentSec.aspx.vb" Inherits="UserPortal_AssignmentSec" %>

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
      .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}  
  .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      text-align:center;
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
    
      
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
   #AssignmentsUpload
   {
       min-height:455px;
       max-height:455px;
       }
       .SubjectName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
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
    <asp:LinkButton ID="backbotton" CssClass="fw-bold text-black p-2" runat="server"><i class="fa-solid fa-arrow-left fs-7"></i> 
    </asp:LinkButton>
    </td>
    <td width="94%">
    <h5 class="enquiry">&nbsp;&nbsp;Assignment Files</h5>
    </td>
    </tr>
    </table>    
    </div>
    <div class="col-md-6 text-end">
       
    </div>
    </div>
    <div class="row mt-1">
    <div class="col-md-12">
    <div class="line border-top"></div>
    </div>
    </div>

    <div class="row mt-2">
    <div class="col-md-12">
     <asp:Panel ID="PnlAssignment" runat="server" Visible="true" ScrollBars="Auto">


        <asp:GridView ID="GridAssignment" GridLines="Both" Width="100%" AutoGenerateColumns="False"
            runat="server" CellPadding="0" ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-sm" HorizontalAlign="Center">
            <Columns>
                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Assignment Name" DataField="Doc" ></asp:BoundField>
                <asp:BoundField HeaderText="AssignmentNo" DataField="AssignmentNo"></asp:BoundField>
                <asp:BoundField HeaderText="Subject" DataField="Subject"></asp:BoundField>
                <asp:BoundField HeaderText="Submission Date" DataField="SubmissionDate"></asp:BoundField>
                <asp:TemplateField HeaderText="Download Assignment" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="namelink1" Text='<%# Eval("Doc") %>' CssClass="SubjectName" CommandArgument='<%# Eval("Path") %>' OnClick="DownloadFile"  runat="server" >
                   
                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Upload Assignment"  ItemStyle-HorizontalAlign="center" >
                    <ItemTemplate>
                        <asp:LinkButton ID="btnOpenPopup" runat="server" CommandName="OpenPopup" CssClass="SubjectName" CommandArgument='<%# Eval("uploadassignmentid") %>'><i class="fa-solid fa-plus"></i></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View Assignment" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnassignment1"  runat="server" CommandName="viewassignment"  CssClass="SubjectName"  CommandArgument='<%# Eval("uploadassignmentid") %>'><i class="fa-solid fa-eye"></i></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" />
        </asp:GridView>

    </asp:Panel>
    </div>
    </div>
    
   </div>
   </div>
  </div>
 

   
</div>
</asp:Panel>
 
    </div>
    </div>
    </div>
   <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header p-2">
        <h5 class="modal-title" id="H1">
            <asp:Label ID="Label1" ForeColor="White" runat="server" Text="Add Assignment"></asp:Label>
        </h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="row mb-2">
        <div class="col-md-1"></div>
        <div class="col-md-10">
        
     <%--   <div class="row">
           <div class="col-md-4">
             <asp:Label ID="Label4"  runat="server" Text="Assignment No :"></asp:Label>
           </div>
           <div class="col-md-8">           
         <asp:DropDownList ID="ddlAssignNo" runat="server" class="form-select">
         <asp:ListItem Text="1" Value="1"></asp:ListItem>
         <asp:ListItem Text="2" Value="2"></asp:ListItem>
         </asp:DropDownList>
           </div>
        </div>
        <div class="row mt-2">
           <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text="Topic :"></asp:Label>
           </div>
           <div class="col-md-8">
           <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
           </div>
        </div>
     
        <div class="row mt-2">
           <div class="col-md-4">
           <asp:Label ID="Label6" runat="server" Text="Submission Date :"></asp:Label>
           </div>
           <div class="col-md-8">
            <asp:TextBox ID="txtSubmissionDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
           </div>
        </div>--%>
           <div class="row mt-2">
           <div class="col-md-4">
            <asp:Label ID="Label3" runat="server" Text="Documents :"></asp:Label>
           </div>
           <div class="col-md-8">
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn"/>
           </div>
        </div>
        

       
         
        
             
              
          
         
           
        </div>
        <div class="col-md-1"></div>
      </div>
      </div>
      <div class="modal-footer">
        
          <asp:Button ID="Button1" runat="server" class="btn Submit" Text="Upload" />
        
      </div>
    </div>
  </div>
</div>
<script type="text/javascript">
    function OpenBootstrapModal(dataValue) {
        // Assuming your modal has the ID 'myModal'
        $('#myModal').modal('show');

        // You can also update the modal content based on the dataValue if needed
        // For example, assuming there's an element with ID 'modalContent' in your modal
        // document.getElementById('modalContent').innerHTML = "Data from grid: " + dataValue;
    }
</script>

     <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
  
    </form>
<%--   function openModal(gridValue) {
    // Modify this function based on your requirements
    // Here, I'm assuming the modal has an element with ID "modalContent" to update its content
    document.getElementById("modalContent").innerHTML = "Clicked value from grid: " + gridValue;
    $('#myModal').modal('show');
}--%> 
</body>
</html>
