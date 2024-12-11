<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DashboardStu.aspx.vb" Inherits="UserPortal_DashboardStu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
<title>Dashboard</title>
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
                      <asp:LinkButton ID="lbtngrievence" runat="server" class="nav-item nav-link">
                    <i class="fa fa-user-circle"></i>&nbsp;Grievence
                    </asp:LinkButton>
                    <asp:LinkButton ID="lblbtnApplyForLeave" runat="server" class="nav-item nav-link">
                    <i class="fa-solid fa-shop"></i>&nbsp;Apply For Leave
                    </asp:LinkButton>
                </div>
                <div class="navbar-nav ms-auto">
                <ul class="navbar-nav flex-row align-items-start justify-content-start">
                <li class="nav-item dropdown">
                <asp:LinkButton ID="btnNotification" runat="server" class="nav-link nav-icon-hover" data-bs-toggle="dropdown" aria-expanded="false">
                <i class="far fa-bell"></i>&nbsp;
                <asp:Label ID="lblNotificataion" runat="server" CssClass="fs-2"><div class="notification bg-primary rounded-circle"></div></asp:Label>  
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
    <div class="container-fluid mt-2">
    <div class="row p-1">
     <div class="col-md-3">
      <div class="card border-1">
      <div class="card-body p-3">
       <table width="100%">
        <tr width="100%">
        <td width="30%">
        <div class="profileimage">
        <asp:Image ID="Imgprofile" runat="server" class="rounded-circle" ImageUrl="~/assets/images/user.png" Height="100px" Width="100px" />
        </div>
        </td>
        <td width="70%">
        <div class="ms-3">
        <span class="h4 fullname fs-5 fw-bold">Hey, <asp:Label ID="Lblstuname" class="fs-5 fw-bold" runat="server" Text="">Shivani</asp:Label> </span><br />
        <span class="h4 fullname" style="margin-top:20px"><asp:Label ID="lblRollNo" class="fs-6" runat="server" Text="100180168"></asp:Label></span>
        </div>
        </td>
        </tr>
        </table>
   
   <div class="row">
   <div class="col-md-12 mb-1 mt-2">
   <label class="fw-bold text-dark mb-1">Program :</label><br />
   <asp:Label ID="lblProgram" runat="server">B.Tech in Computer Science and Engineering</asp:Label>
   </div>
   <div class="col-md-12 mb-1">
   <label class="fw-bold text-dark mb-1">DOB :</label><br />
   <asp:Label ID="lbldob" runat="server">10-12-1997</asp:Label>
   </div>
   <div class="col-md-12 mb-1">
   <label class="fw-bold text-dark mb-1">Mobile No :</label><br />
   <asp:Label ID="Label2" runat="server">7854612532</asp:Label>
   </div>
   <div class="col-md-12 mb-1">
   <label class="fw-bold text-dark mb-1">Email ID :</label><br />
   <asp:Label ID="Label3" runat="server" CssClass="mb-1">unknowndepart@gmail.com</asp:Label>
   </div>
   <div class="col-md-12 mb-1">
   <label class="fw-bold text-dark mb-1">Address :</label><br />
   <asp:Label ID="Label4" runat="server">Ghost road chauraha, U.P. Meerut, 250004</asp:Label>
   </div>
 
   <div class="col-md-12 mt-2 text-center border-top pt-3">
       <asp:LinkButton ID="btnUpdateProfile" runat="server" Width="100%" BackColor="#1ed085" BorderColor="#1ed085" CssClass="btn btn-primary"><i class="fa fa-edit"></i>&nbsp;Update Profile</asp:LinkButton>
   </div>
   </div>
      </div>
      </div>
     </div>
     <div class="col-md-9">
    <div class="row">
     <div class="col-lg-4">
    <asp:LinkButton ID="btnRevolution" runat="server" CssClass="hoverCard1">
         <div class="card hoverCard border-1">
        <div class="card-body p-4">
        <div class="row align-items-center">
            <div class="col-10">
            <h5 class="card-title mb-9 fw-semibold">
                Revolution Dashboard
            </h5>
            <h6 class="fw-semibold mb-3" style="line-height:1.6;"> 
            Click here to go for revolution dashboard/time-table</h6>
                            
            </div>
            <div class="col-2">
            <div class="d-flex justify-content-center border-1">
                <i class="fas fa-light fa-building fs-11"></i>
            </div>
            </div>
        </div>
        </div>               
     </div>
    </asp:LinkButton>
    </div>
    <div class="col-lg-4">
    <asp:LinkButton ID="btnExamDashboard" runat="server" CssClass="hoverCard1">
        <div class="card hoverCard border-1">
            <div class="card-body p-4">
                <div class="row align-items-center">
                    <div class="col-10">
                        <h5 class="card-title mb-9 fw-semibold">
                            Exam Dashboard
                        </h5>
                        <h6 class="fw-semibold mb-3" style="line-height: 1.6;">
                            Click here to fill your examination form</h6>
                    </div>
                    <div class="col-2">
                        <div class="d-flex justify-content-center border-1">
                            <i class="fas fa-solid fa-book-open fs-11"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:LinkButton>
    </div>
    <div class="col-lg-4">
    <asp:LinkButton ID="btnPaymentSec" runat="server" CssClass="hoverCard1">
    <div class="card hoverCard border-1">
        <div class="card-body p-4">
            <div class="row align-items-center">
                <div class="col-10">
                    <h5 class="card-title mb-9 fw-semibold">
                        Pay Fee/ Dues Fee
                    </h5>
                    <h6 class="fw-semibold mb-3" style="line-height: 1.6;">
                        Click here to pay your old dues and examination fee</h6>
                </div>
                <div class="col-2">
                    <div class="d-flex justify-content-center border-1">
                        <i class="fas fa-regular fa-credit-card fs-11"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:LinkButton>
    </div>
    </div>
    <div class="row" style="margin-top:-14px;">
     <div class="col-lg-4">
    <asp:LinkButton ID="btnAssignmentFile" runat="server" CssClass="hoverCard1">
         <div class="card hoverCard border-1">
        <div class="card-body p-4">
        <div class="row align-items-center">
            <div class="col-10">
            <h5 class="card-title mb-9 fw-semibold">
                Assignments
            </h5>
            <h6 class="fw-semibold mb-3" style="line-height:1.6;"> 
            Click here to go for revolution dashboard/time-table</h6>
                            
            </div>
            <div class="col-2">
            <div class="d-flex justify-content-center border-1">
                <i class="fa fa-upload fs-11"></i>
            </div>
            </div>
        </div>
        </div>               
     </div>
    </asp:LinkButton>
    </div>
    <div class="col-lg-4">
    <asp:LinkButton ID="btnCertificateDoc" runat="server" CssClass="hoverCard1">
        <div class="card hoverCard border-1">
            <div class="card-body p-4">
                <div class="row align-items-center">
                    <div class="col-10">
                        <h5 class="card-title mb-9 fw-semibold">
                            Certificate/Document
                        </h5>
                        <h6 class="fw-semibold mb-3" style="line-height: 1.6;">
                            Click here to download your TC, Bonafide, and other certificates</h6>
                    </div>
                    <div class="col-2">
                        <div class="d-flex justify-content-center border-1">
                            <i class="fa-solid fa-certificate fs-11"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:LinkButton>
    </div>
    <div class="col-lg-4">
    <asp:LinkButton ID="btnjobs" runat="server" CssClass="hoverCard1">
    <div class="card hoverCard border-1">
        <div class="card-body p-4">
            <div class="row align-items-center">
                <div class="col-10">
                    <h5 class="card-title mb-9 fw-semibold">
                        Job's/Placement
                    </h5>
                    <h6 class="fw-semibold mb-3" style="line-height: 1.6;">
                        Click here to view applying job status and apply for jobs</h6>
                </div>
                <div class="col-2">
                    <div class="d-flex justify-content-center border-1">
                        <i class="fa-solid fa-briefcase fs-11"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:LinkButton>
    </div>
    </div>
    <div class="row" style="margin-top:-14px;">
    <div class="col-lg-4">
    <asp:LinkButton ID="btnStudyMaterials" runat="server" CssClass="hoverCard1">
         <div class="card hoverCard border-1">
        <div class="card-body p-4">
        <div class="row align-items-center">
            <div class="col-10">
            <h5 class="card-title mb-9 fw-semibold">
                Study Materials
            </h5>
            <h6 class="fw-semibold mb-3" style="line-height:1.6;"> 
            Click here to download and get ready for learn something new and cazy by experienced professionals.</h6>
                            
            </div>
            <div class="col-2">
            <div class="d-flex justify-content-center border-1">
                <i class="fa fa-download fs-11"></i>
            </div>
            </div>
        </div>
        </div>               
     </div>
    </asp:LinkButton>
    </div>
      <div class="col-lg-4">
    <asp:LinkButton ID="btnQuizzes" runat="server" CssClass="hoverCard1">
         <div class="card hoverCard border-1">
        <div class="card-body p-4">
        <div class="row align-items-center">
            <div class="col-10">
            <h5 class="card-title mb-9 fw-semibold">
                Quizzes
            </h5>
            <h6 class="fw-semibold mb-3" style="line-height:1.6;"> 
            Click here to complete quizzes and get ready for evaluate yourself by experienced professionals.</h6>
                            
            </div>
            <div class="col-2">
            <div class="d-flex justify-content-center border-1">
                <i class="fa fa-question-circle fs-11"></i>
            </div>
            </div>
        </div>
        </div>               
     </div>
    </asp:LinkButton>
    </div>
    </div>
    <div class="row" style="margin-top:-14px;">
     <div class="col-lg-12">
         <div class="card border-1 rounded-1" style="height:380px">
        <div class="card-body p-3">
        <div class="row align-items-center">
            <div class="col-12">
            <h5 class="card-title mb-9 fw-semibold border-bottom pb-1">
                Notice/Announcement  &nbsp;<i class="fa-solid fa-bullhorn rounded-circle bg-primary-subtle p-2"></i>
            </h5>                 
            </div>
           <div class="col-12">
           <marquee onMouseOver="this.stop()"  direction="down" onMouseOut="this.start()" style="color:#152837;" scrollamount="2">
                    <ul class="ul">
                    <li>
                    Note: all the property values listed above can be used for styling both ordered and unordered lists (ex: an ordered list with square list markers).
                    </li>
                     <li>
                    Note: all the property values listed above can be used for styling both ordered and unordered lists (ex: an ordered list with square list markers).
                    </li>
                     <li>
                    Note: all the property values listed above can be used for styling both ordered and unordered lists (ex: an ordered list with square list markers)&nbsp; <a href="../AdmStuFinalCSS/Notice.pdf" style="font-size:22px; color:#1ed085;" download><i class="fa-regular fa-file-pdf"></i></a> .
                    </li>
                      <li>
                    Note: all the property values listed above can be used for styling both ordered and unordered lists (ex: an ordered list with square list markers)&nbsp; <a href="../AdmStuFinalCSS/Notice.pdf" style="font-size:22px; color:#1ed085;" download><i class="fa-regular fa-file-pdf"></i></a> .
                    </li>
                      <li>
                    Note: all the property values listed above can be used for styling both ordered and unordered lists (ex: an ordered list with square list markers)&nbsp; <a href="../AdmStuFinalCSS/Notice.pdf" style="font-size:22px; color:#1ed085;" download><i class="fa-regular fa-file-pdf"></i></a> .
                    </li>
                    </ul>
                    </marquee>
           </div>
        </div>
        </div>               
     </div>    
    </div>
   
    </div>
    </div>
    </div>
    </div>
    </form>
    
</body>
</html>
