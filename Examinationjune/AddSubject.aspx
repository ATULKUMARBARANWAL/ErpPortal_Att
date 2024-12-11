<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddSubject.aspx.vb" Inherits="AddSubject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
   
   
    <title></title>
<style>
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
            width:23%;
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
         .hiddencol1
{
    Display:none;
    }
       .maincontainer
{
    
   
    border-radius:8px;
     padding-top:24px;
    margin-top:12px;
    background-color:#fff;
    padding:12px 18px;
}
   </style>
    <script>
        function SelectheaderCheckboxes(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {



                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {


                        row.style.backgroundColor = "#f2f3f5";

                        inputList[i].checked = true;

                    }

                    else {

                        if (row.rowIndex % 2 == 0) {
                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }
 
</script>
</head>
<body>
    <form id="form1" runat="server">
    
     <div class="my-form">
      <div class="cotainer">
        <div class="row justify-content-center mt-1">
          <div class="col-md-12">
            <div class="card">
              <div class="card-header"><h5 class="subject">Subject Master</h5></div>
                <div class="card-body">
                            
                   <div class="row">
                     <div class="col-md-1"></div>
                      <asp:Label ID="lblsubject" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject :</asp:Label>
                       <div class="col-md-6">
                           <asp:TextBox ID="txtsub" runat="server" class="form-control input-sm" Placeholder="Subject name"></asp:TextBox>
                       </div>             
                     </div>

                        <div class="row mt-2">
                        <div class="col-md-1"></div>
                        <asp:Label ID="lblcode" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Code :</asp:Label>
                        <div class="col-md-6">
                           <asp:TextBox ID="txtcode" runat="server" class="form-control input-sm cap" Placeholder="Subject code"></asp:TextBox>
                        </div>          
                        </div>

                         <div class="row mt-2">
                           <div class="col-md-1"></div>
                             <asp:Label ID="lblprefix" class="col-md-4 col-form-label text-md-left labeltext" runat="server">Subject Prefix :</asp:Label>
                            <div class="col-md-6">
                              <asp:TextBox ID="txtprefix" runat="server" class="form-control input-sm cap" Placeholder="Subject prefix"></asp:TextBox>
                            </div>         
                          </div>
                          <br />
         
                          <div class="row mt-2">  
                          <div class="col-md-1"></div>    
                            <div class="col-md-10 ps-5 text-center">       
                              <asp:Button ID="btnAdd" class="btn Submit" runat="server" Text="Save"></asp:Button>  
                                <asp:Button ID="btnupdate" class="btn Submit" Visible="false" runat="server" Text="Update"></asp:Button>         
                           
                              </div>
                             
                              <div class="col-md-1"><asp:Button ID="btndelete" class="btn btn-danger" runat="server" Text="Delete"></asp:Button>  </div>    
                           </div>  
                <hr />
      <div class="container-fluid bg-white mt-2 maincontainer">
          <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Visible="true">

         <asp:GridView ID="grdSubject" AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="Subjectid"
          Class="table table-bordered" OnRowEditing="OnRowEditing"  OnRowDeleting="OnRowDeleting" >
        <Columns>
              <asp:TemplateField HeaderText="S.No.">
                         <HeaderTemplate>
                         <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)" />
                         </HeaderTemplate>
                         <ItemTemplate>
                         <asp:CheckBox ID="btnselect" runat="server" />
                         </ItemTemplate>
                         </asp:TemplateField>
                     
             <asp:TemplateField HeaderText="Modify">
                   <ItemTemplate>
                       <asp:LinkButton ID="LinkModify" CommandName="Edit" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>
                       </ItemTemplate>
                    <%--   <EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                   </EditItemTemplate>--%>
             </asp:TemplateField>
        <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
         <asp:BoundField HeaderText="Subjectid" ItemStyle-CssClass="hiddencol1" HeaderStyle-CssClass="hiddencol1" DataField="Subjectid"></asp:BoundField>
                <asp:BoundField HeaderText="Subject" ReadOnly="true" ItemStyle-Width="350" DataField="Subject"></asp:BoundField>
                <asp:BoundField HeaderText="Subject Code" ReadOnly="true" DataField="Subjectcode"></asp:BoundField>
                <asp:BoundField HeaderText="Subject Prefix" ReadOnly="true" DataField="Subprefix"></asp:BoundField>   
           <asp:TemplateField HeaderText="Delete">
                   <ItemTemplate>
                       <asp:ImageButton ID="lnkDelete" CommandName="Delete" CausesValidation="False" ImageUrl="~/img/url.png"
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
 </div>
 
    </form>
</body>
</html>