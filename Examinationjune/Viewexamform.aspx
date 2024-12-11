<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Viewexamform.aspx.vb" Inherits="StudentAdm_Viewexamform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%------------------------------------Designed by Er Mohit Kumar --------------------------%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
   
   <style type="text/css">
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
   </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div class="bg-white">
              
         <img alt="" src="../img/sarallogo.png" width="40px" onclick="PrintPanel();"    
        align="right" style="cursor:pointer;"/>
                    
     <div class="body" runat="server" id="divPrint" align="center">
 
      <table cellpadding="0" cellspacing="0" style="height: 66px; width: 901px; border-bottom:1px solid #000;"> 
   
    <tr>

    <td class="style1" rowspan="2">
    <asp:LinkButton ID="backstudent" runat="server" CssClass="backbotton"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    &nbsp
        <asp:Image ID="Image3" ImageUrl ="../img/sarallogo.png" style="border-radius:50%" Width="90px" Height="80px" runat="server" />


    </td>

    <td colspan="3"  style="color: #000080; font-weight: bold; font-size: 28px" 
            align="center" class="style10">
       <asp:Label ID="lblcollege" runat="server" Text="19002540"></asp:Label> <br />
       
    </td>
     <td class="style1" rowspan="2">
        


    </td>
    </tr>

     <tr>

    <td colspan="3"  style="color: #000080; font-weight: bold; font-size: 16px" 
             align="center">
        Exam Form for Session : <asp:Label ID="Lblsession" runat="server" Text="Label"></asp:Label><br />
        Dowload Date & Time : <asp:Label ID="lblDate" runat="server">13/02/2021 02:00 PM</asp:Label>
        
    </td>

    <td colspan=""  style="color: #000080; font-weight: bold; font-size: 18px" rowspan="3"
             align="center">
      Form No : <asp:Label ID="Lblexamform" runat="server" Text="19002540"></asp:Label>
       
    </td>
    </tr>


    </table>



       <table cellpadding="5" cellspacing="5" style="height: 143px; width: 901px;" >

      <tr>
    <td >
  <b>Roll No</b>
    </td>
    <td >
        <b><asp:Label ID="lblrollno" runat="server" Text="100180168168"></asp:Label></b>
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
     <b><asp:Label ID="lblenrollment" runat="server" Text="E191646743475875"></asp:Label></b>
    </td>
  
    </tr>

     <tr>
    <td >
   Program/Course
    </td>
    <td >
   <asp:Label ID="lblProgramCourse" runat="server" Text="B.Tech in Computer Science and Engineering"></asp:Label>
    </td>
    </tr>

    <tr>
    <td >
   Exam Name
    </td>
    <td >
  <asp:Label ID="Lblexamname" runat="server" Text="Main Exam"></asp:Label> &nbsp (&nbsp<asp:Label ID="lblexamtype" runat="server" Text="Main Exam"></asp:Label>&nbsp)
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
<%--<tr width="100%" >
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
          <%--</th>
          <th width="10%"> <asp:Button ID="btnverify" CssClass="btn btn-secondary" runat="server" Text="OK" /></th>
         </tr>--%>
           
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
    <br />
    <br />

  
     
    </div>

       </div>
    
    </div>
    </form>
</body>
</html>
