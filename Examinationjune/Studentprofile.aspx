<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StudentProfile.aspx.vb" Inherits="StudentAdm_StudentProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />

    <style>
    
    body
       {
    background:#f2f3f5;
     overflow-x: hidden;    
     background-repeat: no-repeat;
     background-size: 100% 100%;
       }
 input {
	 position: absolute;
	 opacity: 0;
	 z-index: -1;
}
 .row {
	 display: flex;
}
 .row .col {
	 flex: 1;
}
 .row .col:last-child {
	 margin-left: 1em;
}
/* Accordion styles */
 .tabs {
	 
	 overflow: hidden;
	
}
 .tab {
	 width: 100%;
	 color: white;
	 overflow: hidden;
}
 .tab-label {
	 display: flex;
	 justify-content: space-between;
	 padding: 1em;
	 background: #152837;
	 font-weight: bold;
	 cursor: pointer;
	/* Icon */
}
 .tab-label:hover {
	 background: #1a252f;
}
 .tab-label::after {
	 content: "\276F";
	 width: 1em;
	 height: 1em;
	 text-align: center;
	 transition: all 0.35s;
}
 .tab-content {
	 max-height: 0;
	 padding: 0 1em;
	 color: #2c3e50;
	 background: white;
	 transition: all 0.35s;
}
 .tab-close {
	 display: flex;
	 justify-content: flex-end;
	 padding: 1em;
	 font-size: 0.75em;
	 background: #2c3e50;
	 cursor: pointer;
}
 .tab-close:hover {
	 background: #1a252f;
}
 input:checked + .tab-label {
	 background: #1a252f;
}
 input:checked + .tab-label::after {
	 transform: rotate(90deg);
}
 input:checked ~ .tab-content {
	 max-height: 100vh;
	 padding: 1em;
}
 .card-header
 {
     background-color:#152837;
     color:#fff;
     font-weight:500;
     font-size:22px;
     letter-spacing:1px;
     }
   
   .hiddencol
   {
       display:none;
   }
   .lblhead
   {
       Color:#15283c;
       font-size:18px;
       font-weight:400;
   }
   .subheading h4
    {
        color:#15283c;
    font-size:22px;
    font-weight:500;
    margin-bottom:12px;
   
    }
      .line
   {
       content:" ";
       width:100%;
       height:1px;
        background-color:#e1e2e3;
        margin-top:8px;
        margin-bottom:18px;
        
       
   }
      .backbotton
{
    padding-top:4px;
    font-size:22px;
    font-weight:600;
    color:#fff;
    
}

.backbotton:hover
{
    color:#f2f3f5;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="container maincontainer">
  <div class="row justify-content-center">
        
         
<%--Start Accordian--%>
       <div class="col-md-12">
<div class="card-header d-flex">
    <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
  &nbsp &nbsp  <asp:Label ID="Label14" runat="server" Text="Label">My Profile</asp:Label> </div>


  <div class="card-body bg-white mb-2">
   
        
        <div class="row">
        <div class="col-md-12">
        <div class="d-flex">
        
        <table width="50%">
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="lblName" runat="server" CssClass="lblhead"> Name :</asp:Label>
            
           
          
        </td>
        <td width="60%">
         <asp:Label ID="lblStuName" runat="server" Text=""></asp:Label>
           
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label3" runat="server" CssClass="lblhead">Gender :</asp:Label>
           
        </td>
        <td width="60%">
         <asp:Label ID="lblgender" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
             <asp:Label ID="Label4" runat="server" CssClass="lblhead">DOB :</asp:Label>
          
        </td>
        <td width="60%">
           <asp:Label ID="lblDOB" runat="server" Text="">NA</asp:Label>
             
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
           <asp:Label ID="Label7" runat="server" CssClass="lblhead">Department :</asp:Label>
            
        </td>
        <td width="60%">
        <asp:Label ID="lblDepartment" runat="server" Text="">NA</asp:Label>
             
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label8" runat="server" CssClass="lblhead">Program :</asp:Label>
           
        </td>
        <td width="60%">
         <asp:Label ID="lblProgram" runat="server" Text="">NA</asp:Label>
             
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label9" runat="server" CssClass="lblhead">Semester :</asp:Label>
           
        </td>
        <td width="40%">
          <asp:Label ID="lblSemester" runat="server" Text="">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
             <asp:Label ID="Label10" runat="server" CssClass="lblhead">Student Type :</asp:Label>
          
        </td>
        <td width="60%">
         <asp:Label ID="lblStudenttype" runat="server" Text="">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label22" runat="server" CssClass="lblhead">Mobile :</asp:Label>
           
        </td>
        <td width="60%">
         <asp:Label ID="lblstumobile" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label5" runat="server" CssClass="lblhead">Email :</asp:Label>
           
        </td>
        <td width="60%">
         <asp:Label ID="lblstuemail" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="lblstuadhar" runat="server" CssClass="lblhead">Adhar No :</asp:Label>
           
        </td>
        <td width="60%">
         <asp:Label ID="Lbladhar" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>

        </table> 
        <table width="50%">
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label1" runat="server" CssClass="lblhead">Academic Session :</asp:Label>
            
           
          
        </td>
        <td width="60%">
         <asp:Label ID="LblAcdmic" runat="server" Text=""></asp:Label>
           
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label6" runat="server" CssClass="lblhead"> Admission No :</asp:Label>
            
           
          
        </td>
        <td width="60%">
         <asp:Label ID="Lbladmisinno" runat="server" Text=""></asp:Label>
           
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label16" runat="server" CssClass="lblhead">Enrollment No :</asp:Label>
            
           
          
        </td>
        <td width="60%">
         <asp:Label ID="Lblenrolment" runat="server" Text=""></asp:Label>
           
        </td>
        </tr>
        <tr width="100%">
        <td width="40%">
            <asp:Label ID="Label11" runat="server" CssClass="lblhead">Roll No :</asp:Label>
            
           
          
        </td>
        <td width="60%">
         <asp:Label ID="LblRollno" runat="server" Text=""></asp:Label>
           
        </td>
        </tr>
        <tr width="100%">
        <td width="100%" colspan="2">
            
           <div class="row">
            <div class="col-md-12 d-flex justify-content-center ">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/img/collegelogo.jpg" Width="130px" Height="140px" style="border:1px solid #000;"/>
            </div>
           </div>
          
        </td>
        
        </tr>
          
         
          
          
          </table>  
           
      
      </div>
      </div>
      </div> 

      <div class="row">
       <div class="col-md-12">
           <asp:Panel ID="Pnlstueducation" Visible="false"  runat="server">
           <div class="line"></div>
       <div class="col-md-12">
       <div class="subheading">
       <h4>Qualification Detail</h4>
       </div>
       </div>

           <asp:GridView ID="Grideducationdetail" class="table table-bordered" AutoGenerateColumns="False" runat="server">
           <Columns>
                                                    <asp:TemplateField HeaderText="SNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSno" runat="server" Text="<%#Container.DataItemIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                            <asp:BoundField HeaderText="Qualification" DataField="Qualification"></asp:BoundField>
                            <asp:BoundField HeaderText="Passing Year" DataField="PassingYear"></asp:BoundField>
                            <asp:BoundField HeaderText="University" DataField="Institution"></asp:BoundField>
                             <asp:BoundField HeaderText="Stream" DataField="Stream"></asp:BoundField>
                              <asp:BoundField HeaderText="Roll No" DataField="Roll_No"></asp:BoundField>

 
                                                   </Columns>
           </asp:GridView>

    </asp:Panel>
      </div>

      </div>
     

     <div class="row">
     <div class="col-md-12">
     <div class="line"></div>
     <div class="subheading">
       <h4> Family Detail</h4>
     </div>
     </div>
     <div class="col-md-6">
     <table width="100%">
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label17" runat="server" CssClass="lblhead">Father's Name :</asp:Label>
            
           
          
        </td>
        <td width="50%">
         <asp:Label ID="lblFathername" runat="server" Text="">NA</asp:Label>
           </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label19" runat="server" CssClass="lblhead">Mother's Name :</asp:Label>
           
        </td>
        <td width="50%">
          <asp:Label ID="lblMothername" runat="server" Text="">NA</asp:Label>
             
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
             <asp:Label ID="Label21" runat="server" CssClass="lblhead">Father's Occupation :</asp:Label>
          
        </td>
        <td width="50%">
           <asp:Label ID="Lblfatherocupation" runat="server">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
           <asp:Label ID="Label24" runat="server" CssClass="lblhead">Mother's Occupation :</asp:Label>
            
        </td>
        <td width="50%">
      <asp:Label ID="lblmotherocupation" runat="server">NA</asp:Label>
                   
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label28" runat="server" CssClass="lblhead">Father's income :</asp:Label>
           
        </td>
        <td width="50%">
        <asp:Label ID="Lblfatherincm" runat="server">NA</asp:Label>
                 
        </td>
        </tr>
        </table> 
      
     </div>
    
     <div class="col-md-6">
     <table width="100%">
       <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label32" runat="server" CssClass="lblhead">Guardian Name :</asp:Label>
           
        </td>
        <td width="50%">
          <asp:Label ID="lblguardinName" runat="server" Text="">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
             <asp:Label ID="Lablel" runat="server" CssClass="lblhead">Relation :</asp:Label>
          
        </td>
        <td width="50%">
         <asp:Label ID="LblgrdnRlsn" runat="server" Text="">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label37" runat="server" CssClass="lblhead">Guardian Email :</asp:Label>
           
        </td>
        <td width="50%">
         <asp:Label ID="LblGEmail" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label36" runat="server" CssClass="lblhead">Guardian Mobile :</asp:Label>
           
        </td>
        <td width="50%">
         <asp:Label ID="LblGmobile" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label41" runat="server" CssClass="lblhead">Landline No :</asp:Label>
           
        </td>
        <td width="50%">
         <asp:Label ID="lblLineno" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        

        </table>
     </div>
     
     </div>


     <div class="row">
     <div class="col-md-12">
     <div class="line"></div>
     </div>
     <div class="col-md-6">
     <div class="row">
     <div class="col-md-12">

     <div class="subheading">
     <h4>Permanent Address</h4>    
      </div>
     </div>
     </div>
     <table width="100%">
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label18" runat="server" CssClass="lblhead">Address 1:</asp:Label>
            
           
          
        </td>
        <td width="50%">
         <asp:Label ID="LblPAdd1" runat="server" Text="">NA</asp:Label>
           </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label23" runat="server" CssClass="lblhead">Address 2:</asp:Label>
           
        </td>
        <td width="50%">
          <asp:Label ID="LblPadd2" runat="server" Text="">NA</asp:Label>
             
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
             <asp:Label ID="Label30" runat="server" CssClass="lblhead">Country :</asp:Label>
          
        </td>
        <td width="50%">
           <asp:Label ID="LblPcountry" runat="server">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
           <asp:Label ID="Label35" runat="server" CssClass="lblhead">State :</asp:Label>
            
        </td>
        <td width="50%">
      <asp:Label ID="LblPstate" runat="server">NA</asp:Label>
                   
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label39" runat="server" CssClass="lblhead">City:</asp:Label>
           
        </td>
        <td width="50%">
        <asp:Label ID="LblPcity" runat="server">NA</asp:Label>
                 
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label44" runat="server" CssClass="lblhead">Pincode :</asp:Label>
           
        </td>
        <td width="50%">
        <asp:Label ID="LblPpincode" runat="server">NA</asp:Label>
                 
        </td>
        </tr>
        </table>
     </div>
     <div class="col-md-6">
      <div class="row">
     <div class="col-md-12">
     <div class="subheading">
     <h4>Correspondence Address </h4>    
      </div>
     </div>
     </div>
     <table width="100%">
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label20" runat="server" CssClass="lblhead">Address 1:</asp:Label>
            
           
          
        </td>
        <td width="50%">
         <asp:Label ID="LblCAdd1" runat="server" Text="">NA</asp:Label>
           </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label33" runat="server" CssClass="lblhead">Address 2:</asp:Label>
           
        </td>
        <td width="50%">
          <asp:Label ID="LblCadd2" runat="server" Text="">NA</asp:Label>
             
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
             <asp:Label ID="Label43" runat="server" CssClass="lblhead">Country :</asp:Label>
          
        </td>
        <td width="50%">
           <asp:Label ID="LblCcountry" runat="server">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
           <asp:Label ID="Label46" runat="server" CssClass="lblhead">State :</asp:Label>
            
        </td>
        <td width="50%">
      <asp:Label ID="LblCstate" runat="server">NA</asp:Label>
                   
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label48" runat="server" CssClass="lblhead">City:</asp:Label>
           
        </td>
        <td width="50%">
        <asp:Label ID="LblCcity" runat="server">NA</asp:Label>
                 
        </td>
        </tr>
        <tr width="100%">
        <td width="50%">
            <asp:Label ID="Label50" runat="server" CssClass="lblhead">Pincode :</asp:Label>
           
        </td>
        <td width="50%">
        <asp:Label ID="LblCpincode" runat="server">NA</asp:Label>
                 
        </td>
        </tr>
        </table>
    
     </div>
     </div>

     <div class="row">
     <div class="col-md-12">
     
     <div class="line"></div>
     <div class="row">
     <div class="col-md-12">
     <div class="subheading">
     <h4>Personal Detail</h4> 
      </div>
     </div>
     </div>
     <table width="100%">
       <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label2" runat="server" CssClass="lblhead">Nationality :</asp:Label>
           
        </td>
        <td width="70%">
          <asp:Label ID="Lblnationlity" runat="server" Text="">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="30%">
             <asp:Label ID="Label13" runat="server" CssClass="lblhead">Religion :</asp:Label>
          
        </td>
        <td width="70%">
         <asp:Label ID="Lblreligion" runat="server" Text="">NA</asp:Label>
              
        </td>
        </tr>
        <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label15" runat="server" CssClass="lblhead">Caste Category:</asp:Label>
           
        </td>
        <td width="70%">
         <asp:Label ID="Lblcaste" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label26" runat="server" CssClass="lblhead">Blood group :</asp:Label>
           
        </td>
        <td width="70%">
         <asp:Label ID="Lblblood" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>

        <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label12" runat="server" CssClass="lblhead">Height :</asp:Label>
           
        </td>
        <td width="70%">
         <asp:Label ID="Lblheight" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label25" runat="server" CssClass="lblhead">Weight :</asp:Label>
           
        </td>
        <td width="70%">
         <asp:Label ID="Lblweight" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label29" runat="server" CssClass="lblhead">Hobbies :</asp:Label>
           
        </td>
        <td width="70%">
         <asp:Label ID="Lblhobbies" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        <tr width="100%">
        <td width="30%">
            <asp:Label ID="Label34" runat="server" CssClass="lblhead">Physical Disability :</asp:Label>
           
        </td>
        <td width="70%">
         <asp:Label ID="Lblphysical" runat="server" Text="">NA</asp:Label>
          
        </td>
        </tr>
        

        </table>
     
     </div>
     </div>

          
        
        
       

      <%--<div class="tab mt-3">
        <input type="checkbox" id="chck2">
        <label class="tab-label" for="chck2">My Information For Editing</label>
        <div class="tab-content">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. A, in!
        </div>
      </div>--%>
   
  </div>
</div>
  </div>
</div>
    </form>
</body>
</html>
