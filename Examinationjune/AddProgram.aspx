<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddProgram.aspx.vb" Inherits="AddProgram" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
      width:24%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
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
     
  
    border-radius:8px;
     padding-top:24px;
    margin-top:12px;
    background-color:#fff;
    padding:12px 18px;
}
.hiddencol1
{
    Display:none;
    }
    .cap
    {
         
    }
    
    .backbotton
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    }

.backbotton:hover
{
    color:#fff;
}
    
    </style>
    <!-- Include jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- Include Select2 CSS -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />

<!-- Include Select2 JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>

<script>
    $(document).ready(function () {
        $('#multiSelect').select2({
            placeholder: 'Select Sections',
            allowClear: true
        });
    });
</script>

</head>
<body>
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
     <div class="container-fluid">
       <div class="row justify-content-center mt-1">
        <div class="col-md-12">
           <div class="card">
             <div class="card-header d-flex">
             <div>
              <asp:LinkButton ID="backbotton" class="backbotton mt-1" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>&nbsp;&nbsp;
             <h5 class="program">Class Master</h5>
             </div>
             <div>
           <asp:Label ID="classI" runat="server" style="color:White;"></asp:Label>
           
           </div>
             </div>
               <div class="card-body">
                  <asp:Panel ID="PanelInputFields" Visible="false" runat="server">
                  <asp:Button ID="ButtonShowPanel" runat="server" Text="-" OnClick="addButtonTwo" />
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                   <ContentTemplate>
                <div class="row">
                      <div class="col-md-1"></div>
                         <asp:Label ID="Label1" class="col-md-4 labeltext" runat="server">Institute :</asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="ddlfaculty" class="form-select" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                         </div>              
                     </div>
                      <div class="row mt-2">
                      <div class="col-md-1"></div>
                         <asp:Label ID="lblprogram" class="col-md-4 labeltext" runat="server">Class :</asp:Label>
                        <div class="col-md-6">
                          <asp:TextBox ID="txtprogram" runat="server" class="form-control input-sm" Placeholder="Class name"></asp:TextBox>
                         </div>              
                     </div>

                     <div class="row mt-2">
                       <div class="col-md-1"></div>
                           <asp:Label ID="lblcode" class="col-md-4 labeltext "  runat="server">Class Code :</asp:Label>
                        <div class="col-md-6">
                           <asp:TextBox ID="txtcode" runat="server" class="form-control input-sm cap" Placeholder="Class code"></asp:TextBox>
                         </div>             
                      </div>

                      <div class="row mt-2">
                        <div class="col-md-1"></div>
                          <asp:Label ID="lblprefix" class="col-md-4 labeltext" runat="server">Class Prefix :</asp:Label>
                           <div class="col-md-6">
                            <asp:TextBox ID="txtprefix" runat="server" class="form-control input-sm cap" Placeholder="Class prefix"></asp:TextBox>
                           </div>         
                       </div>
                       </ContentTemplate>
                       </asp:UpdatePanel>


                        <br /> 
                                    
                       <div class="row mt-2">
                          <div class="col-md-1"></div>
                         <div class="col-md-10 text-center ps-5">                                     
                            <asp:Button ID="btnAdd" class="btn Submit" runat="server" Text="Save"></asp:Button> 
                             <asp:Button ID="btnupdate" class="btn Submit" Visible="false" runat="server" Text="Update"></asp:Button>          
                         </div>
                         <div class="col-md-1"></div>
                       </div>
                    
                            </asp:Panel>   
                                <asp:Panel ID="PanelGrid" runat="server" Visible="True">   
    <asp:Button ID="addButton" runat="server" Text="+ Add New Class" style="background-color:White;font-size:bold;margin-left:55rem;" OnClick="AddButton_Click" />

    <div class="container-fluid bg-white mt-3 maincontainer">
        <asp:Panel ID="Panel1" runat="server" ScrollBars="auto" Visible="true">
            <asp:GridView ID="grdProgram" AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" 
              DataKeyNames="Classid" CssClass="table table-bordered" 
              OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting">


                <Columns>
                    <asp:TemplateField HeaderText="Modify">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkModify" CommandName="Edit" 
                                            CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' 
                                            runat="server">
                                <i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Cid" ItemStyle-CssClass="hiddencol1" 
                                    HeaderStyle-CssClass="hiddencol1" DataField="Classid" />

                    <asp:TemplateField HeaderText="Class">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkClass" runat="server" 
                                            Text='<%# Eval("ClassName") %>' 
                                            CommandArgument='<%# Eval("Classid") %>' 
                                            OnClick="lnkClass_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField HeaderText="Class Code" ReadOnly="true" DataField="ClassCode" />
                    <asp:BoundField HeaderText="Class Prefix" ReadOnly="true" DataField="Classprefix" />
                    <asp:BoundField HeaderText="School" ReadOnly="true" DataField="institue" />

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkDelete" CommandName="Delete" 
                                             CausesValidation="False" 
                                             OnClientClick="return confirm('Do you want to delete?')" 
                                             ImageUrl="../ExaminationNImages/img/url.png" 
                                             CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  
                                             Width="20px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </asp:Panel>
    </div>
</asp:Panel>

          <asp:Panel ID="Panel3" runat="server" Visible="False">
    <asp:Button ID="Button1" runat="server" Text="-" OnClick="addButtonTwo" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-1"></div>
                <asp:Label ID="Label3" class="col-md-4 labeltext" runat="server">Institute :</asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="DropDownList1" class="form-select" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-md-1"></div>
                <asp:Label ID="Label5" class="col-md-4 labeltext" runat="server">Class :</asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="TextBox1" runat="server" class="form-control input-sm" 
                                 Placeholder="Class name"></asp:TextBox>
                </div>
            </div>
      <div class="row mt-2">
    <div class="col-md-1"></div>
    <asp:Label ID="Label6" class="col-md-4 labeltext" runat="server">No. Of Sections :</asp:Label>
    <div class="col-md-6">
        <select id="multiSelect" class="form-control input-sm cap" multiple runat="server">
            <option value="1">Section 1</option>
            <option value="2">Section 2</option>
            <option value="3">Section 3</option>
            <option value="4">Section 4</option>
            <option value="5">Section 5</option>
        </select>
    </div>
</div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="row mt-2">
        <div class="col-md-1"></div>
        <div class="col-md-10 text-center ps-5">
            <asp:Button ID="Button2" class="btn Submit" runat="server" Text="Save"></asp:Button> 
            <asp:Button ID="Button3" class="btn Submit" Visible="false" runat="server" Text="Update"></asp:Button>
        </div>
        <div class="col-md-1"></div>
    </div>
</asp:Panel>


       </div>
        </div>
         </div>
          </div>
          </div>
    </form>
    
</body>
</html>
