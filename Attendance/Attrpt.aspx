<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Attrpt.aspx.vb" Inherits="Attendance_Attrpt" %>

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
        /* Basic styling for the GridView */
.table {
    width: 100%;
    border-collapse: collapse;
    font-family: 'Arial', sans-serif;
    background-color: #f8f9fa;
}

.table th, .table td {
    padding: 12px;
    text-align: left;
    border-bottom: 1px solid #dee2e6;
    font-size: 14px;
}

.table th {
    background-color:#ffffff;
    color: black;
    font-weight: bold;
    text-transform: uppercase;
    letter-spacing: 1px;
}

/* Hover effects for table rows */

/* Alternating row colors */
.table tbody tr:nth-child(odd) {
    background-color: #ffffff;
}

.table tbody tr:nth-child(even) {
    background-color: #ffffff;
}

/* SNO column styling */
.table .sno-column {
    width: 60px;
    font-weight: bold;
}

/* Add some padding to the cells for better alignment */
.table td {
    padding: 15px 12px;
}

/* Responsive design for smaller screens */
@media (max-width: 768px) {
    .table th, .table td {
        font-size: 12px;
        padding: 10px 8px;
    }

    .table {
        font-size: 12px;
    }

    .table .sno-column {
        width: 50px;
    }
}

/* Custom scrollbar styling for the table */
.custom-scrollbar-css {
    max-height: 400px;
    overflow-y: auto;
}

/* Styling for the SNO column in the ItemTemplate */
#grdPrograms .sno-column {
    text-align: center;
}

/* Additional hover effect for header */

/* Styling for the labels in the GridView */
.grid-label {
    font-size: 14px;
    font-weight: normal;
    color: #555;
}

/* Styling for the Status column */
.table .status-column {
    color: #28a745;  /* Green for "active" or "completed" status */
    font-weight: bold;
}

.table .status-column.inactive {
    color: #dc3545;  /* Red for "inactive" or "pending" status */
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
                                     <div>
                                <span>(</span>
                <asp:Label ID="lblprogram" runat="server" class="Labels" Text=" Program "></asp:Label> &nbsp<asp:Label ID="Label11" runat="server" Text=">>"></asp:Label>&nbsp 
                
                 <asp:Label ID="Lblsection" class="Labels" runat="server" Text="Label"></asp:Label>
            
              <span>)</span>
          </div>
                                      </div>
                              
                                </div>
                     
                               <div class="col-md-6 text-end"style="margin-left:10rem;">
       <asp:LinkButton ID="Download" runat="server" CssClass="Icons" OnClick="Download_Click">
    <i class="fa fa-download fa-2" style="font-size:larger;"></i>
</asp:LinkButton>
                           
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
                       
                       <div class="row" style="width:92%;margin-left:4%;">
                       <div class="col-md-12">
                                  <div class="row mt-3 " >
                          
                                <div class="col-md-3" style="">
                               <asp:Label ID="totalCountLabel" runat="server" CssClass="total-count" style="display:flex; justify-content: flex-end;margin-right:6px; font-size:x-large;color:black;font-weight:bolder;"></asp:Label>
                                </div>

          <div class="col-md-9 text-end">
                <table width="100%">
                 <tr width="100%">
                   <td width="45%">
                   From : 
                         </td>
                   <td width="25%" >
                       <datewithoutFormat:textbox ID="TxtFromDate" width="100%" class="form-control" runat="server"></datewithoutFormat:textbox>
                                       
                    </td>
                    <td>
                   To : 
                         </td>
                    
                   <td width="25%" >
                   <datewithoutFormat:textbox ID="TxtTodate" width="100%" class="form-control" runat="server"></datewithoutFormat:textbox>
                                            
                 
                    </td>
                    
                 </tr>
              </table>         
          </div>
                                </div>
                        


                                <div class="row mt-3">
                                
                                <div class="col-md-6">
              
          </div>

         
                                </div>





                                <div class="row mt-3">
                                <div class="col-md-12 mb-2">
    <small>
        <strong>Instructions <i class="fa-solid fa-circle-info"></i>:</strong>
       
                <i class="fa-solid fa-star" style="color: #1ed085;"></i> here, user are able see the attendence of "students" between any two days.
           
                <i class="fa-solid fa-star" style="color: #1ed085;"></i>"User also able to download the Attendence Report.
            
    </small>
</div>
    <div class="col-md-12">
        <div class="custom-scrollbar-css mt-3">
            
        <asp:GridView ID="grdPrograms" runat="server" Width="100%" 
    CellPadding="0" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" 
    CssClass="table table-bordered" OnRowDataBound="grdPrograms_RowDataBound" DataKeyNames="studentID">
    <Columns>
     
        <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

     
        <asp:BoundField DataField="studentID" HeaderText="StudentID" SortExpression="studentID" Visible="false" />

      
        <asp:BoundField DataField="student" HeaderText="Student" SortExpression="student" />

     
        <asp:TemplateField HeaderText="Present Attendance">
            <ItemTemplate>
                <asp:Label ID="lblTotalAttendance" runat="server" Text='<%# Eval("TotalAttendance") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="Total Absent">
            <ItemTemplate>
                <asp:Label ID="lblTotalAbsent" runat="server" Text='<%# Eval("TotalAbsent") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="Attendance Percentage">
            <ItemTemplate>
                <asp:Label ID="lblAttendancePercentage" runat="server" Text='<%# Eval("AttendancePercentage") & "%" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>





        </div>
    </div>
</div>

                              
                                </div>
                              
                </div>
                </div> 
    </form>
 

</body>
</html>
