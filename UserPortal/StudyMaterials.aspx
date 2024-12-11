<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StudyMaterials.aspx.vb" Inherits="UserPortal_StudyMaterials" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">   
<title>Study Materials</title>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<script src="../assets/plugins/bootstrap/js/bootstrap.bundle.min.js" type="text/javascript"></script>
<link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />  
    <link href="../assets/css/styles.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
  <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
<!-- Include jQuery and Bootstrap JS -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />

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
    th{font-size:20px;}
    td{font-size:17px;}
</style>
<style>
    
    body 
{
    background-color:#f2f3f5;
}
 .heading1
{
    padding-top:2px;
    }
  .heading1 h3
{
    color:#15283c;
    font-size:20px;
    font-weight:600;
   
    }
    
     .line
   {
       content:" ";
       width:100%;
       height:1px;
        background-color:#e1e2e3;
        margin-top:2px;
        margin-bottom:5px;
        
       
   }
    
      .backbotton
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    
}

.backbotton:hover
{
    color:#15283c;
}

  
       .Labels
       {
           
            font-size:17px;
    font-weight:500;
    color:#000;
           }
    
    .row1
    {
        margin-top:20px;
        }
        
        #lbtnDoc
        {
            color:White;
            background:#1ed085;
            border:3px solid #f0f0f0;
            }
        #lbtnvideo
        {
            border:3px solid #f0f0f0; }
            
           .button_outer {background: #1ed085; border-radius:30px; 
               text-align: center; height: 50px; 
               width: 50px; display: inline-block; 
              position: relative; overflow: hidden;}
 .button_outer:hover {background: #1aad6f; 
                      box-shadow:0px 1px 5px 1px #e1e2e3;
                      }

.btn_upload {padding: 15px 12px;
              color: #fff; 
             text-align: center; position: relative; font-size:18px;  font-weight:bold;
             display: inline-block; overflow: hidden; z-index: 3; white-space: nowrap;
            }
.btn_upload #upload_file {position: absolute; width: 100%; left: 0; top: 0;
                           width: 100%; height: 105%; cursor: pointer; opacity: 0;}


.floatbtn
{
    right:40px;
    top:90px;
      position: fixed;
      
    }
    
    .floatbtn.sticky{
     right:10px;
    
     
}


    .upload
    {
        background: #1ed085;
        color:#fff;
        }
        
         .upload:hover {background: #1aad6f; 
                      box-shadow:0px 1px 5px 1px #e1e2e3;
                       color:#fff;
                      }
                      
               
                   .secondary
    {
        border:1px solid #1ed085;
        color:#1ed085;
        }
        
         .secondary:hover {background: #1aad6f; 
                      box-shadow:0px 1px 5px 1px #e1e2e3;
                       color:#fff;
                      }
                      
                       
                      
           #lbtnvideo
           {
               
               color:Black;
               }
               
               .doccontainer{
    
    width:100%;
    position: relative;
    
    }
    
    .doccontainer img {
  width: 40%;
  height: auto;
}

.doccontainer .downloaddoc {
  position: absolute;
  top: 15%;
  left: 75%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.doccontainer .downloaddoc:hover {
 
  background-color: #F0f0f0;
  color: black;

}

.doccontainer .deletedoc {
  position: absolute;
  top: 40%;
  left: 75%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: Gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.doccontainer .deletedoc:hover {
 
  background-color: #F0f0f0;
  color: black;
 
}
.doccontainer .viewdoc {
  position: absolute;
  top: 65%;
  left: 75%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: Gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.doccontainer .viewdoc:hover {
 
  background-color: #F0f0f0;
  color: black;
 
}
  .vidcontainer{
    
    width:100%;
    position: relative;
    
    }
 
   .vidcontainer .video
   {
        width:130px ;
  height: 100px;
   }

.vidcontainer .downloaddoc {
  position: absolute;
  top: 20%;
  left: 95%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.vidcontainer .downloaddoc:hover {
 
  background-color: #F0f0f0;
  color: black;

}

.vidcontainer .deletedoc {
  position: absolute;
  top: 45%;
  left: 95%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #F0f0f0;
  color: Gray;
  font-size: 12px;
  padding:4px 8px;
 
  cursor: pointer;
  border-radius: 5px;
}

.vidcontainer .deletedoc:hover {
 
  background-color: #F0f0f0;
  color: black;
 
}
 .maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}          

/* Simple sleek dropdown styling */
.col-md-3 select {
    width: 100%;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #fff;
    font-size: 16px;
    color: #333;
    transition: border-color 0.3s;
}

.col-md-3 select:focus {
    border-color: #007bff; /* Blue border on focus */
    outline: none;
}

.col-md-3 select:hover {
    border-color: #007bff; /* Blue border on hover */
}

  .dropzone {
        width: 85%;
        height: 80px;
        border: 2px dashed #ccc;
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        font-size: 18px;
        color: #999;
        margin-top: 10px;
        cursor: pointer;
        position: relative;
    }
    .dropzone.dragover {
        border-color: #666;
        background-color: #f9f9f9;
    }
    .dropzone i {
        font-size: 20px;
        color: #666;
    }
    .file-list {
        margin-top:2px;
        font-size: 14px;
        color: #333;
        text-align: center;
    }
    .file-item {
        display: inline-block;
        margin-top: 5px;
    }
    .preview-img {
        width: 50px;
        height: 50px;
        object-fit: cover;
        margin: 5px;
        border: 1px solid #ddd;
    }
    .eye{
    content: "\f06e";
    margin-left: 335px;
    color: gray;
    cursor:pointer;
    margin-top:5px;
    
}
.eye:hover{color:Black;}
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
    <asp:LinkButton ID="backbotton" OnClientClick="goBack(); return false;" CssClass="fw-bold text-black p-2" runat="server"><i class="fa-solid fa-arrow-left fs-7"></i> 
    </asp:LinkButton>
    <script>
        function goBack() {
            window.history.back();
        }
            </script>
    </td>
    <td width="94%">
    <h5 class="enquiry">&nbsp;&nbsp;Study Materials</h5>
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
    <%--<div class="col-md-12">
<div class="row mt-3">
                <div class="col-md-12">
                    <nav>
                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                            <asp:LinkButton class="nav-item nav-link active" ID="lbtnDoc" runat="server" OnClick="lbtnDoc_Click">Documents</asp:LinkButton>
                            <asp:LinkButton class="nav-item nav-link" ID="lbtnVideo" runat="server" OnClick="lbtnVideo_Click">Videos</asp:LinkButton>
                        </div>
                    </nav>
                </div>
            </div>

            <!-- Panel for Documents -->
            <asp:Panel ID="panelDoc" runat="server" CssClass="tab-pane fade show active">
                <div class="row">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div class="col-md-2 text-center mt-2">
                                <div class="doccontainer">
                                    <img src="../ExaminationNImages/document-icon.png" alt="Document Icon" class="img-fluid" />
                                   
                                    <span>
                                        <a href='<%# Eval("Path") %>' target="_blank" class="viewdoc">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                    </span>
                                    <h2>
                                        <asp:Label ID="lblTopicDoc" runat="server" Text='<%# Eval("Topic") %>'></asp:Label>
                                    </h2>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>

            <!-- Panel for Videos -->
            <asp:Panel ID="panelVideo" runat="server" CssClass="tab-pane fade">
                <div class="row">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="col-md-2 text-center mt-2">
                                <div class="vidcontainer">
                                    <video class="video" controls>
                                        <source src='<%# Eval("Path") %>' type="video/mp4" />
                                    </video>
                                    <asp:HiddenField ID="hfSubplanitemid" runat="server" Value='<%# Eval("Subplanitemid") %>' />
                                    
                                    <h2>
                                        <asp:Label ID="lblTopicVid" runat="server" Text='<%# Eval("Topic") %>'></asp:Label>
                                    </h2>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>

    </div>--%>
          <div class="row row1">
                       <div class="col-md-12">
                               <nav>
                                   <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                    <asp:LinkButton class="nav-item nav-link active" ID="lbtnDoc" runat="server">Documents</asp:LinkButton>
                                    <asp:LinkButton class="nav-item nav-link " ID="lbtnvideo" runat="server">Videos</asp:LinkButton>
                                     </div>
                                </nav>
                    
                      </div>
                       </div>

         <asp:Panel ID="paneldoc" runat="server">
    <div class="row">
        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-2 text-center mt-2">
            <div class="doccontainer">
                <img src="../ExaminationNImages/j4cuv0iciu1adu5oef0imtkca8-92728334e12d3f4c8c60ace58f4ba84d.png" />
                <span>
                <asp:HiddenField ID="hfSubplanitemid" runat="server" Value='<%# Eval("Subplanitemid") %>' />

                    <!-- Download Icon (Initially hidden, visibility controlled in the code-behind) -->
                    <asp:LinkButton ID="lbtndownloaddoc" runat="server" class="downloaddoc" CommandArgument='<%# Eval("Path") %>' OnClick="DownloadFile">
                        <i class="fa fa-download"></i>
                    </asp:LinkButton>
                </span>
                <span>
                    <a href='<%# Eval("Path") %>' target="_blank" class="deletedoc">
                        <i class="fa fa-eye"></i>
                    </a>
                </span>
                <h2>
                    <asp:Label ID="Lbltopicdoc" class="Labels" runat="server" Text='<%# Eval("Topic") %>'></asp:Label>
                </h2>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

    </div>
</asp:Panel>



             <asp:Panel ID="panelvideo" Visible="false" runat="server">
        
           <div class="row">


<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-2 text-center mt-1">
            <div class="vidcontainer">
                <video class="video" controls>
                    <source src='<%# Eval("Path") %>' type="video/mp4" />
                </video>

                <!-- HiddenField for Subplanitemid -->
                <asp:HiddenField ID="hfSubplanitemid" runat="server" Value='<%# Eval("Subplanitemid") %>' />

                <span>
                    <!-- Download Icon -->
                    <asp:LinkButton ID="lbtndownloadvid" runat="server" class="downloaddoc" CommandArgument='<%# Eval("Path") %>' OnClick="DownloadFile" Visible="True">
                        <i class="fa fa-download"></i>
                    </asp:LinkButton>
                </span>
               <%-- <span>
                    <a href='<%# Eval("Path") %>' target="_blank" class="deletedoc">
                        <i class="fa fa-eye"></i>
                    </a>
                </span>--%>
                <asp:HyperLink ID="lnkViewDocument" runat="server" Target="_blank" CssClass="deletedoc">
    <i class="fa fa-eye"></i>
</asp:HyperLink>
                <h2>
                    <asp:Label ID="lbltopicvid" class="Labels" runat="server" Text='<%# Eval("Topic") %>'></asp:Label>
                </h2>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>




           </div>

            </asp:Panel>
    </div>
    
   </div>
   </div>
  </div>
 

   
</div>
</asp:Panel>
 
    </div>
    </div>
    </div>

     <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
  
    </form>
</body>
</html>