<%@ Page Language="VB"  AutoEventWireup="false" EnableEventValidation="false" CodeFile="Student.aspx.vb"
    Inherits="Admin_Student" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>student mis</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Bootstrap -->
       <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
 
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../AR/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="../AR/build/css/custom.min.css" rel="stylesheet">
    <style>
     .maincontainer {
   
    background-color:none;

    text-align: left;
   }
   body
   {
       background-color:#f2f3f5;
   }
   .title_left h2
   {
    color:#15283c;
    font-size:24px;
    font-weight:400;
  
   }
   .x_title h2
   {
        color:#15283c;
    font-size:20px;
    font-weight:400;
  
   }
   .control-label
   {
       color:#15283c;
    font-size:16px;
    font-weight:400;
  
   }
   .submit
 {
     margin-top:10px;
     font-size: 16px !important;
      font-weight: 500;
      
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      width:25%;
   }
   .submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
    
      .backbotton
{
    margin-top:10px;
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    
}

.backbotton:hover
{
    color:#15283c;
}
  .toprow
  {
      margin-bottom:12px;
  }
  .hiddencol
  {
      display:none;
  }
    </style>
</head>
<body>
   <form id="form1"  runat="server" >
    <div class="container-fluid maincontainer">
        <div class="row">
            <div class="col-md-12">
           
                    <div class="page-title">
                        <div class="title_left d-flex">
                         <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                &nbsp &nbsp
                            <h2>
                                Student Information</h2>
                            <%-- <div class="title_right">
                <div class="col-md-5 col-sm-5 col-xs-12 form-group pull-right top_search">
                  <div class="input-group">
                    <input type="text" class="form-control" placeholder="Search for...">
                    <span class="input-group-btn">
                      <button class="btn btn-default" type="button">Go!</button>
                    </span>
                  </div>
                </div>
              </div>--%>
                        </div>
                        <div class="title_right">
                            <txtstu:textbox ID="stusearch" class="form-control" runat="server"></txtstu:textbox>
                        </div>
                        <div class="clearfix">
                        </div>
                    
                    <div class="row">
                            <!-- form input mask -->
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <h2>
                                            Basic Information</h2>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                            <%--  <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                        <ul class="dropdown-menu" role="menu">
                          <li><a href="#">Settings 1</a>
                          </li>
                          <li><a href="#">Settings 2</a>
                          </li>
                        </ul>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>--%>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="x_content">
                                     
                                        <div class="form-horizontal form-label-left">

                                        <div class="row">
                                        <div class="col-md-6">
                                            <div class="row pb-2">

                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                    Admission Date
                                                 </label>
                                                <div class="col-md-9 col-sm-9 col-xs-9">
                                                    <a runat="server" class="form-control" id="txtdated"></a>
                                                    <%-- <span class="fa fa-user form-control-feedback right" aria-hidden="true"></span>--%>
                                                </div>
                                                
                    </div>
                    <div class="row pb-2">
                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                            Admission_Year</label>
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <%--     <input type="text" class="form-control" data-inputmask="'mask' : '(999) 999-9999'">--%>
                            <a runat="server" class="form-control" id="ddlacademicyear"></a>
                        </div>
                    </div>
                    <div class="row pb-2">
                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                            Registraion No.</label>
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <a runat="server" class="form-control" id="TxtAdmNo"></a>
                        </div>
                    </div>
                    <div class="row pb-2">
                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                            Admission_Mode</label>
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <a runat="server" class="form-control" id="ddlstutype"></a>
                        </div>
                    </div>
                    <div class="row pb-2">
                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                            Fee_Category</label>
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <a runat="server" class="form-control" id="ddlseat"></a>
                        </div>
                    </div>
                    
                    </div>
                  
                    <div class="col-md-6">
                      <div class="row pb-2 hiddencol">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                            Program</label>
                                        <div class="col-md-9 col-sm-9 col-xs-9">
                                            <a runat="server" class="form-control" id="ddlcollege"></a>
                                            <%-- <span class="fa fa-user form-control-feedback right" aria-hidden="true"></span>--%>
                                        </div>
                                    </div>
                                    <div class="row pb-2">
                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                            Student_Name</label>
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <a runat="server" class="form-control" id="TxtFirstname"></a>
                        </div>
                    </div>
                                    <div class="row pb-2">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                            Program</label>
                                        <div class="col-md-9 col-sm-9 col-xs-9">
                                            <%--     <input type="text" class="form-control" data-inputmask="'mask' : '(999) 999-9999'">--%>
                                            <a runat="server" class="form-control" id="ddlcourse"></a>
                                        </div>
                                    </div>
                                    <div class="row pb-2">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                            Semester/Year</label>
                                        <div class="col-md-9 col-sm-9 col-xs-9">
                                            <a runat="server" class="form-control" id="ddlsem"></a>
                                        </div>
                                    </div>
                                    <div class="row pb-2">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                            Section</label>
                                        <div class="col-md-9 col-sm-9 col-xs-9">
                                            <a runat="server" class="form-control" id="ddlsec"></a>
                                        </div>
                                    </div>
                                    <div class="row pb-2 hiddencol ">
                                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                            Batch</label>
                                        <div class="col-md-9 col-sm-9 col-xs-9">
                                            <a runat="server" class="form-control" id="ddlbatch"></a>
                                        </div>
                                    </div>
                                  
                    <div class="row pb-2">
                        <label class="control-label col-md-3 col-sm-3 col-xs-3">
                            Father's_Name</label>
                        <div class="col-md-9 col-sm-9 col-xs-9">
                            <a runat="server" class="form-control" id="TxtFname"></a>
                        </div>
                    </div>
                       </div>
                                        </div>
                                     
                    <%--   <div class="row pb-2">
                        <div class="col-md-9 col-md-offset-3">
                          <button type="submit" class="btn btn-primary">Cancel</button>
                          <button type="submit" class="btn btn-success">Submit</button>
                        </div>
                      </div>--%>
                    </div>
                    
                     </div> 
                     </div>
                      </div>
                    
                    </div>

                        <asp:Panel ID="Panelpersonal" runat="server">
                       
                   
                        <!-- form input mask -->
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                             <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="x_panel">
                                        <div class="x_title">
                                            <h2>
                                                Academic Details</h2>
                                            <ul class="nav navbar-right panel_toolbox">
                                                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                            </ul>
                                            <div class="clearfix">
                        </div>
                     
                                        </div>
                                        <div class="x_content">
                                          
                                            <div class="form-horizontal form-label-left">
                                            <div class="row">
                                            <div class="col-md-12">
                                                <div class="row pb-2">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                        Roll No.</label>
                                                    <div class="col-md-9 col-sm-9 col-xs-9">
                                                        <asp:TextBox ID="txtsturollno" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row pb-2">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                        Student Status</label>
                                                    <div class="col-md-9 col-sm-9 col-xs-9">
                                                        <%--     <input type="text" class="form-control" data-inputmask="'mask' : '(999) 999-9999'">--%>
                                                        <asp:DropDownList ID="ddlstatus" runat="server"  class="form-control">
                                                        
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                 <div class="row pb-2">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                        Student Category</label>
                                                    <div class="col-md-9 col-sm-9 col-xs-9">
                                                       <asp:DropDownList ID="ddlstudentmode"  AppendDataBoundItems="true" runat="server"  class="form-control" >
                                                                </asp:DropDownList>
                                                    </div>
                                                </div>


                                                <div class="row pb-2">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                        Enrollment No</label>
                                                    <div class="col-md-9 col-sm-9 col-xs-9">
                                                        <asp:TextBox ID="txtenrolment" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row pb-2">
                                                    <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                        Aadhar No.</label>
                                                    <div class="col-md-9 col-sm-9 col-xs-9">
                                                        <asp:TextBox ID="txtadharno" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row pb-2">
                                                    <div class="col-md-12 d-flex justify-content-center ">
                                                        <%-- <button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                        <asp:Button ID="btnsaveacademic" class="btn btn-success submit" runat="server" Text="Save" />
                                                    </div>
                                                </div>
                                                </div>
                                            </div>
                                            
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                        Photograph/Signature/Thumb</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                    <div class="form-horizontal form-label-left">
                                        <div class="row pb-2">
                                            
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:Image ID="stuimage" runat="server" Height="90px" Width="80px" BorderColor="Gray"
                                                        BorderStyle="Double" />
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:Image ID="Stuimagesig" runat="server" Height="90px" Width="80px" BorderColor="Gray"
                                                        BorderStyle="Double" />
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:Image ID="Image1" runat="server" Height="90px" Width="80px" BorderColor="Gray"
                                                        BorderStyle="Double" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                           
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <%--     <input type="text" class="form-control" data-inputmask="'mask' : '(999) 999-9999'">--%>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Image Upload" />
                                                    <asp:Button ID="btnimgdelete" runat="server" Text="X" />
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:FileUpload ID="FileUpload2" runat="server" ToolTip="Image Upload" />
                                                    <asp:Button ID="btnsigdelete" runat="server" Text="X" />
                                                </div>
                                                <div class="col-md-4 col-sm-4 col-xs-4">
                                                    <asp:FileUpload ID="FileUpload3" runat="server" ToolTip="Image Upload" />
                                                    <asp:Button ID="Button1" runat="server" Text="X" />
                                                </div>
                                            </div>
                                        </div>
                                       
                                        <div class="row pb-2">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <%-- <button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btnsatephoto" class="btn btn-success submit" runat="server" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                            </ContentTemplate>
                            <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtDt" EventName="TextChanged" />
        </Triggers>--%>
                        </asp:UpdatePanel>
                        <!-- /form input mask -->
                        <!-- form color picker -->
                        <!-- /form color picker -->
                        <!-- form input knob -->
                        <!-- /form input knob -->
                        
                    
                    <div class="row">
                        <!-- form input mask -->
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                        Personal Information</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                  
                                    <div class="form-horizontal form-label-left">
                                    <div class="row">
                                    <div class="col-md-6">
                                    
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Date Of Birth</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <datecrtwithpostback:textbox ID="TxtDob" class="form-control" width="100%" runat="server">
                                                </datecrtwithpostback:textbox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Gender</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <%--     <input type="text" class="form-control" data-inputmask="'mask' : '(999) 999-9999'">--%>
                                                <asp:DropDownList ID="ddlgender" class="form-control" runat="server">
                                                    <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Nationality</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TxtNationality" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Martial Status</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList AppendDataBoundItems="true" class="form-control" ID="ddlmarital"
                                                    runat="server">
                                                    <asp:ListItem Selected="True" Value="" Text="">
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="Unmarried">Unmarried
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="married">married</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Religion</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlreligion" class="form-control" runat="server" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Caste</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="DdlCategoryc" class="form-control" runat="server" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2" >
                                           <div class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Father's Occupation</div>

                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlFOccupation" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Mother's Name</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="Txtmothername" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Mother's Occupation</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlMOccupation" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Income</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="Txtincome" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                       
                                        <div class="row pb-2 hiddencol">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <%--  <button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btnpersonalsave" class="btn btn-success submit" runat="server" Text="Save" />
                                            </div>
                                        </div>


                                        </div>

                                        <div class="col-md-6">
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Seat Info</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TxtDomocile" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Account No.</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtaccountno" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Bank Name</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlbankname" class="form-control" runat="server" AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Branch Name</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtbranchname" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                IFSC Code</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtifsecode" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Hobbies</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TxtHobbies" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Physical Disability</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TxtPassport" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Vision</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="DdlVision" runat="server" class="form-control">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>Good</asp:ListItem>
                                                    <asp:ListItem>High</asp:ListItem>
                                                    <asp:ListItem>Low</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Blood Group</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="DdlBlood" runat="server" class="form-control">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>A</asp:ListItem>
                                                    <asp:ListItem>A-</asp:ListItem>
                                                    <asp:ListItem>A+</asp:ListItem>
                                                    <asp:ListItem>AB</asp:ListItem>
                                                    <asp:ListItem>AB-</asp:ListItem>
                                                    <asp:ListItem>AB+</asp:ListItem>
                                                    <asp:ListItem>B</asp:ListItem>
                                                    <asp:ListItem>B-</asp:ListItem>
                                                    <asp:ListItem>B+</asp:ListItem>
                                                    <asp:ListItem>O</asp:ListItem>
                                                    <asp:ListItem>O-</asp:ListItem>
                                                    <asp:ListItem>O+</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Height</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TxtHeight" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Weight</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TxtWeight" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                       
                                        
                                        
                                        </div>

                                        <div class="col-md-12 d-flex justify-content-center">
                                          
                                                <%--  <button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btnfamilysave" class="btn btn-success submit" Width="18%" runat="server" Text="Save" />
                                          
                                       
                                        </div>
                                    </div>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /form input mask -->
                        <!-- form color picker -->
                        <!-- /form color picker -->
                        <!-- form input knob -->
                        <!-- /form input knob -->
                        
                    </div>
                    <div class="row">
                        <!-- form input mask -->
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                        correspondence address </h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                   
                                    <div class="form-horizontal form-label-left">
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Address 1
                                            </label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="textcorraddress1" placeholder="Address 1" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Address 2</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="textcorraddress2" placeholder="Address 2" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Pin-Code</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtcorrpin" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Country</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlcorrcountry" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                State</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlcorrstate" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                District</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlcordistt" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                City</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlcorrcity" class="form-control required" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Gaurdian Name</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtGaurdianname" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Relation</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtGaurdianrelation" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Guardian Email</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtGaurdianemail" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Mobile Number</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtGaurdianmobile" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                LandLine Number</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtGaurdianphone" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="row pb-2">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <%--<button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btnaddress1" class="btn btn-success submit" runat="server" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /form input mask -->
                        <!-- form color picker -->
                        <!-- /form color picker -->
                        <!-- form input knob -->
                        <!-- /form input knob -->
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                        Permanent Address</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                   
                                    <div class="form-horizontal form-label-left">
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Address 1
                                            </label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtprmadd1" placeholder="Address 1" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Address 2</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtprmadd2" placeholder="Address 2" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Pin-Code</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtprmpin" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Country</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlprmcountry" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                State</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlprmsate" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                District</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlprmdistt" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                City</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlprmcity" class="form-control required" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                           <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Phone</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtcorrphone" placeholder="Mobile no" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Mobile No.</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtcorrmobile" placeholder="Mobile no" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Email</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtcorrEmail" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                     
                                        <div class="row pb-2 hiddencol">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Phone</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtPrmPhone" placeholder="Mobile no" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2 hiddencol">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Mobile No.</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtPrmMobile" placeholder="Mobile no" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row pb-2 hiddencol">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Email</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TextBox6" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="row pb-2">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <%--<button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btnsavecontect" class="btn btn-success submit" runat="server" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <!-- form input mask -->
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                        Education Details</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                    <br />
                                    <div class="form-horizontal form-label-left">
                                        <div class="row pb-2">
                                            <asp:SqlDataSource ID="sdsquali" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT [Qualification] FROM [Qualification]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="board" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT FieldValue  AS BOARD  FROM LovUserEnglish WHERE MasterListingID=  48 ORDER BY FieldValue">
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="university" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT FieldValue  AS BOARD  FROM LovUserEnglish WHERE MasterListingID=  61 ORDER BY FieldValue">
                                            </asp:SqlDataSource>
                                            <asp:GridView ID="GrdEdu" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False"
                                                GridLines="Horizontal" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSno" runat="server" Text="<%#Container.DataItemIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qualification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQual" runat="server" Font-Bold="true" Text='<%#Eval("Qualification")%>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlquli" SelectedValue='<%#Eval("Board") %>' Width="100px"
                                                                AppendDataBoundItems="True" runat="server" DataSourceID="board" DataTextField="BOARD"
                                                                DataValueField="BOARD">
                                                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="University">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddluni"  Width="100px"
                                                                AppendDataBoundItems="True" runat="server" DataSourceID="university" DataTextField="BOARD"
                                                                DataValueField="BOARD">
                                                                <asp:ListItem Selected="True" Value="" Text=""></asp:ListItem>
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
                                                    <asp:TemplateField HeaderText="Institute ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtinstitute" Text='<%#Eval("Institution")%>' runat="server" Width="80px"></asp:TextBox>
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
                                                    <asp:TemplateField HeaderText="PCM %" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtpcm" Text='<%#Eval("PCM")%>'  runat="server" Width="50px"></asp:TextBox></ItemTemplate>
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
                                                            <asp:TextBox ID="Txtgrade" Text='<%#Eval("Grade")%>' runat="server" Width="50px"></asp:TextBox>
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

                                        <div class="row pb-2">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <%--<button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btneducation" runat="server" Width="18%" class="btn btn-success submit" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /form input mask -->
                        <!-- form color picker -->
                    </div>
                     </asp:Panel>

                       <asp:Panel ID="Paneldocuments" runat="server">
                    <div class="row">
                        <!-- form input mask -->
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                        Documents Details</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                    <br />
                                    <div class="form-horizontal form-label-left">
                                        <div class="row pb-2">
                                            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT [EssentialDoc], [EssentialDocID] FROM [EssentialDoc]" ID="SqlDataSource1">
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                                                SelectCommand="SELECT [EssentialDoc], [EssentialDocID] FROM [EssentialDoc]" ID="SdsDoclist">
                                            </asp:SqlDataSource>
                                            <asp:GridView ID="GrdDoclist" CssClass="table table-bordered" Width="100%" runat="server"
                                                AutoGenerateColumns="False" DataKeyNames="EssentialDocID" GridLines="Horizontal">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Document">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" Font-Bold="true" Text='<%#Bind("EssentialDocid") %>' runat="server"
                                                                Visible="False"></asp:Label>
                                                            <asp:Label ID="LbDocument" Text='<%#Bind("EssentialDoc") %>' Font-Bold="true" runat="server"
                                                                Width="200px"></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chkrequired" Checked='<%# Eval("reqid") %>' runat="server" /></ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="Chkheader" runat="server" Width="150px" Text="Is Required" AutoPostBack="true" /></HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Submited">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkSubmit" Checked='<%# Eval("IsSub") %>' runat="server" Width="150px" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Photo Copy/Original">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlphotocopy" SelectedValue='<%#Eval("PhotoCopy") %>' runat="server">
                                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                                <asp:ListItem>Photo Copy</asp:ListItem>
                                                                <asp:ListItem>Original Copy</asp:ListItem>
                                                                <asp:ListItem>Original + Photo Copy</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDownload" Text="Download" runat="server" CommandArgument='<%# eval("EssentialDocID") %>'
                                                                OnClick="DownloadFile"></asp:LinkButton></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandArgument='<%# Eval("EssentialDocID") %>'
                                                                OnClick="DeleteFile" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UPload">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="FileUpload3" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    Batches Not Available</EmptyDataTemplate>
                                                <%--<FooterStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Left" />
        <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                        </div>
                                        <div class="ln_solid">
                                        </div>
                                        <div class="row pb-2">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <%--  <button type="submit" class="btn btn-primary">Cancel</button>--%>
                                                <asp:Button ID="btnuploaddoc" runat="server" class="btn btn-success submit" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /form input mask -->
                        <!-- form color picker -->
                    </div>
                    </asp:Panel>

                   <%--  <asp:Panel ID="panelconcession" runat="server">
                    <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                       Concession Apply</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                    <br />
                                    <div class="form-horizontal form-label-left">
                                       
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Concession Name</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="ddlconcessiontype" runat="server" class="form-control" AppendDataBoundItems="true"
                                                    AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Concession By</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TextBox13" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Approved By</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TextBox14" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Approved Date</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <datecrtwithpostback:textbox ID="txtappdated" width="350" class="form-control" AutoPostBack="false"
                                                    runat="server"></datecrtwithpostback:textbox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Remark</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="TextBox15" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="ln_solid">
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-9 col-md-offset-3">
                                                <asp:Button ID="Button5" class="btn btn-success" runat="server" Text="Save" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                      </asp:Panel>--%>

                    <asp:Panel ID="panelConsultancy" runat="server">
                      <div class="row">
                        <div class="col-md-6 col-sm-12 col-xs-12">
                            <div class="x_panel">
                                <div class="x_title">
                                    <h2>
                                       Consultancy</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a></li>
                                    </ul>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="x_content">
                                    <br />
                                    <div class="form-horizontal form-label-left">
                                      
                                      <%-- <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Fee Category</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="DropDownList8" runat="server" class="form-control" AppendDataBoundItems="true"
                                                    AutoPostBack="false">
                                                    <asp:ListItem Selected="true" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                       <%-- <div class="form-group">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Admission Type</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="DdlCase" runat="server" class="form-control" AppendDataBoundItems="true"
                                                    AutoPostBack="false">
                                                    <asp:ListItem Selected="true" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Consultant Name</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="Ddlconsul" runat="server" class="form-control" AppendDataBoundItems="true"
                                                    AutoPostBack="false" DataTextField="ConsultancyName" DataValueField="ConsultantId">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                       <%-- <div class="row pb-2" style="display: none">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Country</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="row pb-2">
                                            <label class="control-label col-md-3 col-sm-3 col-xs-3">
                                                Consultancy</label>
                                            <div class="col-md-9 col-sm-9 col-xs-9">
                                                <asp:TextBox ID="txtConsultant" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                          <div class="ln_solid">
                                        </div>
                                        <div class="row pb-2">
                                            <div class="col-md-12 d-flex justify-content-center ">
                                                <asp:Button ID="btnConsultant" class="btn btn-success submit" runat="server" Text="Save" />
                                            </div>
                                        </div>

                                         <div class="form-group">
                                            <div class="col-md-9 col-md-offset-3">
                                                <asp:GridView ID="grdconsultant" Width="100%" runat="server"
            HeaderStyle-Font-Size="medium" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:BoundField DataField="sem" HeaderText="Sem" />
              
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtcamt" text='<%# bind("amount") %>' runat="server"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                </asp:TemplateField>
                 
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Left" Font-Bold="True" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
                                            </div>
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
    </div>
    </form>
       <script src="../AR/vendors/jquery/dist/jquery.min.js"></script>
        <!-- Custom Theme Scripts -->
    <script src="../AR/build/js/custom.min.js"></script>
</body>
<%--<script src="../AR/vendors/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap -->
<script src="../AR/vendors/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- FastClick -->
<script src="../AR/vendors/fastclick/lib/fastclick.js"></script>
<!-- NProgress -->
<script src="../AR/vendors/nprogress/nprogress.js"></script>
<!-- Chart.js -->
<script src="../AR/vendors/Chart.js/dist/Chart.min.js"></script>
<!-- gauge.js -->
<script src="../AR/vendors/gauge.js/dist/gauge.min.js"></script>
<!-- bootstrap-progressbar -->
<script src="../AR/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js"></script>
<!-- iCheck -->
<script src="../AR/vendors/iCheck/icheck.min.js"></script>
<!-- Skycons -->
<script src="../AR/vendors/skycons/skycons.js"></script>
<!-- Flot -->
<script src="../AR/vendors/Flot/jquery.flot.js"></script>
<script src="../AR/vendors/Flot/jquery.flot.pie.js"></script>
<script src="../AR/vendors/Flot/jquery.flot.time.js"></script>
<script src="../AR/vendors/Flot/jquery.flot.stack.js"></script>
<script src="../AR/vendors/Flot/jquery.flot.resize.js"></script>
<!-- Flot plugins -->
<script src="../AR/vendors/flot.orderbars/js/jquery.flot.orderBars.js"></script>
<script src="../AR/vendors/flot-spline/js/jquery.flot.spline.min.js"></script>
<script src="../AR/vendors/flot.curvedlines/curvedLines.js"></script>
<!-- DateJS -->
<script src="../AR/vendors/DateJS/build/date.js"></script>
<!-- JQVMap -->
<script src="../AR/vendors/jqvmap/dist/jquery.vmap.js"></script>
<script src="../AR/vendors/jqvmap/dist/maps/jquery.vmap.world.js"></script>
<script src="../AR/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js"></script>
<!-- bootstrap-daterangepicker -->
<script src="../AR/vendors/moment/min/moment.min.js"></script>
<script src="../AR/vendors/bootstrap-daterangepicker/daterangepicker.js"></script>
<!-- Custom Theme Scripts -->
<script src="../AR/build/js/custom.min.js"></script>--%>
</html>
