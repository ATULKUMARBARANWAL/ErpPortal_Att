<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Session.aspx.vb" Inherits="Session" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
     
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <style>
        body
        {
            overflow-x: hidden;
            background-color: #f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
        
           .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:8px;
       margin-bottom:8px;
       
   }
        .grass5:hover
        {
            background-color: #1ed085;
            color: White;
        }
        .grass5
        {
            background-color: #fff;
            border: 1px solid #1ed085;
            color: #1ed085;
            text-align: center;
            height: 40px;
            padding-top: 4px;
        }
        
        .icongrass
        {
            font-size: 25px;
        }
        .Submit
        {
            font-size: 18px !important;
            font-weight: 500;
            height: 40px;
            cursor: pointer;
            color: White;
            background-color: #1ed085;
            border: none;
            width: 20%;
        }
        .Submit:hover
        {
            color: White;
            background-color: #1aad6f;
            border: none;
            box-shadow: 0px 1px 5px 1px #dcdcdc;
        }
        
        .card-header
        {
            background-color: #152837;
            color: #f2f3f5;
            font-weight: 500;
            font-size: 22px;
            letter-spacing: 1px;
        }
        .labeltext
        {
            color: #15283c;
            font-size: 17px;
            font-weight: 450;
            letter-spacing: 1px;
        }
        
        .hiddencol1
        {
            display:none;
            }
    </style>
<%--<script type="text/javascript">
    $(function () {
        var today = new Date();
        var month = ('0' + (today.getMonth() + 1)).slice(-2);
        var day = ('0' + today.getDate()).slice(-2);
        var year = today.getFullYear();
        var date = year + '-' + month + '-' + day;
        $('[ID*=txtFromdate]').attr('min', date);
    });
</script>
 <script type="text/javascript">
     $(function () {
         var today = new Date();
         var month = ('0' + (today.getMonth() + 1)).slice(-2);
         var day = ('0' + today.getDate()).slice(-2);
         var year = today.getFullYear();
         var date = year + '-' + month + '-' + day;
         $('[ID*=txtfromfinance]').attr('min', date);
     });
</script>
<script type="text/javascript">
    $(function () {
        var today = new Date();
        var month = ('0' + (today.getMonth() + 1)).slice(-2);
        var day = ('0' + today.getDate()).slice(-2);
        var year = today.getFullYear();
        var date = year + '-' + month + '-' + day;
        $('[ID*=txttofinance]').attr('min', date);
    });
</script>--%>

</head>
<body>
    <form id="form1" runat="server">
     <main class="my-form">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:Panel ID="pnlSession" runat="server" Visible="True">
       <div class="container-fluid">
        <div class="row justify-content-center mt-2">
         <div class="col-xs-12 col-sm-12 col-md-12">
           <div class="card input-sm">
             <div class="card-header"><h5 class="enquiry">Session</h5></div>
               <div class="card-body">
              
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>

                  <div class="form-group row">
                  <div class="col-md-1"></div>
                  <asp:Label ID="lblSession" class="col-md-3 col-form-label text-md-left labeltext" runat="server">Session :</asp:Label>
                   <div class="col-md-6">
                    <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" CssClass="form-select">
                        
                              <%--<asp:ListItem>2018-19</asp:ListItem>--%>
                             <%-- <asp:ListItem>2019-20</asp:ListItem>--%>
                              <%--<asp:ListItem>2020-21</asp:ListItem>--%>
                             <%-- <asp:ListItem>2021-22</asp:ListItem>
                              <asp:ListItem>2022-23</asp:ListItem>
                              <asp:ListItem>2023-24</asp:ListItem>
                              <asp:ListItem>2024-25</asp:ListItem>--%>

                                           
                     </asp:DropDownList>
                    </div>
                  </div>
                 
                <div class="form-group row">
                <div class="col-md-1"></div>
                 <asp:Label ID="lblAcademicyear" class="col-md-3 col-form-label text-md-left labeltext" runat="server">Academic year :</asp:Label>
                  <div class="col-md-6">
                  <asp:TextBox ID="txtAcademicyear" class=" form-control" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="form-group row">
                  <div class="col-md-1"></div>
                    <asp:Label ID="lblAcademicyearft" class="col-md-3 col-form-label text-md-left labeltext" runat="server">Academic year :</asp:Label>
                   <div class="col-xs-6 col-sm-6 col-md-6">

                        <table width="100%">
                        <tr width="100%">
                        <td width="48%">
                        <asp:TextBox ID="txtFromdate" runat="server"  TextMode="Date" class="form-control"></asp:TextBox>
                        </td>
                        <td width="4%">To</td>
          
                        <td width="48%">

                        <asp:TextBox ID="txtTodate" runat="server" TextMode="Date" class="form-control"></asp:TextBox>
                        </td>
                        </tr>
                        </table>
                         <asp:CompareValidator ID="CompareValidator1" ValidationGroup="MyValidation" ForeColor = "Red" Display="Dynamic" runat="server" ControlToValidate = "txtFromdate" ControlToCompare = "txtTodate" Operator = "LessThan" Type = "Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                     </div>
                     </div>

                     <div class="form-group row">
                     <div class="col-md-1"></div>
                      <asp:Label ID="lblFinancialyearft" class="col-md-3 col-form-label text-md-left labeltext" runat="server">Financial year :</asp:Label>
                     <div class="col-xs-6 col-sm-6 col-md-6">
                             <table width="100%">
                             <tr width="100%">
                             <td width="48%">
                             <asp:TextBox ID="txtfromfinance" runat="server" TextMode="Date" class="form-control"></asp:TextBox>
                             </td>
                             <td width="4%">To</td>

                             <td width="48%">
                             <asp:TextBox ID="txttofinance" runat="server" TextMode="Date" class="form-control" ></asp:TextBox>
                             </td>
                             </tr>
                             </table>
                              <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" ValidationGroup="MyValidation" ForeColor = "Red" runat="server" ControlToValidate = "txtfromfinance" ControlToCompare = "txttofinance" Operator = "LessThan" Type = "Date" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                         </div>
                         </div>
                            
                             </ContentTemplate>

                             </asp:UpdatePanel>

                                <br />
                                                      
                         <div class="form-group row">
                         <div class="col-md-12 text-center">
                           <asp:LinkButton ID="btnrefresh" Class="btn grass5" runat="server">
                            <i class="fa fa-refresh icongrass mt-1"></i>
                           </asp:LinkButton> &nbsp;&nbsp;
                           <asp:Button ID="btnSessionAdd" ValidationGroup="MyValidation" class="btn Submit" runat="server" Text="Save"></asp:Button>&nbsp;&nbsp;
                         </div>
                         </div>

                          <div class="row">
          <div class="col-md-12">
          <div class="line"></div>
          </div>
          </div>

                         <div class="container-fluid bg-white mt-3 maincontainer">
       <asp:Panel ID="Panel1" runat="server" ScrollBars="auto" Visible="true">
           <asp:GridView ID="GrdViewsession" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" DataKeyNames="Sessionid"
            Class="table table-bordered"   OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting" >
            
              <Columns>
           
        <asp:TemplateField HeaderText="Modify">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkModify" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'   runat="server" ><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>
                      </ItemTemplate>
        </asp:TemplateField>

           
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
                <asp:BoundField HeaderText="Sessionid" ItemStyle-CssClass="hiddencol1" HeaderStyle-CssClass="hiddencol1" DataField="Sessionid"></asp:BoundField>
                <asp:BoundField HeaderText="Session" DataField="Session" ReadOnly="true"></asp:BoundField>
                <asp:BoundField HeaderText="Academic Year" DataField="Academicyear" ReadOnly="true" ></asp:BoundField>
                <asp:BoundField HeaderText="AY Start From" DataField="Academicfromdate" ReadOnly="true"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                <asp:BoundField HeaderText="AY End To" DataField="Academictodate" ReadOnly="true "  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                <asp:BoundField HeaderText="FY Start From" DataField="FinancialFromdate" ReadOnly="true"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                <asp:BoundField HeaderText="FY End To" DataField="FinancialTodate" ReadOnly="true"  DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
            <asp:TemplateField HeaderText="Delete">
                   <ItemTemplate>
                       <asp:ImageButton ID="lnkDelete" CommandName="Delete" CausesValidation="False" ImageUrl="../ExaminationNImages/img/url.png" 
                          
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  Width="20px" runat="server" />
                   </ItemTemplate>
                  
             </asp:TemplateField>
            </Columns>
            
            
            </asp:GridView>
         </asp:Panel>
         </div>
                   </div>
                </div>
            </div>
         </div>
      </div>
     </asp:Panel>
     </main>
    </form>
</body>
</html>


