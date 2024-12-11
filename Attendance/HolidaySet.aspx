<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HolidaySet.aspx.vb" Inherits="NEWFiles_HolidaySet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Bootstrap5/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
  
    <style type="text/css">
         body
        {
            
            
            overflow-x: hidden;
            background-color:#f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
        
        .grass5
{
  background-color:#1ed085;
    border:none;
    color:White;
    text-align:center;
   height: 40px;
   padding-top:4px;
    }
 .grass5:hover
 {
    background-color:#1aad6f;
     box-shadow:0px 1px 5px 1px #dcdcdc;
     border:none;
     color:White;
     
     } 
     
.icongrass
{
   font-size:25px;
    }
 .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      height: 40px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
      width:15%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
    .Clear
 {
     font-size: 18px !important;
      font-weight: 500;
      height: 40px;
      cursor: pointer;
      color:#15283c;
      background-color:#fff;
      border:1px solid #15282c;
      width:15%;
   }
   .Clear:hover
   {
       color:#fff;
       background-color:#282f42;
      border:1px solid #15282c;
    }
 .card-header
 {
     background-color:#152837;
     color:#fff;
     font-weight:500;
     font-size:22px;
     letter-spacing:1px;
     }
 .labeltext
 {
     color:#15283c;
     font-size:17px;
     font-weight:450;
     letter-spacing:1px;
 }
 
 .grdcourse
 {
   margin-right:auto;
   margin-left:auto;
   page-break-after:20px;
    
     }
      .maincontainer
{
     
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-top:24px;
    
    background-color:#fff;
    padding:12px 18px;
}
#Panel1
{
   min-height:300px;
   max-height:300px; 
    }
.hiddencol1
{
    Display:none;
    }    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
   
     <div class="container-fluid maincontainer">
  <div class="row justify-content-center mt-1">
         <div class="col-md-12">
           <div class="card">
             <div class="card-header"><h5 class="program">Set Holiday</h5></div>
               <div class="card-body"> 
               
               <div class="row">
       <div class="col-md-7">
       </div>
        <div class="col-md-5 text-end">
        <table width="100%">
                    <tr width="100%">
                    <td width="10%"></td>
                     <td width="40%"> 
                        <asp:Label ID="Label2" class="fx-bold" runat="server" >Academic Year :</asp:Label>
                     </td>
                     <td width="2%">
                     <asp:Label ID="lblacademicyear" class="fx-bold" runat="server" Text="Label"></asp:Label>
                     </td>
                     <td width="48%" hidden="true"> 
                     <asp:DropDownList ID="ddlAcademicyear"  runat="server" CssClass="form-select" AppendDataBoundItems="True"
                            AutoPostBack="True">
                         
                     </asp:DropDownList>
                         
                     </td>
                    </tr>
        </table>
       </div>
     </div>      
                    <div class="row mt-2">
                      <div class="col-md-1"></div>
                         <asp:Label ID="lblNatinalholiday" class="col-md-4 labeltext" runat="server">National Holiday :</asp:Label>
                        <div class="col-md-6">
                          <asp:DropDownList ID="ddtype" runat="server" CssClass="form-select">
                            <asp:ListItem>National</asp:ListItem>
                            <asp:ListItem>Optional</asp:ListItem>
                          </asp:DropDownList>
                         </div>              
                     </div>

                     <div class="row mt-2">
                       <div class="col-md-1"></div>
                           <asp:Label ID="lbl" class="col-md-4 labeltext" runat="server">Date From :</asp:Label>
                        <div class="col-md-6">
                          <table width="100%">
                            <tr width="100%">
                              <td width="45%"> 
                               <asp:TextBox ID="txtDateFrom" runat="server" class="form-control" Placeholder="" type="date"></asp:TextBox>
                              </td>
                              <td width="10%" align="center">
                                  <asp:Label ID="Label1" runat="server">To</asp:Label>
                              </td>
                              <td width="45%">
                               <asp:TextBox ID="txtDateTo" runat="server" class="form-control" Placeholder="" type="date"></asp:TextBox>
                              </td>
                             
                            </tr>
                          </table>
                           <asp:CompareValidator ID="CompareValidator1" ValidationGroup="MyValidation" ForeColor = "Red" Display="Dynamic" runat="server" ControlToValidate = "txtDateFrom" ControlToCompare = "txtDateTo" Operator="LessThanEqual" Type = "Date" ErrorMessage="Start date must be less than or equal to end date."></asp:CompareValidator>
                         </div>  
                      
                      </div>

                      <div class="row mt-2">
                        <div class="col-md-1"></div>
                          <asp:Label ID="lblReason" class="col-md-4 labeltext" runat="server">Reason :</asp:Label>
                           <div class="col-md-6">
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" Placeholder="" TextMode="MultiLine"></asp:TextBox>
                           </div>         
                       </div>

                                    
                       <div class="row mt-2">
                          
                         <div class="col-md-12 text-center"> 
                          <asp:Button ID="btnclear" class="btn Clear" Visible="true" runat="server" hidden="true" Text="Clear"></asp:Button>               
                                                            
                            <asp:Button ID="Button1" class="btn Submit" runat="server" Text="Save" CausesValidation="True"
                        CommandName="Insert"></asp:Button>
                        <asp:Button ID="btnupdate" class="btn Submit" Visible="false" runat="server" Text="Update"></asp:Button>               
                        
                         </div>
                       </div>  


             <div class="line mt-3"></div>

             <div class="row">
             <div class="col-md-12">
              <asp:Panel ID="Panel1" runat="server" Visible="true" ScrollBars="Auto">
            <asp:GridView ID="GridHolidayList"  AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="HolidayId"
            Class="table table-bordered mt-2" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing"  OnRowDeleting="OnRowDeleting"
        Width="100%" >
           <Columns>
           
        <asp:TemplateField HeaderText="Modify">
             <ItemTemplate>
              <asp:LinkButton ID="lnkModify" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  runat="server" ><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>
             </ItemTemplate>
             
        </asp:TemplateField>

           
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
                <%-- <asp:BoundField HeaderText="HolidayId" ItemStyle-CssClass="hiddencol1" HeaderStyle-CssClass="hiddencol1" DataField="HolidayId"></asp:BoundField>--%>
                <asp:BoundField HeaderText="Holiday Type" ReadOnly="true" DataField="Type"></asp:BoundField>
                <asp:BoundField HeaderText="Start Date" ReadOnly="True" DataField="Start"></asp:BoundField>
                <asp:BoundField HeaderText="End Date" ReadOnly="True" DataField="enddate"></asp:BoundField>
               <%-- <asp:BoundField HeaderText="Day" DataField="day"></asp:BoundField>--%>
               <asp:BoundField HeaderText="Reason" ReadOnly="True" DataField="Reason"></asp:BoundField>
           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" Delete">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDelete" CommandName="Delete"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  ImageUrl="~/img/url.png" Width="17px" OnClientClick="return confirm('Are you sure you want to delete?')" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
           <EditRowStyle Forecolor="#fff" />
          
         </asp:GridView>
         </asp:Panel>
             </div>
             </div>
                 </div>
               </div>
              </div>
           </div>
        </div>
   
    </form>
    
</body>
</html>

