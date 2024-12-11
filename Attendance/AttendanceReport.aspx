<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceReport.aspx.vb" EnableEventValidation="false" Inherits="Attendance_AttendanceReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../ExaminationNCSS/ExamDahboard1.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
  <script src="../LeadJquery/jquery1.9.1.min.js" type="text/javascript"></script>
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
       font-size:22px;
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
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}

.hidden
{
    display:none;
    }
    .hiddencol
    {
        display:none;
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
 .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:4px;
       margin-bottom:8px;
       
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
    
    .Labels
    {
        font-size:17px;
        font-weight:500;
        color:#15283c;
        }
</style>
</head>
<body>
<form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 <div class="container-fluid maincontainer mt-1">
                   
                       <div class="row">
                        <div class="col-md-4">
                              <div class="heading1 d-flex">
                               <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                             &nbsp &nbsp
                                  <h5>Attendance Report</h5> 
                                     
                                      </div>
                              
                                </div>
                     
                              
                 <div class="col-md-8 hiddencol">
                  
                                <h5 >Academic Year:</h5>
                             
                         
                              <asp:DropDownList ID="ddlacademicyear" autopostback="true" class="form-select" runat="server">
                 </asp:DropDownList>
                 </div>
                               
                     
                   </div>
                       <div class="row">
                          <div class="col-md-12">
                            <div class="line">
                            </div>
                          </div>
                       </div>
                       
                       <div class="row">
                       <div class="col-md-12">
                                  <div class="row mt-3">
                                
                                <div class="col-md-6">
                <asp:Label ID="lblprogram" runat="server" class="Labels" Text=" Program "></asp:Label> &nbsp<asp:Label ID="Label11" runat="server" Text=">>"></asp:Label>&nbsp 
                 <asp:Label ID="lblSem" class="Labels" runat="server" Text="Label"></asp:Label> &nbsp  <asp:Label ID="Label13" runat="server" Text=">>"> </asp:Label>&nbsp
                 <asp:Label ID="lblsubject" class="Labels" runat="server" Text="Label"></asp:Label> &nbsp  <asp:Label ID="Label3" runat="server" Text=">>"> </asp:Label>&nbsp
                 <asp:Label ID="Lblsection" class="Labels" runat="server" Text="Label"></asp:Label>
            
              
          </div>

          <div class="col-md-6 text-end">
                <table width="100%">
                 <tr width="100%">
                   <td width="20%">
                   Dated :
                         </td>
                   <td width="35%" >
                       <datewithoutFormat:textbox ID="TxtFromDate" width="100%" class="form-control" runat="server"></datewithoutFormat:textbox>
                                            
                    </td>
                    <td width="10%" align="center">
                        To
                         </td>
                   <td width="35%" >
                   <datewithoutFormat:textbox ID="TxtTodate" width="100%" class="form-control" runat="server"></datewithoutFormat:textbox>
                                            
                 
                    </td>
                 </tr>
              </table>         
          </div>
                                </div>

                                <div class="row mt-3">
                                
                                <div class="col-md-6">
              
          </div>

          <div class="col-md-6 text-end">
         <asp:LinkButton ID="Download" runat="server" CssClass="Icons"> <i class="fa fa-download fa-2"></i></asp:LinkButton>
                           
          </div>
                                </div>





                                <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                  </div>
                                </div>
                              
                                </div>
                              
                </div>
                </div> 
    </form>
    <script src="../LeadJquery/table2excel.js" type="text/javascript"></script>
  
<script type="text/javascript">
    $("body").on("click", "#Download", function () {
        $("[id*=grdPrograms]").table2excel({
            filename: "ProgramList.xls"
        });
    });
</script>

</body>
</html>
