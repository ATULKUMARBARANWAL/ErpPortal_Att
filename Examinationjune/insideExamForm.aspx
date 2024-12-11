<%@ Page Language="VB" AutoEventWireup="false" CodeFile="insideExamForm.aspx.vb" EnableEventValidation="false" Inherits="TESTFILES_insideExamForm" %>
<%--'Design By Shivani And Developed By Avaneesh Yadav--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
   <script src="../LeadJquery/jquery1.min.js" type="text/javascript"></script>
    <style>
        body
        {
        background:#f2f3f5;
        overflow-x: hidden;
        background-repeat: no-repeat;
        background-size: 100% 100%;
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
    background-color:gray;
    color:#f2f3f5;
    border-radius:50%;
    font-weight:600;
    font-size:18px;
    margin-top:-15px;
    text-align:center;
    padding:8px 6px 8px 6px;
    padding-left:14px;
    }
 .btnaddDecription:hover
{
    font-weight:600;
    font-size:18px;
    background-color:#000;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid gray;
    }
   #Download
   {
   color:gray;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #Download:hover {
   color:black;
   font-weight: 600;
    } 
 #DownloadSubject
 {
   color:gray;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #DownloadSubject:hover {
   color:black;
   font-weight: 600;
    } 
 #DdownloadUnitName
 {
   color:gray;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #DdownloadUnitName:hover {
   color:black;
   font-weight: 600;
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
    @media print {
   #btnverify {
      visibility: hidden;
       }
       #ddlverify {
      visibility: hidden;
       }
    }
    .hiddencol
    {
        display :none;
    }
    .heading
{
    font-size:20px;
    color:#15283c;
    font-weight:500;
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
  height:5px;
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
 .Icons
   {
   color:#1ed085;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 400;
   border:none;
       }
   
  .Icons:hover {
   color:#1aad6f;
   font-weight: 400;
    }
    .Clgdetail
    {
        color: #000080; 
        font-weight: 400; 
        font-size: 18px;
        text-align:center;
    }
    .Formno
    {
         color: red; 
        font-weight: 400; 
        font-size: 18px;
       
    }
 
 #backstudent
{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#backstudent:hover
{
    color:#15283c;
}
 
    </style>
    <script type="text/javascript">
        function print_page() {
            var ButtonControl = document.getElementById("btnprint");
            ButtonControl.style.visibility = "hidden";
            window.print();
        }
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

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
 
    <div class="col-md-12">
    <div class="Performance">
    
    </div>
    </div>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
       <ContentTemplate >
        
      
    <asp:Panel ID="Paanelstudentlistsuccess" runat="server" Visible="True">
      
   <div class="container-fluid maincontainer mt-2">
                     
                       <div class="row">
     <div class="col-md-6">
     <table width="70%">
     <tr width="100%">
     <td width="8%">
      <div class="text-center maincontainerm m-0">
        <asp:LinkButton ID="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
     </div>
     </td>
     <td width="40%" align="center"> 
     <div class="heading1">
         <h5>
             <asp:Label ID="coursename" class="heading" runat="server" >Exam Form : </asp:Label>
          </h5>   
            
     </div>
     </td>
     <td width = "52%">
     <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" class="form-select">
              <asp:ListItem Value="0" Selected="True">Pending</asp:ListItem>
              <asp:ListItem Value="1">Verified</asp:ListItem>
              <asp:ListItem Value="2">Rejected</asp:ListItem>
              </asp:DropDownList>
     </td>
     
     </tr>
     </table>
      
     </div>
     <div class="col-md-6 text-end">
       <asp:LinkButton ID="Download" runat="server" CssClass="DownloadExcel"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
     </div>
     </div>
          

                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>

                       

                       <div class="row mt-2">
                                  <div class="col-md-12 text-end">
                                
                                  </div>
                       </div>

                       <div class="row mt-3">
                          <div class="col-md-12">
                          
                          <div class="custom-scrollbar-css" id="d">
                            <asp:GridView ID="FormVerified" GridLines="Both"  Width="100%"  DataKeyNames="StudentId"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
                          
                           <asp:BoundField HeaderText="AdmissionNo" DataField="AdmissionNo"></asp:BoundField>
                           <asp:TemplateField HeaderText="Student">
                          <ItemTemplate >
                          <asp:LinkButton  Text='<%# Eval("FirstName") %>' ID="namelink" runat="server" CommandName="SubjectName"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"></asp:LinkButton>       
                           </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Program" DataField="Course"></asp:BoundField>
                            <asp:BoundField HeaderText="Roll no" DataField="Roll_No"></asp:BoundField>
                             <asp:BoundField HeaderText="Enrollment No" DataField="EnrollmentNo"></asp:BoundField>
                           <asp:BoundField HeaderText="Father Name" DataField="FatherName"></asp:BoundField> 
                            <asp:BoundField HeaderText="Mobile" DataField="Mobile"></asp:BoundField> 
                            <asp:BoundField HeaderText="CEId" DataField="Courseexamid" ReadOnly="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField> 
                         <asp:BoundField HeaderText="Examformid" DataField="Examformid" ReadOnly="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField> 
                         <asp:BoundField HeaderText="Stuid" DataField="Studentid" ReadOnly="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField> 
                         
                         <asp:TemplateField HeaderText="View Exam Form">
            <ItemTemplate >
             <asp:LinkButton ID="namelink1" runat="server" CssClass="Icons" CommandName="ViewExamForm" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'> <i class="fa fa-eye"></i>  </asp:LinkButton>       
            </ItemTemplate>
           </asp:TemplateField>
                          </Columns>
           
                         </asp:GridView>
                         </div>
                          </div>
                       </div>
         
 </div>
 </asp:Panel>

    <asp:Panel ID="Panel1" runat="server" Visible="False">
    <div class="container-fluid maincontainer body" runat="server" id="divPrint">
    
    <div class="row">
    <div class="col-md-12 d-flex justify-content-between">
    <asp:LinkButton ID="backstudent" runat="server" CssClass="backbutton" ><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    <img alt="" src="../img/pr1.png" width="40px" onclick="PrintPanel();"    
        align="right" style="cursor:pointer;"/>
         
    </div>
    </div>

    <div class="row">
    <div class="col-md-12">
    <table width="100%">
    <tr width="100%">
    <td width="20%">
    <asp:Image ID="Image3" ImageUrl ="../img/collegelogo.jpg" style="border-radius:50%; border:1px solid #15283c; "  Width="100px" Height="100px" runat="server" />
    
    </td>
    <td width="60%" align="center">
    <div class="Clgdetail justify-content-center">
        <asp:Label ID="lblcollegename" runat="server" style="font-size:24px; font-weight:600;" class="clgname" Text="College Name"></asp:Label><br />
        
        Exam Form for Session  <asp:Label ID="Lblsession" class="clgsesn" style="font-size:18px; font-weight:500;" runat="server" Text="College Name"></asp:Label>
        <br />
        Exam form Date <asp:Label ID="lblDate" class="clgsesn" style="font-size:18px; font-weight:500;" runat="server"></asp:Label>
       
        </div>
    </td>
    <td width="20%" align="right">
    <div class="Formno" style="color:#000080; font-size:18px; font-weight:500;" >
        Form No : <asp:Label ID="lblformno" runat="server" Text=""></asp:Label>
      
        </div>
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
    
    <div class="row">
    <div class="col-md-2">
    </div>
    <div class="col-md-8">
    <table cellpadding="5" cellspacing="5" width="100% " >

      <tr>
    <td >
  <b>Roll No</b>
    </td>
    <td >
        <b><asp:Label ID="lblrollno" runat="server" Text=""></asp:Label></b>
    </td>

   
    <td colspan="" rowspan="5" align="center" valign="top">
        <asp:Image ID="imgstudnet"   Width="130px" Height="140px" runat="server" style="border:2px solid #000; padding:4px 4px 4px 4px;" /><br />
        <asp:Image ID="Image1"   Width="130px" Height="60px" runat="server" style="border:2px solid #000; padding:2px 2px 2px 2px;" />
                                       
    </td>

    </tr>

     <tr>
    <td >
   <b>Enrollment No</b>
    </td>
    <td >
     <b><asp:Label ID="lblenrollment" runat="server" Text=""></asp:Label></b>
    </td>
  
    </tr>

     <tr>
    <td >
   Program
    </td>
    <td >
   <asp:Label ID="lblProgramCourse" runat="server" Text=""></asp:Label>
    </td>
    </tr>

    <tr>
    <td >
   Name of Exam
    </td>
    <td >
   <asp:Label ID="lblexamname" runat="server" Text="Main Exam"></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="style6">
      Semester/Year
    </td>
    <td >
  <asp:Label ID="lblsemyear" runat="server" Text="2 Year 2 Sem"></asp:Label>
    </td>
    </tr>
    <tr>
    <td class="style6">
      College Name
    </td>
    <td >
  <asp:Label ID="Label5" runat="server" Text="Chaudhary Charan Singh University(SCRIET), Meerut"></asp:Label>
    </td>
    </tr>
    
    
     <tr>
    <td >
       Student Name
    </td>
    <td >
   <asp:Label ID="lblStudentName" runat="server" Text="Shivani Pankaj"></asp:Label>
    </td>
  
    </tr>
      <tr>
    <td >
        Gender
    </td>
    <td >
   <asp:Label ID="lblgender" runat="server" Text="Male"></asp:Label>
    </td>
  
    </tr>
    <tr>
     <td >
        Father Name</td>
       <td>
  <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label>
    </td>
    </tr>
     <tr>
        <td >
        <h5><strong><u>Paper Name: </u></strong></h5>
       </td>
   
     </tr>
     <tr style="width:840px;">
           <td colspan="4" class="style2" style="text-align: justify; width:820px;">
          <asp:GridView ID="grdstudent" GridLines="Both"  Width="100%"  DataKeyNames="SubjectId"
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table-bordered bg-white rounded_corners">
        <Columns>
                  <asp:TemplateField HeaderText="S.No.">
                  <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                 </ItemTemplate>
                 </asp:TemplateField>
       
                 <asp:BoundField HeaderText="EXamSubjectid" DataField="Examsubid" ReadOnly="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
                 <asp:BoundField HeaderText="Subjectid" DataField="Subjectid" ReadOnly="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
        
                 <asp:BoundField HeaderText="Examformid" DataField="Examformid" ReadOnly="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"></asp:BoundField>
         
                 <asp:BoundField HeaderText="Subject Code" DataField="Subjectcode"></asp:BoundField>
                 <asp:BoundField HeaderText="Subject Name" DataField="Subject"></asp:BoundField>
                 
                
        </Columns>
           
        </asp:GridView>
      </td>
     </tr>
 
   
 
 <tr>
    <td colspan="4" class="style2" style="text-align: justify; width:840px;">
      <br />
      <br />
      <strong><u>Note : </u></strong><br />
      I hereby declare that all particulars sated in this application form are true to the best of my knowledge and befit. I have read and understood all provision of regular admission. rules & regulation of University and agree to abide by them. I also affirm that I fulfill the eligibility requirements for the courses applied. in events of submission of frequents or incorrect information or suppression or distortion of any fact like educational qualification. Marks, Nationality etc. I understand that my admission/degree is liable for cancellation. I further understand that my admission is purely provisional subject to the verification of the eligibility conditions. I undersigned that none of my information should be given to anyone under the Right to Information Act 2005 without my prior written permission that has valid personal id attached with it.

    
   </td>

    </tr>
  <tr width="100%" >
         <th width="40%" align="center">
         <asp:Label ID="Label7" runat="server" class="Nameletter" Text=""></asp:Label>
         </th>
          <th width="40%" align="right">
         <asp:Label ID="Label8" runat="server" class="Nameletter" Text=""></asp:Label>
         </th>
          <th width="20%" align="left">
              
             <asp:Label ID="Label21" runat="server" class="Nameletter" Text=""></asp:Label>
          </th>
         </tr>
<tr width="100%" >
         <th width="40%" align="center">
         <asp:Label ID="Label17" runat="server" class="Nameletter" Text="Students Signature"></asp:Label>
         </th>
          <th width="40%" align="right">
         <asp:Label ID="lblverified" runat="server" class="Nameletter" Visible="false" Text="Verified"></asp:Label>
         </th>
          <th width="10%" align="left">
              <asp:DropDownList ID="ddlverify" runat="server" class="form-select">
              <asp:ListItem Value="0">Select</asp:ListItem>
              <asp:ListItem Value="1">Approved</asp:ListItem>
              <asp:ListItem Value="2">Reject</asp:ListItem>
              </asp:DropDownList>
              <asp:Label ID="lbladmissionno" runat="server" Text="Label" Visible="false"></asp:Label>
            <%-- <asp:Label ID="Label21" runat="server" class="Nameletter" Text="Create Exam"></asp:Label>--%>
          </th>
          <th width="10%"> <asp:Button ID="btnverify" CssClass="btn btn-secondary" runat="server" Text="Submit" /></th>
         </tr>
           
        <tr width="100%" >
         <th width="30%" align="center">
         <asp:Label ID="Label9" runat="server" class="Nameletter" Text=""></asp:Label>
         </th>
          <th width="30%" align="right">
         <asp:Label ID="Label10" runat="server" class="Nameletter" Text=""></asp:Label>
         </th>
          <th width="40%" align="left">
             
          
          </th>
         </tr>
    </table>
    </div>
    <div class="col-md-2">
    </div>
    </div>

       
    <br />
    <br />
   </div>

       
    </div>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>

    </div>
    </form>

     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=divPrint.ClientID %>");
             var printWindow = window.open('', '', 'height=400,width=800');
             printWindow.document.write('<html><head><title>DIV Contents</title>');
             printWindow.document.write('</head><body >');
             printWindow.document.write(panel.innerHTML);
             printWindow.document.write('</body></html>');
             printWindow.document.close();
             setTimeout(function () {
                 printWindow.print();
             }, 500);
             return false;
             window.close;
         }
    </script>

    <script src="../LeadJquery/table2excel.js" type="text/javascript"></script>
  
<script type="text/javascript">
    $("body").on("click", "#Download", function () {
        $("[id*=grdstudent]").table2excel({
            filename: "ProgramWiseSubjectList.xls"
        });
    });
</script>
<script type="text/javascript">
    $("body").on("click", "#DownloadSubject", function () {
        $("[id*=grdSubjectList]").table2excel({
            filename: "SubjectList.xls"
        });
    });
</script>

<script type="text/javascript">
    $("body").on("click", "#DdownloadUnitName", function () {
        $("[id*=GridUnitDec]").table2excel({
            filename: "UnitList.xls"
        });
    });
</script>

</body>
</html>

