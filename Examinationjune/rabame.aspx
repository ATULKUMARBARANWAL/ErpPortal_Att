<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rabame.aspx.vb" Inherits="Examinationjune_raba" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <style>
      body
        {
            background:#f2f3f5;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">


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
 


    <div class="bg-white">
              
         <img alt="" src="../img/pr1.png" width="40px" onclick="PrintPanel();"    
        align="right" style="cursor:pointer;"/>
                    
     <div class="body" runat="server" id="divPrint" align="center">
 
      <table cellpadding="0" cellspacing="0" style="height: 66px; width: 901px; border-bottom:1px solid #000;"> 
   
    <tr>

    <td class="style1" rowspan="2">
        <asp:Image ID="Image3" ImageUrl ="../Image/Sliet14.jpg" style="border-radius:50%" Width="90" Height="80" runat="server" />


    </td>

    <td colspan="3"  style="color: #000080; font-weight: bold; font-size: 28px" 
            align="center" class="style10">
        College Name, Place <br />
       
    </td>
     <td class="style1" rowspan="2">
        
    </td>
    </tr>

     <tr>

    <td colspan="3"  style="color: #000080; font-weight: bold; font-size: 16px" 
             align="center">
        Raba Sheet for Session 2021-2022<br />
        Dowload Date & Time : <asp:Label ID="lblDate" runat="server">13/02/2021 02:00 PM</asp:Label>
       
    </td>

    <td colspan=""  style="color: #000080; font-weight: bold; font-size: 18px" rowspan="3"
             align="center">
      Form No : <asp:Label ID="Label1" runat="server">19002540</asp:Label>
       
    </td>
    </tr>


    </table>



       <table cellpadding="5" cellspacing="5" style="height: 143px; width: 901px;" >

      <tr>
    <td >
  <b>Roll No</b>
    </td>
    <td >
        <b><asp:Label ID="lblrollno" runat="server" ></asp:Label></b>
    </td>

   <%--
    <td colspan="" rowspan="5" align="left" valign="top">
  
                            <asp:Image ID="imgstudnet"   Width="120px" Height="120px" runat="server" /><br />
                            <asp:Image ID="Image1"   Width="120px" Height="50px" runat="server" />
                                       
    </td>--%>

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
   Program/Course
    </td>
    <td >
   <asp:Label ID="lblProgramCourse" runat="server" Text=""></asp:Label>
    </td>
    </tr>

    <tr>
    <td >
   Type of Exam
    </td>
    <td >
   <asp:Label ID="lblmain" runat="server" Text="">Main Exam</asp:Label>
    </td>
    </tr>
    <tr>
    <td class="style6">
      Semester/Year
    </td>
    <td >
  <asp:Label ID="lblsemyear" runat="server" Text=""></asp:Label>
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
   <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
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
        <b>Subjects Name</b></td>
      
     </tr>

 
 <%--<tr>
    <td >
        Mobile No. (Parents).</td>
    <td >
   <asp:Label ID="lblrmobile2" runat="server" Text=""></asp:Label>
    </td>
   <td >
      E-mail
    </td>
       <td>
  <asp:Label ID="lblremail" runat="server" Text=""></asp:Label>
    </td>
    </tr>--%>
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
         <asp:BoundField HeaderText="Subject Code" DataField="Subjectcode"></asp:BoundField>
                 <asp:BoundField HeaderText="Subject Name" DataField="Subject"></asp:BoundField>
                 <asp:BoundField HeaderText="Exam Date" DataField=""></asp:BoundField>
                  <asp:BoundField HeaderText="Answer Book No" DataField=""></asp:BoundField>
                  <asp:BoundField HeaderText="Suppl Answer Book No" DataField=""></asp:BoundField>
                 <asp:BoundField HeaderText="Candidate Sign" DataField=""></asp:BoundField>
                  <asp:BoundField HeaderText="Invigilator Sign" DataField=""></asp:BoundField>
        </Columns>
           
        </asp:GridView>
      </td>
     </tr>
 
 <%--<tr>
    <td colspan="4" class="style2" style="text-align: justify">
      <br />
      <br />
      I hereby declare that all particulars sated in this application form are true to the best of my knowledge and befit. I have read and understood all provision of regular admission. rules & regulation of University and agree to abide by them. I also affirm that I fulfill the eligibility requirements for the courses applied. in events of submission of frequents or incorrect information or suppression or distortion of any fact like educational qualification. Marks, Nationality etc. I understand that my admission/degree is liable for cancellation. I further understand that my admission is purely provisional subject to the verification of the eligibility conditions. I undersigned that none of my information should be given to anyone under the Right to Information Act 2005 without my prior written permission that has valid personal id attached with it.

        <br />
        <br />
        I will follow all the rules and regulations regarding attendance and Examination as per University/State Government/Central Government or any other regulatory body which governs the course I have taken admission in. During the course of study, I will be a regular student of the University and will not be doing any job in government / Semi Government /Private Sector and if I pursue any course with my job I will be on educational leave from my job/ Department and document of the same will be submitted in the university. The University at any point can cancel my admission due to non fulfill of eligibility criteria as laid down by government I will have no objection in the same.
   </td>

    </tr>--%>
  
 <tr>
    
   
  
       <td colspan="2">
        <br />
        <br />
    Center Supdt.
    </td>
    </tr>
  

    </table>
    <br />
    <br />

  
     
    </div>

  <%--  <div class="row ">
    <div class="col-md-12 text-end">
           <input id="btnprintaa" type="button"  onclick="PrintPanel()" value="Print" class="btn btn-secondary"
             style="color: #fff; width: 140px; height: 36px; font-size:24px; font-weight: 900; padding:0px 0px 20px 0px;"/>
    </div>
     </div>--%>
       </div>
    </form>
</body>
</html>
