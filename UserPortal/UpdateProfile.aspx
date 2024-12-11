<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdateProfile.aspx.vb" Inherits="UserPortal_UpdateProfile" %>

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
    .crdcardmain
    {
        
         border-radius:4px;
        color:#152837;
        line-height:1.6;
         background:#fff;
         
        }
      .crdcardmain:hover
    {
        border-radius:4px;
         background:#fff;
         color:#152837;
        }
     .Submit
    {
   color:#1ed085;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:1px solid #1ed085; 
   background-color:#fff;
   text-decoration:none;
   text-align :center;
   border-radius:4px;
   padding:2px 10px;
   width:100%;
    }
    .Submit:hover
    {
   color:#fff;
   background-color:#1ed085;
   text-decoration:none;
    }
   .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:6px;
       margin-bottom:9px;
       
   }
   
 
     
    </style>
 <style type="text/css" >

   
   .maincontainer {
    border: 2px solid #fff;
    padding: 2px 2px 2px 2px;
  /*  box-shadow:1px 2px 2px 2px rgba(0, 0, 0, 0.4);*/
    border-radius: 6px;
    text-align: left;
   }
   
    .maincontainerm
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:42px;
    height:41px ;
    border-radius:50%;
    padding-top:8px;
  }
   .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:4px;
       margin-bottom:8px;
       
   }
    .btnAddProgram
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    }
 .btnAddProgram:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
    }
     .btnaddDecription
{
    background-color:#808080;
    color:#f2f3f5;
    font-weight:600;
    font-size:18px;
   text-align:center;
   border-radius:50%;
    }
 .btnaddDecription:hover
{
    background-color:#000;
    color:#f2f3f5;
    font-weight:500;
    }

    #backtoprogram
  {
    font-size:24px;
    font-weight:600;
    color:#7c858f;
}
#backtoprogram:hover
{
    color:#15283c;
}
#backtocourseSubject{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#backtocourseSubject:hover
{
    color:#15283c;
}

 .communicatesub
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    }
 .communicatesub:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
    }
  .btncanelg
  {
      margin-left:270px;
      }
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}
.SubjectName
   {
       color:#1ed085;
       text-decoration:none;
       font-size:18px;
       font-weight:400;
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
     
    }
    .hiddencol
    {
        display:none;
    }
    .ddlSemyear
    {
        border:1px solid gray;
        border-radius:3px;
        
    }
   .backbotton
        {
            font-size: 20px;
            font-weight: 600;
            color: #000;
            background-color:#eee;
            padding:5px 10px 7px 10px;
            border-radius:50%;
            text-decoration: none;
            box-shadow: 0px 5px 10px 0px rgba(0, 0, 0, 0.5)
        }
        
        .backbotton:hover
        {
            color: #15283c;
            text-decoration: none;
        }
 .addsubject, #btnsatephoto1
    {
   color:#15283c;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:1px solid #15283c; 
   background-color:#fff;
   text-decoration:none;
   text-align :center;
   border-radius:4px;
   padding:3px 10px;
    }
    .addsubject, #btnsatephoto1:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }
 
     .Submit, #btnsavepersonal, #btneducation, #btnfamilysave, #btncontact, #btnaccount, #btnsatephoto
 {
     font-size: 18px !important;
      font-weight: 500;
      text-align:center;
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      padding:3px 10px;
      width:15%;
      margin-top:8px;
      margin-bottom :5px;
      
   }
   .Submit, #btnsavepersonal, #btneducation, #btnfamilysave, #btncontact, #btnaccount, #btnsatephoto:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }

     
    .Profiledetail .h4
    {
        color:#fff;
    font-size:18px;
    font-weight:400;

    }
    .Profiledetail .fullname
    {
        font-weight:500;
    }
    .Profiledetail .h5
    {
        position:relative;
    }
.Profiledetail .h5 .lblprogram
    {
        color:#fff;
    font-size:16px;
    font-weight:400;

    }
    

    .tablefield tr td
    {
        padding-top:5px;
    }

.paddingtop
{
    padding-top:16px;
}
.Subheading
{
    position :relative;
}
.backicon
{
    position:absolute;
    top:30%;
    padding-right:15px;
}
    </style>
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
</style> 
<style>


#progressbar {
    margin-bottom: 8px;
    overflow: hidden;
    color: lightgrey;
    text-align:center;
}

#progressbar .active {
    color: #673AB7
}

#progressbar li {
    list-style-type: none;
    font-size: 15px;
    width: 16.66%;
    float: left;
    position: relative;
    font-weight: 400
}

#progressbar #account:before {
    font-family: FontAwesome;
    content: "\f13e"
}

#progressbar #personal:before {
    font-family: FontAwesome;
    content: "\f007"
}

#progressbar #Family:before {
    font-family: FontAwesome;
    content: "\e537"
}

#progressbar #educational:before {
    font-family: FontAwesome;
    content: "\f19d"
}


#progressbar #Address:before {
    font-family: FontAwesome;
    content: "\f2bb"
}

#progressbar #UploadFile:before {
    font-family: FontAwesome;
    content: "\f574"
}

#progressbar li:before {
    width: 45px;
    height: 45px;
    line-height: 40px;
    display: block;
    font-size: 19px;
    color: #ffffff;
    background: #15283c;
    border-radius: 50%;
    margin: 0 auto 5px auto;
    padding: 1px
}
.Inactive li:before{
    width: 50px;
    height: 50px;
    line-height: 45px;
    display: block;
    font-size: 20px;
    color: #fff;
    background: #15283c;
    border-radius: 50%;
    margin: 0 auto 10px auto;
    padding: 2px;
}

#progressbar li:after {
    content: '';
    width: 100%;
    height: 2px;
    background: #15283c;
    position: absolute;
    left: 0;
    top: 25px;
    
    z-index: -1
}

#progressbar li.active:before,
#progressbar li.active:after {
    background:lightgray;
    
}

.progress {
    height: 20px
}

.progress-bar {
    background-color: #673AB7
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
                    <asp:LinkButton ID="btnMyProfileChange" runat="server" class="nav-item nav-link">
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
    <asp:LinkButton ID="backbutton" CssClass="fw-bold text-black p-2" runat="server"><i class="fa-solid fa-arrow-left fs-7"></i> 
    </asp:LinkButton>
    </td>
    <td width="94%">
    <h5 class="enquiry">&nbsp;&nbsp;Update Profile</h5>
    </td>
    </tr>
    </table>    
    </div>
    <div class="col-md-6 text-end">
       
    </div>
    </div>
    <div class="row mt-1">
    <div class="col-md-12">
    <div class="border-top"></div>
    </div>
    </div>

    <div class="row profileheader" style="display:none">
                          <div class="col-md-10">
                          
                          <div class="Subheading d-flex ">

                          <div class="backicon">
                            <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i>  &nbsp &nbsp &nbsp</asp:LinkButton> 
                          </div>  
                          &nbsp &nbsp &nbsp
                          <div class="profileimage">
                              <asp:Image ID="Imgprofile" runat="server" class="imageicon" ImageUrl="~/img/collegelogo.jpg"  />
                          </div>
                          &nbsp &nbsp &nbsp
                         <div class="Profiledetail">
                       
                     
                        <asp:Label ID="Lblstuname" class="h4 fullname" runat="server" Text="Full Name"></asp:Label> 
                             <asp:Label ID="Label1" class="h4" runat="server" Text="("></asp:Label><asp:Label ID="Lblstugender" class="h4" runat="server" Text="Label"></asp:Label><asp:Label
                                 ID="Label60" class="h4" runat="server" Text=")"></asp:Label>
                       
                   <div class="h5" > 
                       <asp:Label ID="ddlcourse" class="lblprogram" runat="server" Text="Program Name"></asp:Label>(<asp:Label ID="Lblcoursecode" runat="server" Text="code"></asp:Label>)
                       
                      
                       <div class="admission d-flex lblprogram" >
                       Admission No:&nbsp<asp:Label ID="LbladmissionNo" runat="server" Text="Label"></asp:Label>
                       </div>
                         </div>
                         </div>
                              </div> 
                             
                          </div>
    </div>
   <div class="row mt-2 mb-1">
        <ul id="progressbar">
                       <asp:LinkButton ID="LnkPersonal" runat="server">
                        <li id="personal" runat="server" ><strong id="PerNAme" runat="server">Personal</strong></li>
                        <asp:LinkButton ID="Lnkeducation" runat="server">
                        <li id="educational" runat="server"><strong>Educational</strong></li>
                        </asp:LinkButton>
                       </asp:LinkButton>                       
                        <asp:LinkButton ID="lnkFamily" runat="server">
                        <li id="Family" runat="server"><strong>Family</strong></li>
                        </asp:LinkButton>
                      
                        <asp:LinkButton ID="LnkContact" runat="server">
                        <li id="Address" runat="server"><strong>Address</strong></li>
                        </asp:LinkButton>
                      <asp:LinkButton ID="LnkAccount" runat="server">
                         <li id="account" runat="server"><strong>Account</strong></li>
                        </asp:LinkButton>
                        <asp:LinkButton ID="Lnkphotos" runat="server">
                        <li id="UploadFile" runat="server"><strong>Upload File</strong></li>
                        </asp:LinkButton>
          </ul>   
       </div>                
    <div class="hiddencol">
         <txtstu:textbox ID="stusearch" class="form-control" runat="server"></txtstu:textbox>
 </div>

    
 <%-- -------------------   Personal Detail------------------------------------------ --%>   
       <asp:Panel ID="Pnlpersonal" Visible="true" CssClass="border-top" runat="server">
         <div class="row mt-2">

         <div class="col-md-12">
         
                                        <div class="row">

                                        
                                        <div class="col-md-6">
                                        <table width="100%" class="tablefield">
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label2" class="lablfield" runat="server" Text="Name"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="TxtFirstname" placeholder="First Name" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        <td width="10%"></td>
                                        <td width="30%">
                                           <asp:TextBox ID="Txtlastname" placeholder="Last Name" class="form-control" runat="server"></asp:TextBox>
                                       
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label3" class="lablfield" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                         <asp:DropDownList ID="ddlgender" class="form-select" runat="server">
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label4" class="lablfield" runat="server" Text="Date Of Birth :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                             <datecrtwithpostback:textbox ID="TxtDob" class="form-control" width="100%" runat="server">
                                                </datecrtwithpostback:textbox>
                                            
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label5" class="lablfield" runat="server" Text="Mobile :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                            <asp:TextBox ID="txtcorrmobile" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label6" class="lablfield" runat="server" Text="Email :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                            <asp:TextBox ID="txtcorrEmail" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label7" class="lablfield" runat="server" Text="Nationality :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                            <asp:TextBox ID="TxtNationality" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label14" class="lablfield" runat="server" Text="Martial Status :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                          <asp:DropDownList AppendDataBoundItems="true" class="form-select" ID="ddlmarital"
                                                    runat="server">
                                                    <asp:ListItem Value=""  Text="Select">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Unmarried">Unmarried
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="married">married</asp:ListItem>
                                                </asp:DropDownList>  
                                         </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label19" class="lablfield" runat="server" Text="student Category :"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                       <asp:DropDownList ID="ddlstudentmode"  AppendDataBoundItems="true" runat="server"  class="form-control" >
                                                                </asp:DropDownList>
                                         </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label25" class="lablfield" runat="server" Text="Adhar No:"></asp:Label>
                                        </td>
                                        <td width="70%" colspan="3">
                                              <asp:TextBox ID="txtadharno" runat="server" class="form-control"></asp:TextBox>
                                         </td>
                                        </tr>
                                       
                                        </table>
                                        
                                        </div>

                                        <div class="col-md-6">
                                        <table width="100%" class="tablefield">

                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label8" class="lablfield" runat="server" Text="Religion :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:DropDownList ID="ddlreligion" class="form-select" runat="server" AppendDataBoundItems="True">
                                             
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label9" class="lablfield" runat="server" Text="Caste Category :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                       <asp:DropDownList ID="DdlCategoryc" class="form-select" runat="server" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label10" class="lablfield" runat="server" Text="Caste :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtcaste" placeholder="Caste" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                          <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label17" class="lablfield" runat="server" Text="Hobbies :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="TxtHobbies" placeholder="Hobbies" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                      
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label11" class="lablfield" runat="server" Text="Height :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="TxtHeight" placeholder="Height" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>

                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label12"  class="lablfield" runat="server" Text="Weight :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="TxtWeight" placeholder="Weight" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label13" class="lablfield" runat="server" Text="Vision :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                          <asp:DropDownList ID="DdlVision" runat="server" class="form-select">
                                                     <asp:ListItem Value="">Select</asp:ListItem>
                                                   
                                                    <asp:ListItem Value="Good">Good</asp:ListItem>
                                                    <asp:ListItem Value="High" >High</asp:ListItem>
                                                    <asp:ListItem Value="Low">Low</asp:ListItem>
                                                </asp:DropDownList>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label15" class="lablfield" runat="server" Text="Physical Disability :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                        <asp:TextBox ID="TxtPassport" placeholder="Physical disability" runat="server" class="form-control"></asp:TextBox>
                                           
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label16" class="lablfield" runat="server" Text="Blood Group :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                      <asp:DropDownList ID="DdlBlood" runat="server" class="form-select">
                                                    <asp:ListItem Value="">Select</asp:ListItem>
                                                    <asp:ListItem Value="A">A</asp:ListItem>
                                                    <asp:ListItem Value="A-">A-</asp:ListItem>
                                                    <asp:ListItem Value="A+">A+</asp:ListItem>
                                                    <asp:ListItem Value="AB">AB</asp:ListItem>
                                                    <asp:ListItem Value="AB-">AB-</asp:ListItem>
                                                    <asp:ListItem Value="AB+">AB+</asp:ListItem>
                                                    <asp:ListItem Value="B">B</asp:ListItem>
                                                    <asp:ListItem Value="B-">B-</asp:ListItem>
                                                    <asp:ListItem Value="B+">B+</asp:ListItem>
                                                    <asp:ListItem Value="O">O</asp:ListItem>
                                                    <asp:ListItem Value="O-">O-</asp:ListItem>
                                                    <asp:ListItem Value="O+">O+</asp:ListItem>
                                                </asp:DropDownList>
                                         </td>
                                        </tr>
                                       
                                        </table>
                                        
                                        </div>

                                       
                                       
                  
                    
                                        </div>

                                        <div class="row">
                                        <div class="col-md-12 d-flex justify-content-center ">
                                          <asp:Button ID="btnsavepersonal" class="btn Submit" runat="server" Text="Update" />
                                          
                                        </div>
                                        </div>
                                     
                              </div>
         
         </div>
         </asp:Panel>
      

      
 <%-- -------------------   Education Detail------------------------------------------ --%>  
                       
         <asp:Panel ID="Pnleducation" Visible="False" runat="server">
         
          <div class="row mt-3">

         <div class="col-md-12">
         <div class="row">
         <div class="col-md-6">
         <table with="100%" class="tablefield">
         <tr width="100%">
         <td width="30%">
             <asp:Label ID="Label18" runat="server" Text=" Admission Date :"></asp:Label>
         </td>
         <td width="30%">
             <asp:Label ID="txtdated" runat="server" Text="txtdated"></asp:Label>
         </td>
         </tr>
         <tr width="100%">
         <td width="30%">
             <asp:Label ID="Label20" runat="server" Text=" Admission Year :"></asp:Label>
         </td>
         <td width="30%">
             <asp:Label ID="ddlacademicyear" runat="server" Text="N/A"></asp:Label>
         </td>
         </tr>
         <tr width="100%">
         <td width="30%">
             <asp:Label ID="Label22" runat="server" Text="Registration No :"></asp:Label>
         </td>
         <td width="30%">
             <asp:Label ID="TxtAdmNo" runat="server" Text="N/A"></asp:Label>
         </td>
         </tr>
         </table>
         </div>
         <div class="col-md-6">
         <table with="100%" class="tablefield">
         <tr width="100%">
         <td width="30%">
             <asp:Label ID="Label21" runat="server" Text=" Enrollment No :"></asp:Label>
         </td>
         <td width="30%">
             <asp:Label ID="txtenrolment" runat="server" Text="N/A"></asp:Label>
         </td>
         </tr>
         <tr width="100%">
         <td width="30%">
             <asp:Label ID="Label24" runat="server" Text="Roll No :"></asp:Label>
         </td>
         <td width="30%">
             <asp:Label ID="txtsturollno" runat="server" Text="Not available"></asp:Label>
         </td>
         </tr>
         
         </table>
         </div>
         </div>


         <div class="row">
         <div class="col-md-12 mt-2">
         <div class="row">
                                            <asp:SqlDataSource ID="sdsquali" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT [Qualification] FROM [Qualification]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="board" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT FieldValue  AS BOARD  FROM LovUserEnglish WHERE MasterListingID=  48 ORDER BY FieldValue">
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="university" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT FieldValue  AS BOARD  FROM LovUserEnglish WHERE MasterListingID=  61 ORDER BY FieldValue">
                                            </asp:SqlDataSource>
                                            <asp:GridView ID="GrdEdu" CssClass="table table-bordered table-sm" runat="server" AutoGenerateColumns="False"
                                                GridLines="Horizontal" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSno" runat="server" Text="<%#Container.DataItemIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField  HeaderText="Rid" DataField="Rid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"/>
                                                    <asp:TemplateField HeaderText="Qualification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQual" runat="server" Font-Bold="true" DataSourceID="sdsquali" Text='<%#Eval("Qualification")%>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlquli"  Width="130px"
                                                                AppendDataBoundItems="True" runat="server" DataSourceID="board" DataTextField="BOARD"
                                                                DataValueField="BOARD" >
                                                                <asp:ListItem  Text="Select"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="University">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddluni"  Width="130px"
                                                                AppendDataBoundItems="True" runat="server" DataSourceID="university" DataTextField="BOARD"
                                                                DataValueField="BOARD" >
                                                                <asp:ListItem   Text="Select"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Stream">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TxtStream" Text='<%#Eval("Stream")%>' runat="server" Width="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Txtyear" Text='<%#Eval("PassingYear")%>' runat="server" Width="50px">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roll NO ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRoll_no" Text='<%#Eval("Roll_No")%>' runat="server" Width="80px">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" HeaderText="Institute ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtinstitute" Text='<%#Eval("Institution")%>'  runat="server" Width="80px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MM ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TxtMM" runat="server" Text='<%#Eval("MM")%>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Marks Obt.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Txtobt" Text='<%#Eval("Marks")%>' runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="%age">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Txtpercentage" Text='<%#Eval("Percentage")%>' runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PCM %">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtpcm" Text='<%#Eval("PCM")%>' runat="server" Width="50px"></asp:TextBox></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phy.%" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtphy" Text='<%#Eval("phy")%>' runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chem.%" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtchem" Text='<%#Eval("chem")%>' runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Maths %" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmaths" Text='<%#Eval("maths")%>' runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eng.%" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Txteng" Text='<%#Eval("english")%>' runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade/CGPA.%" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Txtgrade" Text='<%#Eval("Grade")%>'   runat="server" Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    Batches Not Available</EmptyDataTemplate>
                                                <%--  <FooterStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                        </div>
     
     <div class="row">
     
        <div class="col-md-12 d-flex justify-content-center ">
                                                <asp:Button ID="btneducation" runat="server" class="btn Submit" Text="Update" />
                                            </div>

    </div>
         </div>
         
         </div>

         </div> 
         </div> 

         </asp:Panel> 
         
         
          <%---------------------Family Detail------------------------------------------ --%>    
             

             <asp:Panel ID="PnlFamily" Visible="false" runat="server">
         <div class="row mt-3">

         <div class="col-md-12">
         
                                        <div class="row">

                                        <div class="col-md-6">
                                        <table width="100%" class="tablefield">
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label23" class="lablfield" runat="server" Text="Father's Name"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="TxtFname" Placeholder="Father's name" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label26" class="lablfield" runat="server" Text="Father's Occupation"></asp:Label>
                                        </td>
                                        <td width="70%">
                                          <asp:DropDownList ID="ddlFOccupation" class="form-select" runat="server">
                                                </asp:DropDownList>
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label27" class="lablfield" runat="server" Text="Father's Income :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                              <asp:TextBox ID="Txtincome" Placeholder="Father's income" runat="server" class="form-control"></asp:TextBox>
                                             
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label28" class="lablfield" runat="server" Text="Father's Mobile :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtFmobile" Placeholder="Father's Mobile" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label29" class="lablfield" runat="server" Text="Mother's Name :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="Txtmothername" Placeholder="Mother's name" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label30" class="lablfield" runat="server" Text="Mother's Occupation :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                         <asp:DropDownList ID="ddlMOccupation" class="form-select" runat="server">
                                                </asp:DropDownList>
                                        </td>
                                        </tr>
                                       <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label31" class="lablfield" runat="server" Text="Mother's Income:"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="TxtMincome" Placeholder="Mother's Income" class="form-control" runat="server"></asp:TextBox>
                                         </td>
                                        </tr>
                                        </table>
                                        
                                        </div>

                                        <div class="col-md-6">
                                        <table width="100%" class="tablefield">
                                         
                                        
                                       
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label33" class="lablfield" runat="server" Text="Guardian Name :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                             <asp:TextBox ID="txtGaurdianname" Placeholder="Guardian's Name" class="form-control" runat="server"></asp:TextBox>
                                            
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label34" class="lablfield" runat="server" Text="Relation :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                       <asp:TextBox ID="txtGaurdianrelation" Placeholder="Relation" class="form-control" runat="server"></asp:TextBox>
                                           
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label35" class="lablfield" runat="server" Text="Guardian's Mobile :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                          <asp:TextBox ID="txtGaurdianmobile" Placeholder="Guardian's Mobile No" class="form-control" runat="server"></asp:TextBox>
                                            
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label36" class="lablfield" runat="server" Text="Guardian's Email :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                          <asp:TextBox ID="txtGaurdianemail" Placeholder="Guardian's Email" class="form-control" runat="server"></asp:TextBox>
                                           
                                        </td>
                                        </tr>

                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label37" class="lablfield" runat="server" Text=" LandLine Number :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtGaurdianphone" Placeholder="Landline Number" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label38" class="lablfield" runat="server" Text="Domicile :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                             <asp:TextBox ID="TxtDomocile" Placeholder="Domicile" runat="server" class="form-control"></asp:TextBox>
                                        
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label39" class="lablfield" runat="server" Text="Family Income:"></asp:Label>
                                        </td>
                                        <td width="70%">
                                        <asp:TextBox ID="TxtfamIncome" runat="server" Placeholder="Family income" class="form-control"></asp:TextBox>
                                           
                                        </td>
                                        </tr>
                                        
                                       
                                        </table>
                                        
                                        </div>

                                        
                    
                                        </div>

                                        <div class="row">
                                        <div class="col-md-12 d-flex justify-content-center ">
                                          <asp:Button ID="btnfamilysave" class="btn Submit" runat="server" Text="Update" />
                                          
                                        </div>
                                        </div>
                                     
                              </div>
         
         </div>
         </asp:Panel>


              <%---------------------Contact Detail------------------------------------------ --%>    
          
                      
     <asp:Panel ID="Pnlcontact" Visible="false" runat="server">
         <div class="row mt-3">

         <div class="col-md-12">
         
                                        <div class="row">
                                        
                                        <div class="col-md-6 " style="padding-right:15px;">

                                        <div class="col-md-12 subheadingc">
                                        <h5>
                                        Correspondance Address
                                        </h5>
                                        <div class="line">
                                        
                                        
                                        </div>
                                        
                                        </div>

                                        <table width="100%" class="tablefield">
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label32" class="lablfield" runat="server" Text="Address 1 :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="textcorraddress1" Placeholder="Address 1" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label40" class="lablfield" runat="server" Text="Address 2 :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                              <asp:TextBox ID="textcorraddress2" placeholder="Address 2" class="form-control" runat="server"></asp:TextBox>
                                              </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label41" class="lablfield" runat="server" Text="Country :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                             <asp:DropDownList ID="ddlcorrcountry" autopostback="true" runat="server" class="form-select">
                                                </asp:DropDownList>
                                                
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label42" class="lablfield" runat="server" Text="State :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                     <asp:DropDownList ID="ddlcorrstate" autopostback="true"  class="form-select" runat="server">
                                                </asp:DropDownList>
                                                     </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label43" class="lablfield" runat="server" Text="District :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                         <asp:DropDownList ID="ddlcordistt" autopostback="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                               </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label44" class="lablfield" runat="server" Text="City :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                      <asp:DropDownList ID="ddlcorrcity" class="form-select " runat="server">
                                                </asp:DropDownList>
                                              </td>
                                        </tr>
                                       <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label45" class="lablfield" runat="server" Text="Pin code :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtcorrpin" Placeholder="Pin code" class="form-control" runat="server"></asp:TextBox>
                                         </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label53" class="lablfield" runat="server" Text="Mobile No :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtcorrphone" Placeholder="Mobile number" class="form-control" runat="server"></asp:TextBox>
                                         </td>
                                        </tr>
                                        </table>
                                        
                                        </div>

                                        <div class="col-md-6" style="padding-left:15px;">
                                        <div class="col-md-12 subheadingc">
                                        <h5>
                                        Permanent Address
                                        </h5>
                                        <div class="line">
                                        
                                        
                                        </div>
                                        
                                        </div>
                                        <table width="100%" class="tablefield">
                                         
                                        
                                       
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label46" class="lablfield" runat="server" Text="Address 1 :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                             <asp:TextBox ID="txtprmadd1" Placeholder="Address 1" class="form-control" runat="server"></asp:TextBox>
                                            
                                            </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label47" class="lablfield" runat="server" Text="Address 2 :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                       <asp:TextBox ID="txtprmadd2" Placeholder="Address 2" class="form-control" runat="server"></asp:TextBox>
                                           
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label48" class="lablfield" runat="server" Text="Country :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                         <asp:DropDownList ID="ddlprmcountry" AutoPostBack="True" runat="server" class="form-select">
                                                </asp:DropDownList>
                                                   
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label49" class="lablfield" runat="server" Text="State :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                             <asp:DropDownList ID="ddlprmsate" AutoPostBack="true" class="form-select" runat="server">
                                                </asp:DropDownList>
                                              
                                        </td>
                                        </tr>

                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label50" class="lablfield" runat="server" Text="District :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                         <asp:DropDownList ID="ddlprmdistt" AutoPostBack="True" class="form-select" runat="server">
                                                </asp:DropDownList>
                                               </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label51" class="lablfield" runat="server" Text="City :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                                <asp:DropDownList ID="ddlprmcity" class="form-select " runat="server">
                                                </asp:DropDownList>
                                        
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label52" class="lablfield" runat="server" Text="Pin code :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                        <asp:TextBox ID="txtprmpin" runat="server" Placeholder="Pin code" class="form-control"></asp:TextBox>
                                           
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label54" class="lablfield" runat="server" Text="Mobile No :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                        <asp:TextBox ID="txtPrmMobile" runat="server" Placeholder="Mobile number" class="form-control"></asp:TextBox>
                                           
                                        </td>
                                        </tr>

                                        
                                       
                                        </table>
                                        
                                        </div>

                    
                                        </div>

                                        <div class="row">
                                        <div class="col-md-12 d-flex justify-content-center ">
                                          <asp:Button ID="btncontact" class="btn Submit" runat="server" Text="Update" />
                                          
                                        </div>
                                        </div>
                                     
                              </div>
         
         </div>
         </asp:Panel>
                       
            <%---------------------Account Detail------------------------------------------ --%>    
             
             <asp:Panel ID="PnlAccount" Visible="False" runat="server">
         <div class="row mt-3">

         <div class="col-md-12">
         
                                        <div class="row">

                                        <div class="col-md-2"></div>
                                        <div class="col-md-8 ">

                                        

                                        <table width="100%" class="tablefield">
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label55" class="lablfield" runat="server" Text="Account No :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                            <asp:TextBox ID="txtaccountno" Placeholder="Account number" class="form-control" runat="server"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label56" class="lablfield" runat="server" Text="Bank Name :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                      <asp:DropDownList ID="ddlbankname" class="form-select" runat="server" AppendDataBoundItems="True">
                                             
                                                </asp:DropDownList>
                                        </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label57" class="lablfield" runat="server" Text="Brach Name :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                              <asp:TextBox ID="txtbranchname" runat="server" Placeholder="Brach Name" class="form-control"></asp:TextBox>
                                                  
                                           </td>
                                        </tr>
                                        <tr width="100%">
                                        <td width="30%">
                                            <asp:Label ID="Label58" class="lablfield" runat="server" Text="IFSC Code :"></asp:Label>
                                        </td>
                                        <td width="70%">
                                          <asp:TextBox ID="txtifsecode"  Placeholder="Ifsc code" runat="server" class="form-control"></asp:TextBox>
                                                           </td>
                                        </tr>
                                        </table>
                                        
                                        </div>

                                        

                                         <div class="col-md-2"></div>
                                       
                  
                    
                                        </div>

                                        <div class="row">
                                        <div class="col-md-12 d-flex justify-content-center ">
                                          <asp:Button ID="btnaccount" class="btn Submit" runat="server" Text="Update" />
                                          
                                        </div>
                                        </div>
                                     
                              </div>
         
         </div>
         </asp:Panel>
         
         
         <asp:Panel ID="Pnlphoto" Visible="false" runat="server">
         <div class="row mt-3">

         <div class="col-md-2"></div>
         <div class="col-md-8 paddingtop">
         
                                        <div class="row">


                             <div class="col-md-4">
                             <div class="row">
                             <div class="col-md-12 subheading">
                             <h4>Photo</h4>
                             <div class="line"></div>
                             </div>
                             </div>
       <asp:Image ID="stuimage" runat="server" Height="90px" Width="80px" BorderColor="Gray"
                                                        BorderStyle="Double" />
                                              
     </div>
     
                             <div class="col-md-4">
                             <div class="row">
                             <div class="col-md-12 subheading">
                             <h4>Signature</h4>
                             <div class="line"></div>
                             </div>
                             </div>
      <asp:Image ID="Stuimagesig" runat="server" Height="90px" Width="80px" BorderColor="Gray"
                                                        BorderStyle="Double" />
                                              
     </div>
                             <div class="col-md-4">
                             <div class="row">
                             <div class="col-md-12 subheading">
                             <h4>Thumb</h4>
                             <div class="line"></div>
                             </div>
                             </div>
          <asp:Image ID="Image1" runat="server" Height="90px" Width="80px" BorderColor="Gray"
                                                        BorderStyle="Double" />
                                               
     </div>

                                        
                                        
                                           
                                                <%--     <input type="text" class="form-control" data-inputmask="'mask' : '(999) 999-9999'">--%>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Image Upload" />
                                                    <asp:Button ID="btnimgdelete" class="hiddencol" runat="server" Text="X" />
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:FileUpload ID="FileUpload2" runat="server" ToolTip="Image Upload" />
                                                    <asp:Button ID="btnsigdelete" class="hiddencol" runat="server" Text="X" />
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:FileUpload ID="FileUpload3" runat="server" ToolTip="Image Upload" />
                                                    <asp:Button ID="btnthumbdelete" class="hiddencol" runat="server" Text="X" />
                                                </div>
                                          
                                       
                                       
                                        <div class="row pb-2 hiddencol" style="display:none;">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <asp:Button ID="btnsatephoto1" class="btn addsubject" runat="server" Text="Save" />
                                            </div>
                                        </div>
                                  
                                        
                                       
                    
                                        </div>

                                        <div class="row mt-4">
                                        <div class="col-md-12 d-flex justify-content-center ">
                                          <asp:Button ID="btnsatephoto" class="btn Submit" runat="server" Text="Update" />
                                          
                                        </div>
                                        </div>
                                     
                              </div>
         <div class="col-md-2">
         </div>
         </div>
         </asp:Panel>

   </div>
   </div>
  </div>
 

   
</div>
</asp:Panel>
 
    </div>
    </div>
    </div>
   
    </form>
    
</body>
</html>
