<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Studentupdate.aspx.vb" Inherits="StudentMis_Studentupdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
   <style type="text/css" >
         body
        {
        background:#f2f3f5;
    }
   .maincontainer {
    border: 2px solid #fff;
    padding-top: 0px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
    padding:12px 12px;
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
        margin-bottom:12px;
        
       
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
      padding:4px 10px;
      width:25%;
      border-radius:4px;
      
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
    .menurow
    {
        background-color:#15283c; 
         border-radius:8px;
         color:#fff;
        }
 .menurow .btnmenus
 {
     background-color:#15283c;
     color:#fff;
     text-align:center;
     font-size:18px;
     font-weight:500;
     text-decoration :none;
     width:100%;
 }
 
 .heading h4
    {
        color:#15283c;
    font-size:22px;
    font-weight:400;
    margin-bottom:12px;
   
    }
    .subheading h5
    {
        color:#15283c;
    font-size:18px;
    font-weight:500;
    margin-bottom:12px;
    }
    .x_content table tr td
    {
        padding-bottom:12px;
    }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="PanelCourseWise" runat="server" Visible="True">
    
     <div class="container maincontainer mt-2">
         

            <asp:Panel ID="Panel7" runat="server">
          
          
      <div class="row">


      <div class="col-md-12">
      <div class="heading d-flex">
      <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                        &nbsp    &nbsp  <h4>
                                                  Student Information
                                                </h4>
                                            </div>
    <div class="line"> </div>
  
      </div>

      <div class="row subheading">
      
          <div class="col-md-6 d-flex">
          <h5 class="d-flex">
          Program : 
              <asp:Label ID="Lblprogram" runat="server" Text="N/A"></asp:Label> (<asp:Label ID="lblprogramcode"
                  runat="server" Text="Label"></asp:Label>) <asp:Label ID="lblprgramid" runat="server"  class="hiddencol" Text="N/A"></asp:Label> 
             </h5>

          </div>      

     <div class="col-md-6 d-flex justify-content-end">
     <h5 class="d-flex">
          Admission No : 
             &nbsp <asp:Label ID="lbladmission" runat="server" Text="N/A"></asp:Label>
             </h5> 
          </div>      
          </div>
                                    <div class="col-md-12">
                                          <div class="x_panel information" >
                                         
                                            
                                             <div class="x_content">
                                          
                                            <div class="row">
                                            <div class="col-md-2"></div>
                                             <div class="col-md-8">
                                             <table class="tableform" width="100%">
                                                  <tr width="100%" >
                                                <td width="30%">
                                             Candidate Name:
                                                       
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                  <asp:TextBox ID="txtstudent" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                                          
                                                    </div>
                                                    </td>
                                                    
                                                    </tr>
                                                 <tr width="100%" >
                                                <td width="30%">
                                               Gender:
                                                       
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtgender" ReadOnly="true"  class="form-control" runat="server"></asp:TextBox>

                                                    </div>
                                                    </td>
                                                    </tr>
                                                    <tr width="100%">
                                                <td width="30%">
                                                Date of Birth:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                        <asp:TextBox ID="txtdob"  ReadOnly="true"    class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>

                                                        </div>
                                                    </td>
                                                    </tr>
                                                       <tr width="100%" class="hiddencol">
                                                <td width="30%">
                                                  Program:
                                               </td>
                                                <td width="70%">
                                              <div class="form-group">

                                                  <asp:DropDownList ID="Ddlprogram" AutoPostBack="true" ReadOnly="true" class="form-select" runat="server">
                                                  </asp:DropDownList>
                                                    </div>
                                                    </td>
                                                    </tr>
                                                    <tr class="hiddencol" width="100%">
                                                <td width="30%">
                                                Department:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                        <asp:TextBox ID="txtcoursedepartment"  ReadOnly="true"  class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                         
                                                        </div>
                                                    </td>
                                                    </tr>
                                                     <tr class="hiddencol" width="100%">
                                                <td width="30%" >
                                                 Level of Studies:
                                               </td>
                                                <td width="70%">
                                              <div class="form-group">
                                                <asp:TextBox ID="txtcoursetype"  ReadOnly="true"  class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                          
                                                    </div>
                                                    </td>
                                                    </tr>
                                                    <tr class="hiddencol" width="100%">
                                                <td width="30%">
                                                   Crsoue:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtcourse"  ReadOnly="true"  class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                         
                                                        </div>
                                                    </td>
                                                    </tr>
                                                 
                                                    <tr width="100%">
                                                <td width="30%">
                                                 Email:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                  <asp:TextBox ID="txtemailnew" class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                          
                                                           </div>
                                                    </td>
                                                    </tr>
                                                  
                                                   <tr width="100%">
                                                <td width="30%">
                                                   Mobile No:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                     <asp:TextBox ID="txtmobilenew"  class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                           
                                                        </div>
                                                    </td>
                                                    </tr>
                                                    <tr width="100%">
                                                <td width="30%">
                                                  Aadhar No :
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                          <asp:TextBox ID="txtaadhar"  class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                           
                                                        </div>
                                                    </td>
                                                    </tr>
                                                    <tr width="100%">
                                                <td width="30%">
                                                  Address:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                          <asp:TextBox ID="txtaddress"  class="form-control" runat="server"></asp:TextBox>
                                                           
                                                        </div>
                                                    </td>
                                                    </tr>
                                   
                                                    <tr width="100%" class="hiddencol">
                                                <td width="30%">
                                                 Category:
                                               </td>
                                                <td width="70%">
                                              <div class="form-group">
                                                 <asp:TextBox ID="txtcategory"  ReadOnly="true"    class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
       
                                                    </div>
                                                    </td>
                                                    </tr>

                                                        
                                                    <tr width="100%">
                                                <td width="30%">
                                                   Country:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                     <asp:DropDownList ID="Ddlcountry" AutoPostBack="true" class="form-select" runat="server">
                                                     </asp:DropDownList>
                                                     </div>
                                                    </td>
                                                    </tr>
                                                    <tr width="100%">
                                                <td width="30%">
                                                 State :
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                     <asp:DropDownList ID="Ddlstate" AutoPostBack="true" class="form-select" runat="server">
                                                     </asp:DropDownList>
                                                           </div>
                                                    </td>
                                                    </tr>
                                                     <tr width="100%">
                                                <td width="30%">
                                               City :
                                               </td>
                                                <td width="70%">
                                              <div class="form-group">
                                                  <asp:DropDownList ID="ddlcity" class="form-select" runat="server">
                                                  </asp:DropDownList>
                                                    </div>
                                                    </td>
                                                    </tr>
                                                    <tr width="100%">
                                                <td width="30%">
                                                  Pin code:
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                      <asp:TextBox ID="txtpincode" class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                            

                                                        </div>
                                                    </td>
                                                    </tr>
                                                    <tr width="100%">
                                                <td width="30%">
                                              Guardian's Mobile No:
                                                       
                                                </td>
                                                <td width="70%">
                                                
                                                 <div class="form-group">
                                                   <asp:TextBox ID="txtguardianmobil" class="form-control" ValidationGroup="stuentry" runat="server"></asp:TextBox>
                                                          
                                                    </div>
                                                    </td>
                                                    </tr>
                                               
                                                    
                                                   </table>

                                                   <div class="col-md-12 d-flex justify-content-center">
                                  
                                                        
                                                            <asp:Button ID="btnUpdate" runat="server" class="Submit" Text="Update" />
                                                       
                                    </div>
                                                   <div class="col-md-2"></div>
                                              </div>
                                           </div>

                                            </div> 
                                        </div>
                                    </div>

                               

                                    
                                    

                                </div>
       

                </asp:Panel>          

                       
                       
    </div>
    </asp:Panel>

    

    

    </form>
</body>
</html>
