<%--'Design And Developed By Avaneesh Yadav--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Marksheet.aspx.vb" Inherits="Examinationjune_Marksheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../CSS1/ExamDahboard1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
       <script src="../LeadJquery/jquery1.min.js" type="text/javascript"></script>

    <style>
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
   .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
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
    
      .Submit1
 {
     font-size: 18px !important;
      font-weight: 500;
       padding:2px 12px;
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
       border:none;
     
   }
   .Submit1:hover
   {
      
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
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
  #backbotton
{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#backbotton:hover
{
    color:#15283c;
}
#btn_back_print
  {
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#btn_back_print:hover
{
    color:#15283c;
}
.maincontainerm
{
   
    color:#15283c; 
    font-size:16px; 
    width:42px;
    height:41px ;
    border-radius:50%;
    padding-top:8px;
  }
  #backbotton
{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#backbotton:hover
{
    color:#15283c;
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
  .Subheading h4 .heading
{
    font-size:20px;
    color:#15283c;
    font-weight:500;
}
.subheading1
{
    font-size:18px;
    color:#15283c;
    font-weight:400;
}
.hiddencol
{
    display:none;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Container-fluid maincontainer">
    <div class="row">
                       <div class="col-md-12">
                       <asp:Panel ID="pnlmain" runat="server">
                     
                                <div class="container-fluid">
                                    <div class="row">
            <div class="row">
            <div class="col-md-4">
                          <div class="Subheading">
                          <h4>
                           <table width="100%">
                    <tr width="100%">
                    <td width="10%">
                              <div class="heading1 d-flex">
                              <asp:LinkButton ID="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
       
                               </div>
                   </td>
                   
                    
                    <td width="80%">
                        <asp:Label ID="LblStName" Cssclass="heading" runat="server" Text="Generate Report"></asp:Label> 
                                   
     
                    </td>
                    </tr>
                    </table>
                      </h4>
                              </div> 
                             
                          </div>
                          <div class="col-md-8 d-flex justify-content-end">
                         <table width="100%">
                         <tr width="100%">
                         <td width="18%" align="right">
                          <asp:Label ID="lblprogramid" runat="server" Text="Exam Name : "></asp:Label>

                         </td>
                         <td width="30%">
                           <asp:DropDownList ID="ddlExamN" runat="server" AutoPostBack="true" CssClass="form-control input-sm form-select">
                                          </asp:DropDownList>
                         </td>
                         <td width="17%" align="right">
                        Program : &nbsp
                         </td> 
                         <td width="30%">
                              <asp:DropDownList ID="ddlAcademicyear" runat="server" AutoPostBack="true" CssClass="form-select">
                           </asp:DropDownList>
       
                         </td>
                         <td width="5%">
                         &nbsp
                           <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                               
                         </td>
                         </tr>
                         </table>
                           
                          
                          </div>
                                
 </div>
 

 <div class="line mt-2 mb-2"></div>






                      
                    
                       </div>

                                  <div class="row">
                               <div class="col-md-12 text-center">
                                   <asp:LinkButton ID="btnmarksheet" runat="server" CssClass="btn Submit1">MarkSheet</asp:LinkButton>
                                    <%--<asp:LinkButton class="nav-item nav-link active" ID="navactivity1" runat="server">Raba Sheet</asp:LinkButton>--%>
                                  
                                  
                                    </div>
                                    </div>  
                                <div class="msg_list_main mt-3">
                          <asp:GridView ID="grdstudent" GridLines="Both"  Width="100%"  DataKeyNames="StudentId"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white rounded_corners">
                <Columns>
                     <asp:TemplateField HeaderText="Sel">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkselect"   runat="server" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="Change_student" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                <%--  <asp:TemplateField HeaderText="S.No.">
         <HeaderTemplate>
             <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)"/>
          
         </HeaderTemplate>
        <ItemTemplate>--%>
      
           <%-- <asp:CheckBox ID="chkselect" runat="server"   CommandName ="select" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'/>
        </ItemTemplate>
       </asp:TemplateField>--%>
                  <asp:TemplateField HeaderText="S.No.">
                  <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                 </ItemTemplate>
                 </asp:TemplateField>
              
         <asp:BoundField HeaderText="Stid" DataField="StudentID" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
         <asp:BoundField HeaderText="Ceid" DataField="CourseExamid" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                    
                 <asp:BoundField HeaderText="AdmissionNo" DataField="AdmissionNo"></asp:BoundField>
                  <asp:BoundField HeaderText="EnrollmentNo" DataField="EnrollmentNo"></asp:BoundField>
                  <asp:TemplateField HeaderText="Student Name">
            <ItemTemplate >
             <asp:LinkButton  Text='<%# Eval("Student") %>' ID="namelink" runat="server" CommandName="StudetnAdmit" 
             CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="CourseName"></asp:LinkButton>       
            </ItemTemplate>
        </asp:TemplateField>
                  <asp:BoundField HeaderText="Gender" DataField="Gender"></asp:BoundField>
       <asp:BoundField HeaderText="Roll no" DataField="Roll_no"></asp:BoundField>
       
        <asp:BoundField HeaderText="FatherName" DataField="FatherName"></asp:BoundField>
         <asp:BoundField HeaderText="DOB" DataField="Dateofbirth"></asp:BoundField>
                    
                  
                    <asp:BoundField HeaderText="Mobile" DataField="Mobile"></asp:BoundField>
                  
                  
             </Columns>
           
                  </asp:GridView>
                                </div>
                               

                                
                                </div>

                        
                  </asp:Panel>
                      </div>
                      </div>
                     
                      <asp:Panel ID="pnlprint"   Visible="false" runat="server">
                            <div class="row">
                            <div class="col-md-12 d-flex justify-content-between">
                            <asp:LinkButton ID="btn_back_print" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        <asp:ImageButton ID="bntdocprint" ImageUrl="~/img/pr1.png" runat="server" Width="32px" Height="32px" />  
                                      </div>
                                   <div class="col-md-12">
                                        
                                                     
                    <div id="dvReport">
                     <asp:Panel ID="pnl1" runat="server">
                      <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                  
                      </asp:Panel>
                    </div>
                                    </div>
                                   
                                </div>
                                    </asp:Panel> 

           
    </div>
    </form>
</body>
    <script src="../LeadJquery/table2excel.js" type="text/javascript"></script>
  
<script type="text/javascript">
    $("body").on("click", "#Download", function () {
        $("[id*=grdstudent]").table2excel({
            filename: "RabaSheet.xls"
        });
    });
</script>

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
</html>