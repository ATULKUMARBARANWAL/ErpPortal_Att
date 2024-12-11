<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Importtest.aspx.vb" Inherits="Leads_Importtest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <script src="../LeadJquery/JavaScript.js" type="text/javascript"></script>

  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css"/>


  
     <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <link href="../LeadCSS/ImportLead.css" rel="stylesheet" type="text/css" />
    <title></title>
 
  <script>
      $(function () {
          $('[data-toggle="tooltip"]').tooltip()
      })
      function chooseFile() {
          document.getElementById("FileUpload1").click();
      }
     </script>
     <style>
         
     #GridPanel
     {
         
         min-height:0px;
         max-height:400px;
         }
         
      #Panel2
       {
         
         min-height:0px;
         max-height:400px;
         }
         
         #Panel1
         {
               min-height:0px;
         max-height:400px;
             }
   maincontainer
{
     
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-top:24px;
    margin-top:0px;
    background-color:#fff;
    padding:12px 18px;
}


 .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
    
    .hiddencol
    {
        display:none;
        }
        .maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}

     </style>
     
    
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid maincontainer">
    <div class="row"> 
             <div class="col-md-6 text-left pl-4 mt-2">
                <asp:LinkButton ID="Linkbtnsearch" runat="server" class="viewmore1" data-toggle="tooltip" data-placement="bottom" title="" >
                
                 <span><small>
                      1.Only import .xlx or .xlsx file.<br />
                      2.You must be uploaded only 2 mbps
                      <span class="dot" data-toggle="tooltip" data-placement="bottom" title="You can import Excel data into Access desktop 
                      databases or link to an Excel file, which results in a linked table."> <b style="font-weight:900;">...</b>
                 </small></span>
                 </asp:LinkButton>   
             </div>
             <%--<div class="col-md-6 text-end mt-2" > 
                 <a style="text-decoration:none;" class="viewmore" href="../Hemail/file_example_XLS_10%20(1).xls" download><h6><i class="fa fa-download"></i> Sample File</h6></a>
             </div>--%>
      </div>

     <div class="row">
         
		<div class="panel">
			<div class="button_outer">
				<div class="btn_upload">
					<asp:FileUpload id="upload_file" name="" runat="server"></asp:FileUpload>
					<asp:Label ID="lblselectfile" runat="server" CssClass="pr-5" Text=""><i class="fa-solid fa-paperclip"></i> Browse New File</asp:Label>
                    
				</div>
                
				<div class="processing_bar"></div>
				<div class="success_box"></div>
			</div>
            <br />
            <asp:Label ID="filepath" runat="server" Text=""></asp:Label>
		</div>
        <asp:Label ID="Label12" class="error_msg" runat="server" Text=""></asp:Label>
		<div class="uploaded_file_view" id="uploaded_view">
			<%--<span class="file_remove">X</span>--%>
            <asp:Button ID="btnSave" class="btnsave" runat="server" Text="Done" />
		</div>
	</div>

    <div class="row mb-2">
    
    <asp:Panel ID="pnlcorrectIncorrect" runat="server" Visible="false">
    <!-- here the Correct and Incorrect button -->
    <div class="row">
      <div class="col-sm-6 CorrectIncorrect text-success pl-5">
          
        <%--  <asp:LinkButton ID="linkcorrect" runat="server" data-bs-toggle="modal" data-bs-target="#Div1" >
          <asp:Label ID="Lblcorrectleads" runat="server" Text="" CssClass="dot1"></asp:Label>
          </asp:LinkButton> --%>
      </div>
      <div class="col-sm-6 text-end CorrectIncorrect text-danger pr-5">
           Incorrect Data are
          <asp:LinkButton ID="linkwrong" runat="server" >
          <asp:Label ID="Lblwrongleads" runat="server" Text="" CssClass="dot2"></asp:Label>
          </asp:LinkButton>
      </div>
    </div>
       


    <!-- Correct Leads Data show in Grid after click Incorrect Lead Button -->
    <div class="row">
      
          
          
             <asp:GridView   ID="grdwrongpanel"  runat="server" Visible="false"  CellPadding="5"
                 Font-Size="Small" ForeColor="#15283c" GridLines="None" AutoGenerateColumns="False"  width="100%" CssClass="table-bordered" style="text-align:center">
                            <AlternatingRowStyle BackColor="White"  />
                            <Columns>
                             
                                    <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                  <asp:BoundField HeaderText="Reg No" DataField="RegNo"></asp:BoundField>
            <asp:BoundField HeaderText="Student" DataField="Student"></asp:BoundField>
           
            <asp:BoundField HeaderText="Semester" DataField="Sem"></asp:BoundField>
        <asp:BoundField HeaderText="Gender" DataField="Gender"></asp:BoundField>
        <asp:BoundField HeaderText="Batch" DataField="Batch"></asp:BoundField>

          <asp:BoundField HeaderText="Institue" DataField="Institue"></asp:BoundField>
           <asp:BoundField HeaderText="Course" DataField="Course"></asp:BoundField>
            <asp:BoundField HeaderText="Father Name" DataField="FatherName"></asp:BoundField>
            <asp:BoundField HeaderText="Mother Name" DataField="MotherName"></asp:BoundField>
            <asp:BoundField HeaderText="Email" DataField="Email"></asp:BoundField>
        <asp:BoundField HeaderText="Mobile No." DataField="Mobile"></asp:BoundField>
        <asp:BoundField HeaderText="Adhar No." DataField="AdharNo"></asp:BoundField>

                            </Columns>
                            

                        </asp:GridView>
       
         
           
    </div>

    </asp:Panel>

    </div>

     <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" class="panel1" >
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:GridView   ID="grvExcelData"  runat="server" Visible="true" CellPadding="5"
                 Font-Size="Small" ForeColor="#15283c" GridLines="None"  width="100%" CssClass="table-bordered"  style="text-align:center">
                            <AlternatingRowStyle BackColor="White"  />
                            <Columns>
                                  <asp:TemplateField HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
          
            <HeaderTemplate>
                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);true"  />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkRows" runat="server" onclick="Check_Click(this)"/>
            </ItemTemplate>
        </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            

                        </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" class="panel1" >
       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        <asp:GridView   ID="GridView1"  runat="server" Visible="false" CellPadding="5"
                 Font-Size="Small" ForeColor="#15283c" GridLines="None" AutoGenerateColumns="False"  width="100%" CssClass="table-bordered" style="text-align:center">
                            <AlternatingRowStyle BackColor="White"  />
                            <Columns>
                             
                                    <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                  <asp:BoundField HeaderText="Reg No" DataField="RegNo"></asp:BoundField>
            <asp:BoundField HeaderText="Student" DataField="Student"></asp:BoundField>
           
            <asp:BoundField HeaderText="Semester" DataField="Sem"></asp:BoundField>
        <asp:BoundField HeaderText="Gender" DataField="Gender"></asp:BoundField>
        <asp:BoundField HeaderText="Batch" DataField="Batch"></asp:BoundField>

          <asp:BoundField HeaderText="Institue" DataField="Institue"></asp:BoundField>
           <asp:BoundField HeaderText="Course" DataField="Course"></asp:BoundField>
            <asp:BoundField HeaderText="Father Name" DataField="FatherName"></asp:BoundField>
            <asp:BoundField HeaderText="Mother Name" DataField="MotherName"></asp:BoundField>
            <asp:BoundField HeaderText="Email" DataField="Email"></asp:BoundField>
        <asp:BoundField HeaderText="Mobile No." DataField="Mobile"></asp:BoundField>
        <asp:BoundField HeaderText="Adhar No." DataField="AdharNo"></asp:BoundField>

                            </Columns>
                            

                        </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

      <div class="row text-center">
    <div class="col-md-12 mt-2 ">

        <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-dark btnCanel pl-4 pr-5" >
        <i class="fa fa-window-close"></i> Cancel</asp:LinkButton>--%>
        <asp:Button ID="btnsubmit" Visible="true" class="btn Submitbtn" Width="12%" Height="40px" runat="server" Text="Import" />

        <br />
        </div>

</div>

<div class="row text-center">
    <div class="col-md-12 mt-2">

        <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-dark btnCanel pl-4 pr-5" >
        <i class="fa fa-window-close"></i> Cancel</asp:LinkButton>--%>
        <asp:Button ID="btnUPDATE" Visible="FALSE" class="btn Submitbtn" Width="12%" Height="40px" runat="server" Text="Update" />

        <br />
        </div>

</div>
</div>
    </form>

     <script>

         var btnUpload = $("#upload_file"),
            btnOuter = $(".button_outer");
         btnUpload.on("change", function (e) {
             var ext = btnUpload.val().split('.').pop().toLowerCase();
             if ($.inArray(ext, ['xlsx', 'xlx', 'csv', 'jpg']) == 1) {
                 $(".error_msg").text("Something went wrong");
             } else {
                 $(".error_msg").text("");
                 btnOuter.addClass("file_uploading");
                 setTimeout(function () {
                     btnOuter.addClass("file_uploaded");
                 }, 3000);
                 var uploadedFile = URL.createObjectURL(e.target.files[0]);
                 setTimeout(function () {
                     $("#uploaded_view").append('').addClass("show");
                 }, 3500);
             }
         });
         $(".file_remove").on("click", function (e) {
             $("#uploaded_view").removeClass("show");
             $("#uploaded_view").find("img").remove();
             btnOuter.removeClass("file_uploading");
             btnOuter.removeClass("file_uploaded");
         });

    </script>
    <script type = "text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "#f2f3f5";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "white";

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
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnsave.ClientID %>").click();
            }
        }
</script>
    

</body>
</html>
