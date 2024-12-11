<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StudentList.aspx.vb" Inherits="StudentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Bootstrap5/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <script src="../LeadJquery/jquery1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    
   <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>

    <style type="text/css" >
         body
        {
        background:#f2f3f5;
    }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
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
       font-weight:400;
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
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
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    
}

.backbotton:hover
{
    color:#15283c;
}
 .addsubject
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
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }
     .custom-scrollbar-css 
    {
        min-height:0vh;
 max-height:65vh;
}
    .custom-scrollbar-css {
  overflow-y: scroll;
}

/* scrollbar width */
.custom-scrollbar-css::-webkit-scrollbar {
  width: 5px;
}

/* scrollbar track */
.custom-scrollbar-css::-webkit-scrollbar-track {
  background: #eee;
}

/* scrollbar handle */
.custom-scrollbar-css::-webkit-scrollbar-thumb {
  border-radius: 2rem;
  background-color: #00d2ff;
  background-image: linear-gradient(to top, #000 0%, #808080 100%);
}
 .hiddencol
    {
        display:none;
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
      padding:3px 10px;
      width:20%;
      
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
     .Icons
   {
   color:#808080;
   font-size: 24px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .Icons:hover {
   color:#000;
   font-weight: 600;
    } 
    .modifyicon
    {
       color:#1ed085;
       text-decoration:none;
       font-size:20px;
       font-weight:400; 
    }
.modifyicon:hover
   {
       color:#1aad6f;
       text-decoration:none;
 
    }
  
    </style>
    <script>
        function SelectheaderCheckboxes(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {



                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {


                        row.style.backgroundColor = "#f2f3f5";

                        inputList[i].checked = true;

                    }

                    else {

                        if (row.rowIndex % 2 == 0) {
                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }
 
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelCourseWise" runat="server" Visible="True">
    
     <div class="container-fluid maincontainer mt-2">
                     
         <asp:DropDownList ID="ddlacademicyear" AutoPostBack="true" Visible="false" runat="server">
      
         </asp:DropDownList>
        
                       <div class="row mt-2">

                          <div class="col-md-6">
                           <table width="100%">
                    <tr width="100%">
                    <td width="5%">
                              <div class="heading1 d-flex">
                              <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              <asp:LinkButton ID="backbotton1" class="backbotton" Visible="false" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                              
                               </div>
                   </td>
                   
                    <td width="10%" align="right">
                    Program:&nbsp
                    </td>
                    <td width="60%">
                    <asp:DropDownList  ID="Ddlprogram" AutoPostBack="true" class="form-select" runat="server">
                    
                                            </asp:DropDownList>
                                     
                    </td>
                    <td width="30%"></td>
                    </tr>
                    </table>
                    
                              
                             
                          </div>
                          
                          
                          <div class="col-md-6 d-flex justify-content-end">
                          <table width="70%">
                          <tr width="100%">
                          <td width="30%" align="right">
                           <asp:Label ID="Label3" runat="server" Text="Search: "></asp:Label>&nbsp
                          </td>
                          <td width="66%">
                              <asp:TextBox ID="txtsearch" Placeholder="Search by student name" AutoPostBack="true" class="form-control" runat="server"></asp:TextBox>
                          </td>
                          
                          </tr>
                          </table>
                          </div>
                          </div> 
                  

                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>


                       <div class="row mt-1">
                          
                          
                          <div class="col-md-6 d-flex">
                          <table width="50%">
                          <tr width="100%">
                          <td width="30%" align="right">
                           <asp:Label ID="Label2" runat="server" Text="Filter: "></asp:Label>&nbsp
                          </td>
                          <td width="33%">
                          <asp:DropDownList ID="Ddlsemyear" AutoPostBack="true" Width="100%"  runat="server" >
                          
                              </asp:DropDownList>
                              <asp:DropDownList ID="Ddlsemeser" visible="false" AutoPostBack="true" Width="100%"  runat="server" >
                          
                              </asp:DropDownList>
                          </td>
                          <td width="33%" >
                          &nbsp 
                              <asp:Label ID="Lblsemyear" runat="server" Text="N/A"></asp:Label>
                          </td>
                          
                          
                          </tr>
                          </table>
                            
                          </div>
                          
                          <div class="col-md-6 d-flex justify-content-end ">
                             <asp:LinkButton ID="LinkButton4" runat="server" CssClass="Icons"> <i class="fa-solid fa-filter"></i></asp:LinkButton>
                         &nbsp &nbsp   <asp:LinkButton ID="lnkbtnemail" runat="server" CssClass="Icons" 
                         data-bs-toggle="modal" data-bs-target="#myModal" OnClientClick="return true"> <i class="fa-solid fa-envelope"></i></asp:LinkButton>
                         &nbsp &nbsp  <asp:LinkButton ID="LinkButton3"  data-bs-toggle="modal" data-bs-target="#Smsmodal" OnClientClick="return false" runat="server" CssClass="Icons"> <i class="fa-solid fa-comment-dots"></i></asp:LinkButton>
                          
                         &nbsp &nbsp <asp:LinkButton ID="Download" runat="server" CssClass="Icons"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                          
                          </div> 

                         
                       </div>

                       
             
             <asp:Panel ID="PnlStudentlist" Visible="True" runat="server">
                       <div class="row mt-3">
                          <div class="col-md-12">
                              
                              
                          <div class="custom-scrollbar-css">
                          
                            <asp:GridView ID="gridstudentlist" GridLines="Both"  Width="100%"  DataKeyNames="Studentid"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                         <HeaderTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)" />
                         </HeaderTemplate>
                         <ItemTemplate>
                         <asp:CheckBox ID="btnselect" runat="server" />
                         </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                          <asp:BoundField HeaderText="academicyear" ItemStyle-Width="0px"  DataField="Academicyear" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>           
           
              <asp:BoundField HeaderText="Stndtid"  DataField="StudentID" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                         <asp:BoundField HeaderText="Admission No" DataField="AdmissionNo"></asp:BoundField>
                          
                          <asp:TemplateField HeaderText="Student Name">
                          <ItemTemplate >
                          <asp:LinkButton  Text='<%# Eval("Student") %>' ID="namelink" runat="server" CommandName="Studentprofile"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Student Name" DataField="Student" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                           <asp:BoundField HeaderText="Father's Name" DataField="FatherName"></asp:BoundField>
                           <asp:BoundField HeaderText="Email" DataField="Email"></asp:BoundField> 
                           <asp:BoundField HeaderText="Mobile" DataField="Mobile"></asp:BoundField> 
                            <asp:TemplateField HeaderText="Modify">
                          <ItemTemplate>
                          <asp:LinkButton ID="lnkModify" CommandName="Edit"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' class="modifyicon" runat="server" ><i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>     
                           </ItemTemplate>
                           </asp:TemplateField>   
                            
                          </Columns>
           
                         </asp:GridView>
                         
                         </div>
                          </div>
                       </div>

                       
                      </asp:Panel>
                       <div class="modal" id="myModal" data-bs-backdrop="static" data-bs-keyboard="false">
           <div class="modal-dialog modal-lg">
           <div class="modal-content">

           <div class="modal-header">
            <h5 class="modal-title">Email Communication</h5>
            <asp:LinkButton ID="LinkButton6" class="btn-close" data-dismiss="modal" runat="server"></asp:LinkButton>
           </div>

           <div class="container">
            <div class="row">
                <div class="col-md-6">
                
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="Icons">
            <i class="fa fa-envelope"></i>
        </asp:LinkButton>

                        
       
        </div>
        <div class="col-md-6">
                  <div class="mt-1 pl-3">
                  <table width="100%" style="margin-left:8PX;">
                   <tr width="100%">  
                   <td width="10%"></td>  
                   <td width="80%">                
                   <asp:DropDownList ID="ddlEmailSelect" runat="server" CssClass="email form-select">
                         <asp:ListItem>mk214642@gmail.com</asp:ListItem>
                         
                    </asp:DropDownList>
                    </td>  
                    <td width="10%">  
                    <asp:ImageButton ID="ImageButton1" runat="server" img src="images/5.jpg" CssClass="profile text-right"/>
                    </td>
                    </tr>
                    </table>
                    </div>

          </div>
        <div class="col-md-12 text-end text-danger mb-2">Total Selected student: 
            <asp:Label ID="Lblcountstndt" runat="server" Text="0"></asp:Label> 
         </div>     
            </div>
        </div>
            
           <div class="modal-body">
           
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

           <div class="form-row">
                       <div class="row">
                      <div class="form-group col-sm-6">
                        <asp:DropDownList ID="DdlSendTo"  runat="server" class="form-control">
                           
                        </asp:DropDownList>
                      </div>
                      <div class="form-group col-sm-6">
                         <asp:DropDownList ID="DdlEmailType" runat="server" class="form-select">
                            <asp:ListItem>CC</asp:ListItem>
                             <asp:ListItem>BCC</asp:ListItem>
                        </asp:DropDownList>
                      </div>
                      </div>
                      
                      <div class="row mt-3">
                      <div class="form-group  col-sm-12">
                            <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" placeholder="Enter Your Message/Notes" CssClass="form-control"></asp:TextBox>
                      </div>
                      </div>

                      <div class="row mt-3">
                      <div class="form-group  col-sm-12">
                      
                            <asp:DropDownList ID="DdlTemp" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged" >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                             <asp:ListItem Value="1">Select Template</asp:ListItem>
                            </asp:DropDownList>
                      
                      </div>
                      </div>

                      <div class="row mt-3">
                       <div class="form-group  col-sm-12">
                       <asp:Panel ID="pnlTextBox" runat="server" Visible="false">
                           <asp:TextBox ID="txteditor2" runat="server" Height="180px" placeholder="Message formating" CssClass="form-control border" BackColor="#0066FF"></asp:TextBox>
                           <%--<ajaxToolkit:HtmlEditorExtender ID="TextBox2_HtmlEditorExtender" runat="server" 
                            BehaviorID="TextBox2_HtmlEditorExtender" TargetControlID="txteditor2" EnableSanitization="false">
                            </ajaxToolkit:HtmlEditorExtender>--%>
                           <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server"
                            BehaviorID="TextBox2_HtmlEditorExtender" TargetControlID="txteditor2" EnableSanitization="false"></ajaxToolkit:HtmlEditorExtender>
                     - </asp:Panel>

                       
                       
                      </div>
                      </div>
                       
                       </div>
            </ContentTemplate>
            </asp:UpdatePanel>
           
           </div>
               
           <div class="modal-footer">
                  <asp:LinkButton ID="btnCancel" class="btn btn-outline-dark mr-auto" data-dismiss="modal" runat="server"><i class="fa fa-window-close mr-2"></i> Cancel</asp:LinkButton>
                  <asp:Button ID="btnSubmit" class="btn communicatesub" runat="server" Text="Send Now" />
           </div>
          
           </div>
           </div>
           </div>
           
           <div class="modal" id="Smsmodal" data-bs-backdrop="static" data-bs-keyboard="false">
           <div class="modal-dialog modal-lg">
           <div class="modal-content">

           <div class="modal-header">
            <h5 class="modal-title">SMS Communication</h5>
            <asp:LinkButton ID="LinkButton5" class="btn-close" data-dismiss="modal" runat="server"></asp:LinkButton>
           </div>

           <div class="container">
            <div class="row mt-1">
                <div class="col-md-6">
       
       <asp:LinkButton ID="LinkSMS" runat="server" CssClass="Icons">
            <i class="fa fa-mobile"></i>
        </asp:LinkButton>
                   
        </div>
        <div class="col-md-6">
                  <%--<div class="mt-1 pl-3">
                  <table width="100%" style="margin-left:8PX;">
                   <tr width="100%">  
                   <td width="10%"></td>  
                   <td width="80%">                
                   <asp:DropDownList ID="DropDownList2" runat="server" CssClass="email form-select">
                         <asp:ListItem>+91 9864756770</asp:ListItem>
                         
                    </asp:DropDownList>
                    </td>  
                    <td width="10%">  
                    <asp:ImageButton ID="ImageButton2" runat="server" img src="images/5.jpg" CssClass="profile text-right"/>
                    </td>
                    </tr>
                    </table>
                    </div>--%>

          </div>
        <div class="col-md-12 text-end text-danger mb-2">Total Selected Student: 
            <asp:Label ID="Label4" runat="server" Text="0"></asp:Label> 
         </div>     
            </div>
        </div>
     
           <div class="modal-body">
               
           <div class="form-row">
                       <div class="row">
                      <div class="form-group col-sm-12">
                        <asp:DropDownList ID="ddlPhoneFetch"  runat="server" class="form-control">
                             
                        </asp:DropDownList>
                      </div>
                      
                      </div>

                      <div class="row mt-3">
                      <div class="form-group  col-sm-12">
                            <asp:TextBox ID="txtNote1" runat="server" TextMode="MultiLine" placeholder="Enter Your Message/Notes" CssClass="form-control"></asp:TextBox>
                      </div>
                      </div>

                      

                     
                       
                       </div>
                      </div>

       
           <div class="modal-footer">
                  <asp:LinkButton ID="butncancel" class="btn btn-outline-dark mr-auto" data-dismiss="modal" runat="server"><i class="fa fa-window-close mr-2"></i> Cancel</asp:LinkButton>
                  <asp:Button ID="btnSendSMS" class="btn communicatesub" runat="server" Text="Send Now" />
           </div>

           </div>
           </div>
           </div>


                      

                       
                       
    </div>
    </asp:Panel>

    

    

    </form>
</body>
</html>
