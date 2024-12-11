<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Showstudents.aspx.vb" Inherits="Examinationjune_Showstudents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <title></title>
    <style>
    
    #backbotton
{
     font-size:22px;
    font-weight:600;
    color:#7c858f;
   
    }

    #backbotton:hover
{
     color:#15283c;
    }
    
      body 
{
    background-color:#f2f3f5;
}

  .Labels
       {
           
            font-size:17px;
    font-weight:500;
    color:#000;
           }
.maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
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

    </style>
</head>
<body>
    <form id="form1" runat="server">

     <div class="container-fluid maincontainer">

     <div class="row">
            <div class="col-md-5 mt-1">
            <table>
    <tr width="100%">
    <td width="30%">
      <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
    <td width="70%">
       <div class="heading1">
   <h3>Students</h3>
   </div>
    </td>
    </tr>
    </table>
             
              
            </div>
            
            <div class="col-md-4 mt-1">
          
        
     
            </div>


            <div class="col-md-3 mt-1">
            <table>
            <tr width="100%">

          
           

            <td width="50%">
             <asp:Label ID="Label1" runat="server" class="Labels" Text="Academic Year :"></asp:Label>
             </td>
            <td width="50%">
             <asp:Label ID="lblAcademicyear" class="Labels" runat="server" Text=" Academic Year "></asp:Label>
            </td>
            
            </tr>
            </table>
                
            </div>
             <div class="line">
         </div>
           
             
             
            </div>
     
          <asp:GridView ID="grdstudents" GridLines="Both"  Width="100%" 
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered bg-white rounded_corners">
                <Columns>
                  <asp:TemplateField HeaderText="S.No.">
                  <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                 </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField HeaderText="Admission No" DataField="AdmissionNo"></asp:BoundField>
                 <asp:BoundField HeaderText="Student"  DataField="Student"></asp:BoundField>
          
                 
                  <asp:BoundField HeaderText="Sem/Year" DataField="Sem"></asp:BoundField>
                  
                  <asp:BoundField HeaderText="Mobile" DataField="Mobile"></asp:BoundField>
                   <asp:BoundField HeaderText="Emnail" DataField="Email"></asp:BoundField>
              <asp:BoundField HeaderText="EnrollmentNo" DataField="EnrollmentNo"></asp:BoundField>
              <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
              <asp:BoundField HeaderText="Adhar No" DataField="AdharNo"></asp:BoundField>


                </Columns>
           
                  </asp:GridView>

                  </div>
    </form>
</body>
</html>
